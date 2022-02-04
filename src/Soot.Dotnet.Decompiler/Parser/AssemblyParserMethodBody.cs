using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using ICSharpCode.Decompiler.IL;
using ICSharpCode.Decompiler.TypeSystem;
using Soot.Dotnet.Decompiler.Exceptions;
using Soot.Dotnet.Decompiler.Helper;
using Soot.Dotnet.Decompiler.Models.Cli;

namespace Soot.Dotnet.Decompiler.Parser
{
    public partial class AssemblyParser
    {
        public CliByteArray GetMethodBody(string typeReflectionName,
            string methodName, string methodNameSuffix, int peToken)
        {
            methodName = DefinitionUtils.ConvertConstructorMethodName(methodName);
            Logger.SetProperty("TypeName", typeReflectionName);
            Logger.SetProperty("Method", methodName + methodNameSuffix);

            var returnValue = new CliByteArray();
            try
            {
                var declaringType = GetType(typeReflectionName);
                
                // Get method definition by PE Token
                IMethod methodDefinition;
                if (peToken != 0)
                    methodDefinition = GetMethodDefinition(declaringType, peToken, methodName);
                else if (DefinitionUtils.GetPeTokenOfMethodSuffix(methodNameSuffix) != 0)
                    methodDefinition = GetMethodDefinition(declaringType, DefinitionUtils.GetPeTokenOfMethodSuffix(methodNameSuffix), methodName);
                else
                    throw new MemberNotExistException(MemberNotExistException.Member.Method, methodName, peToken, declaringType.ReflectionName);
                
                if (!(methodDefinition is {HasBody: true}))
                    throw new MethodBodyNotExistException(typeReflectionName, methodName, peToken);

                var methodBody = ExtractMethodBody(methodDefinition);
                returnValue = ProtoConverter.ProtoConverter.ConvertMethodBody(methodBody);
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

        private static IMethod GetMethodDefinition(ITypeDefinition declaringType, int peToken, string methodName)
        {
            var methodDefinitions = declaringType.Methods.Where(x => MetadataTokens.GetToken(x.MetadataToken) == peToken).ToList();
            if (methodDefinitions.Count == 0)
                throw new MemberNotExistException(MemberNotExistException.Member.Method, methodName, peToken, declaringType.ReflectionName);
            return methodDefinitions.First();
        }

        /// <summary>
        /// Extract Method Body of given Method Definition
        /// </summary>
        /// <param name="methodDefinition"></param>
        /// <returns></returns>
        private ILFunction ExtractMethodBody(IMethod methodDefinition)
        {
            if (DebugMode)
                Logger.WithProperty("Method", methodDefinition.ReflectionName).Info("Extracting method body");

            return Disassembler.DecompileBody(methodDefinition);
        }
    }
}