using System;
using ICSharpCode.Decompiler.TypeSystem;

namespace Soot.Dotnet.Decompiler.Helper
{
    /// <summary>
    /// Helper for ILSpy definitions such as MethodDefinition or TypeDefinition
    /// </summary>
    public static class DefinitionUtils
    {
        /// <summary>
        /// Convert java byte code constructor names back to dotnet/CLI constructor names
        /// </summary>
        /// <param name="methodName">java byte code constructor method name</param>
        /// <returns>CLI constructor method name</returns>
        internal static string ConvertConstructorMethodName(string methodName)
        {
            methodName = methodName.Replace("<init>", ".ctor");
            methodName = methodName.Replace("<clinit>", ".cctor");
            methodName = methodName.Replace("$", "+");
            return methodName;
        }

        internal static string ConvertCilToJvmNaming(string typeName)
        {
            typeName = typeName.Replace("+", "$");
            return typeName;
        }
        
        internal static string ConvertJvmToCilNaming(string typeName)
        {
            typeName = typeName.Replace("$", "+");
            return typeName;
        }

        /// <summary>
        /// Resolve String of given Type as Fullname
        /// </summary>
        /// <param name="type">e.g. System.Type[]&</param>
        /// <returns>e.g. System.Type</returns>
        public static string GetTypeFullname(IType type)
        {
            while (true)
            {
                switch (type)
                {
                    case ParameterizedType definition:
                        type = definition.GenericType;
                        continue;
                    case ArrayType definition:
                        type = definition.ElementType;
                        continue;
                    case ByReferenceType definition:
                        type = definition.ElementType;
                        continue;
                    case PointerType definition:
                        type = definition.ElementType;
                        continue;
                    case TupleType definition:
                        type = definition.UnderlyingType;
                        continue;
                    default:
                        return type.ReflectionName.StartsWith("`") ? 
                            "System.Object" : 
                            type.ReflectionName;
                }
            }
        }
        
        /// <summary>
        /// A Jimple Method can be: MyMethod[[0123456]]
        /// The suffix is [[0123456]]. Parse this suffix to get the unique PE Token 0123456 of the given method
        /// </summary>
        /// <param name="methodSuffix"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int GetPeTokenOfMethodSuffix(string methodSuffix)
        {
            if (string.IsNullOrWhiteSpace(methodSuffix))
                return 0;
            if (!(methodSuffix.Contains("[[") && methodSuffix.Contains("]]")))
                return 0;
            var token = methodSuffix[2..^2];
            try
            {
                return int.Parse(token);
            }
            catch (Exception)
            {
                throw new Exception("Could not parse the PE token out of the method suffix!");
            }
        }
    }
}