using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf;
using ICSharpCode.Decompiler.TypeSystem;
using Soot.Dotnet.Decompiler.Exceptions;
using Soot.Dotnet.Decompiler.Helper;
using Soot.Dotnet.Decompiler.Models.Cli;
using Soot.Dotnet.Decompiler.Models.Protobuf;

namespace Soot.Dotnet.Decompiler.Parser
{
    public partial class AssemblyParser
    {
        public CliByteArray GetMethodBody(string typeReflectionName, 
            string methodName, List<SootTypeMsg> mArgumentTypes)
        {
            Logger.SetProperty("TypeName", typeReflectionName);
            Logger.SetProperty("Method", methodName);

            var returnValue = new CliByteArray();
            try
            {
                // Split Soot method name (e.g. MyMethod[[&_]]
                var (method, methodArgSpecs) = MethodNamePair(DefinitionUtils.ConvertConstructorMethodName(methodName));

                var declaringType = GetType(DefinitionUtils.ConvertJvmToCilNaming(typeReflectionName));

                // Prepare Method Arg Type List and fill additional details
                ExpandSootMethodArgList(ref mArgumentTypes, methodArgSpecs);

                // Get right method definition
                var methodDefinitions = declaringType.Methods.Where(x => x.Name.Equals(method)).ToList();
                if (DebugMode)
                    Logger.Info("Found " + methodDefinitions.Count +
                                " Definitions for given method name (without checking params)");
                if (methodDefinitions.Count == 0)
                    throw new MemberNotExistException(MemberNotExistException.Member.Method, method, DefinitionUtils.ConvertJvmToCilNaming(typeReflectionName));
                
                var methodDefinition = SelectMethod(methodDefinitions, mArgumentTypes);
                if (!(methodDefinition is {HasBody: true}))
                    throw new MethodBodyNotExistException(DefinitionUtils.ConvertJvmToCilNaming(typeReflectionName), method);

                returnValue = ExtractMethodBody(methodDefinition);
            }
            catch (MethodBodyNotExistException e)
            {
                Logger.Warn(e.Message);
            }
            catch (SystemException e)
            {
                Logger.Error(e.Message);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            Logger.SetProperty("TypeName", "");
            Logger.SetProperty("Method", "");
            return returnValue;
        }
        
        /// <summary>
        /// Extract Method Body of given Method Definition
        /// </summary>
        /// <param name="methodDefinition"></param>
        /// <returns></returns>
        private CliByteArray ExtractMethodBody(IMethod methodDefinition)
        {
            if (DebugMode)
                Logger.Info("Extracting method body of " + methodDefinition.ReflectionName);

            var returnValue = new CliByteArray();
            var ilMethodBlock = Disassembler.DecompileBody(methodDefinition);

            // if (DebugMode)
            //     Logger.Info("Got IL method with " + ((BlockContainer) ilMethodBlock.Body).Blocks.Count +
            //                 " blocks in BlockContainer.");

            var protoConverter = new ProtoConverter.ProtoConverter();
            var protoBlockContainer = protoConverter.ToIlFunctionMessage(ilMethodBlock);
            returnValue.SetArray(protoBlockContainer.ToByteArray());
            return returnValue;
        }
    }
}