using System;
using System.Collections.Generic;
using System.Linq;
using ICSharpCode.Decompiler.TypeSystem;
using Soot.Dotnet.Decompiler.Exceptions;
using Soot.Dotnet.Decompiler.Helper;
using Soot.Dotnet.Decompiler.Models.Protobuf;

namespace Soot.Dotnet.Decompiler.Parser
{
    /**
     * Due to the problem of call-by-val/call-by-ref problem in Soot (Java), select the right one
     */
    public partial class AssemblyParser
    {
        /// <summary>
        /// Part of GetMethodBody(): This method gets a list of method argument types and expand their objects with additional information
        /// </summary>
        /// <param name="mArgumentTypes">list of method argument types</param>
        /// <param name="methodArgSpecs">string of additional information</param>
        private static void ExpandSootMethodArgList(ref List<SootTypeMsg> mArgumentTypes, string methodArgSpecs)
        {
            if (string.IsNullOrWhiteSpace(methodArgSpecs)) return;
            
            // if (DebugMode)
            //     Logger.Info("ExpandSootMethodArgList: " + mArgumentTypes.Count + 
            //                 " Method Argument Types with the Method argument specs " + methodArgSpecs);
            
            // Split ArgumentSpecs string into array
            var methodSpecsArray = methodArgSpecs.Split(",");
            for (var i = 0; i < mArgumentTypes.Count; i++)
            {
                // if spec is a &, the sorted argument is a call by ref
                if (methodSpecsArray[i].Contains("&"))
                    mArgumentTypes[i].IsCallByRef = true;

                // if spec is ( ), the sorted argument is a generic - add generic types
                if (methodSpecsArray[i].StartsWith('(') && methodSpecsArray[i].EndsWith(')'))
                {
                    mArgumentTypes[i].IsGeneric = true;
                    var genericTypes = methodSpecsArray[i].Substring(1, methodSpecsArray[i].IndexOf(')')-1);
                    mArgumentTypes[i].GenericParameters.AddRange(genericTypes.Split(","));
                }
            }
        }

        /// <summary>
        /// Part of GetMethodBody(): Split a given method-name string into real method name and generic/call-by-ref parameter
        /// </summary>
        /// <param name="methodName">e.g. MyMethod[[_&]]</param>
        /// <returns>split tuple</returns>
        private Tuple<string, string> MethodNamePair(string methodName)
        {
            var methodSootName = DefinitionUtils.ConvertConstructorMethodName(methodName);

            // if method is renamed in soot due to cil and java bytecode differences
            if (methodSootName.IndexOf("[[", StringComparison.Ordinal) == -1
                || methodSootName.IndexOf("]]", StringComparison.Ordinal) == -1)
                return new Tuple<string, string>(methodSootName, "");

            var splitMethodName = methodSootName.Split("[[");
            var method = splitMethodName[0];
            var genRefParams = splitMethodName[1][..splitMethodName[1].IndexOf("]]", StringComparison.Ordinal)];
            if (DebugMode)
                Logger.Info("Split Methodname: " + method + " - GenRefParams: " + genRefParams);
            return new Tuple<string, string>(method, genRefParams);
        }

        private IMethod SelectMethod(IList<IMethod> methodDefinitions, IList<SootTypeMsg> mArgumentTypes)
        {
            if (DebugMode)
                Logger.Info("SelectMethod: Start selecting right method definitions with given method parameters (" +
                            mArgumentTypes.Count + " MethodParameters)");

            if (methodDefinitions.Count == 1)
                return methodDefinitions.First();

            // check number of params equal
            var numOfParams = mArgumentTypes.Count;
            methodDefinitions = methodDefinitions.Where(x => x.Parameters.Count.Equals(numOfParams)).ToList();
            if (methodDefinitions.Count.Equals(0))
                throw new MemberNotExistException(MemberNotExistException.Member.Method);
            
            // if only one definition left, returning it
            if (methodDefinitions.Count.Equals(1))
                return methodDefinitions.First();

            // check for equal ref types
            methodDefinitions = SelectMethodFilterRefTypes(methodDefinitions, mArgumentTypes);
            
            if (methodDefinitions.Count.Equals(0))
                throw new MemberNotExistException(MemberNotExistException.Member.Method);
            if (methodDefinitions.Count.Equals(1))
                return methodDefinitions.First();
            
            // check for primitives
            // Tuple with PrimTypesCount, CilPrimTypesCount, MethodDefinition
            var filteredMethodDefinitions = SelectMethodFilterPrimTypes(methodDefinitions, mArgumentTypes);
            var first = filteredMethodDefinitions.OrderByDescending(x => x.Item1)
                .ThenBy(x => x.Item2).FirstOrDefault();
            if (first == null)
                throw new MemberNotExistException(MemberNotExistException.Member.Method);
            
            if (DebugMode)
                Logger.Info("SelectMethod: Finished: Have found " + filteredMethodDefinitions.Count + " method definitions. Return one.");
            return first.Item3;
        }

        /// <summary>
        /// Part of SelectMethod()
        /// </summary>
        /// <param name="methodDefinitions"></param>
        /// <param name="mArgumentTypes"></param>
        /// <returns></returns>
        private IList<IMethod> SelectMethodFilterRefTypes(IList<IMethod> methodDefinitions, IList<SootTypeMsg> mArgumentTypes)
        {
            if (DebugMode)
                Logger.Info("SelectMethod: Check for ref parameters (" + methodDefinitions.Count + " existing definitions)");
            
            var filteredMethodDefinitions = new List<IMethod>();
            foreach (var definition in methodDefinitions)
            {
                var fulfill = true;
                // foreach parameter of given methodDefinition
                for (var j = 0; j < definition.Parameters.Count; j++)
                {
                    var mArgumentType = mArgumentTypes[j];
                    var defParameter = definition.Parameters[j];
                    
                    // if argument details not set correctly
                    if (mArgumentType.Kind == SootTypeMsg.Types.Kind.NoKind)
                    {
                        fulfill = false;
                        break;
                    }

                    // if argument is an array and a reference
                    if (mArgumentType.Kind == SootTypeMsg.Types.Kind.ArrayRef)
                    {
                        var type = defParameter.Type;
                        // if is ref step into to get array type
                        if (type is ByReferenceType tref)
                            type = tref.ElementType;

                        if (type is ArrayType arrayType)
                        {
                            // must have same array dimension
                            if (!mArgumentType.NumDimensions.Equals(arrayType.Dimensions))
                            {
                                fulfill = false;
                                break;
                            }

                            // array type has to be same then selected
                            var paramTypeString = DefinitionUtils.GetTypeFullname(arrayType);
                            if (!mArgumentType.TypeName.Equals(paramTypeString))
                            {
                                fulfill = false;
                                break;
                            }
                        }
                        else
                        {
                            fulfill = false;
                            break;
                        }
                    }

                    // if argument type is reference
                    if (mArgumentType.Kind == SootTypeMsg.Types.Kind.Ref)
                    {
                        if (!mArgumentType.IsGeneric)
                        {
                            // check if given arg type is the same as selected
                            if (!DefinitionUtils.ConvertCilToJvmNaming(DefinitionUtils.GetTypeFullname(defParameter.Type))
                                .Equals(mArgumentType.TypeName))
                            {
                                fulfill = false;
                                break;
                            }
                        }
                        else
                        {
                            // check for generics
                            if (defParameter.Type is ParameterizedType genericParameter)
                            {
                                if (!DefinitionUtils.GetTypeFullname(defParameter.Type).Equals(mArgumentType.TypeName))
                                    fulfill = false;
                                
                                for (int i = 0; i < genericParameter.TypeParameterCount; i++)
                                {
                                    /*var s = Utils.ConvertCilToJvmNaming(
                                            ProtoConverter.GetTypeFullname(genericParameter.TypeParameters[i]))
                                        .Equals(mArgumentType.GenericParameters[i]);*/
                                    var s = genericParameter.TypeParameterCount.Equals(mArgumentType.GenericParameters
                                        .Count);
                                    if (!s)
                                        fulfill = false;
                                }
                            }
                            else
                            {
                                fulfill = false;
                                break;
                            }
                        }
                    }
                }

                // if given definition fulfills ref argument definitions, add to filtered list
                if (fulfill)
                    filteredMethodDefinitions.Add(definition);
            }

            return filteredMethodDefinitions;
        }

        /// <summary>
        /// Part of SelectMethod()
        /// </summary>
        /// <param name="methodDefinitions"></param>
        /// <param name="mArgumentTypes"></param>
        /// <returns></returns>
        private List<Tuple<int, int, IMethod>> SelectMethodFilterPrimTypes(IList<IMethod> methodDefinitions,
            IList<SootTypeMsg> mArgumentTypes)
        {
            if (DebugMode)
                Logger.Info("SelectMethod: Check for primitive parameters (" + methodDefinitions.Count + " existing definitions)");
            
            // Tuple with PrimTypesCount, CilPrimTypesCount, MethodDefinition
            var filteredMethodDefinitions = new List<Tuple<int, int, IMethod>>();
            // foreach available method check if fulfills signature
            foreach (var definition in methodDefinitions)
            {
                var fulfill = true;
                
                // calc foreach method def the amount "right" prim types. Due to the mix of int and uint, select the def with the less difference
                var numPrimTypes = 0;
                var numCilPrimTypes = 0;
                for (var j = 0; j < mArgumentTypes.Count; j++)
                {
                    var mArgumentType = mArgumentTypes[j];
                    var defParameter = definition.Parameters[j];
                    
                    if (mArgumentType.Kind == SootTypeMsg.Types.Kind.Primitive ||
                        mArgumentType.Kind == SootTypeMsg.Types.Kind.ArrayPrim)
                    {
                        // check if prim parameter is call-by-ref or call-by-value - equals
                        if ((defParameter.IsIn || defParameter.IsOut || defParameter.IsRef) != mArgumentType.IsCallByRef)
                        {
                            fulfill = false;
                            break;
                        }
                    }
                    else
                        continue;

                    // if is primitive
                    if (mArgumentType.Kind == SootTypeMsg.Types.Kind.Primitive)
                    {
                        // Count and check for prim types
                        var paramTypeString = DefinitionUtils.GetTypeFullname(defParameter.Type);
                        var f = SelectMethodFillPrimTypeCounter(mArgumentType, paramTypeString,
                            ref numPrimTypes, ref numCilPrimTypes);
                        if (!f)
                        {
                            fulfill = false;
                            break;
                        }
                    }

                    // if is array
                    if (mArgumentType.Kind == SootTypeMsg.Types.Kind.ArrayPrim)
                    {
                        if (defParameter.Type is ArrayType arrayType)
                        {
                            // have to have same array dimension
                            if (!mArgumentType.NumDimensions.Equals(arrayType.Dimensions))
                            {
                                fulfill = false;
                                break;
                            }

                            var paramTypeString = DefinitionUtils.GetTypeFullname(arrayType);
                            var f = SelectMethodFillPrimTypeCounter(mArgumentType, paramTypeString,
                                ref numPrimTypes, ref numCilPrimTypes);
                            if (!f)
                            {
                                fulfill = false;
                                break;
                            }
                        }
                        else
                        {
                            fulfill = false;
                            break;
                        }
                    }
                }

                // Tuple with PrimTypesCount, CilPrimTypesCount, MethodDefinition
                if (fulfill)
                    filteredMethodDefinitions.Add(new Tuple<int, int, IMethod>(numPrimTypes, numCilPrimTypes, definition));
            }

            return filteredMethodDefinitions;
        }

        /// <summary>
        /// Part of SelectMethod()
        /// </summary>
        /// <param name="mArgumentType"></param>
        /// <param name="methodDefParameterString"></param>
        /// <param name="numPrimTypes"></param>
        /// <param name="numCilPrimTypes"></param>
        /// <returns></returns>
        private static bool SelectMethodFillPrimTypeCounter(SootTypeMsg mArgumentType, string methodDefParameterString,
            ref int numPrimTypes, ref int numCilPrimTypes)
        {
            switch (mArgumentType.TypeName)
            {
                case "int":
                    switch (methodDefParameterString)
                    {
                        case "System.Int32":
                            numPrimTypes++;
                            break;
                        case "System.UInt32":
                            numCilPrimTypes++;
                            break;
                        default:
                            return false;
                    }

                    break;
                case "byte":
                    switch (methodDefParameterString)
                    {
                        case "System.Byte":
                            numPrimTypes++;
                            break;
                        case "System.SByte":
                            numCilPrimTypes++;
                            break;
                        default:
                            return false;
                    }

                    break;
                case "char":
                    if (!methodDefParameterString.Equals("System.Char"))
                        return false;
                    break;
                case "double":
                    switch (methodDefParameterString)
                    {
                        case "System.Double":
                            numPrimTypes++;
                            break;
                        case "System.Decimal":
                            numCilPrimTypes++;
                            break;
                        default:
                            return false;
                    }

                    break;
                case "float":
                    if (!methodDefParameterString.Equals("System.Single"))
                        return false;
                    break;
                case "long":
                    switch (methodDefParameterString)
                    {
                        case "System.Int64":
                            numPrimTypes++;
                            break;
                        case "System.UInt64":
                            numCilPrimTypes++;
                            break;
                        default:
                            return false;
                    }

                    break;
                case "short":
                    switch (methodDefParameterString)
                    {
                        case "System.Int16":
                            numPrimTypes++;
                            break;
                        case "System.UInt16":
                            numCilPrimTypes++;
                            break;
                        default:
                            return false;
                    }

                    break;
                case "boolean":
                    if (!methodDefParameterString.Equals("System.Boolean"))
                        return false;
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}