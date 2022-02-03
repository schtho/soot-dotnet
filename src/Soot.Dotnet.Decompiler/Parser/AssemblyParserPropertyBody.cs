using System;
using System.Linq;
using Soot.Dotnet.Decompiler.Exceptions;
using Soot.Dotnet.Decompiler.Helper;
using Soot.Dotnet.Decompiler.Models.Cli;

namespace Soot.Dotnet.Decompiler.Parser
{
    public partial class AssemblyParser
    {
        public CliByteArray GetMethodBodyOfProperty(string typeReflectionName, string propertyName, bool isSetter)
        {
            var returnValue = new CliByteArray();
            try
            {
                var declaringType = GetType(DefinitionUtils.ConvertJvmToCilNaming(typeReflectionName));

                var propertyDefinition = declaringType.Properties.FirstOrDefault(x => x.Name.Equals(propertyName));
                if (propertyDefinition == null)
                    throw new MemberNotExistException(MemberNotExistException.Member.Property, propertyName);

                var methodDefinition = isSetter ? propertyDefinition.Setter : propertyDefinition.Getter;
                if (!(methodDefinition is {HasBody: true}))
                    throw new MethodBodyNotExistException(DefinitionUtils.ConvertJvmToCilNaming(typeReflectionName), methodDefinition.Name + " (property)");

                returnValue = HelperExtractMethodBody(methodDefinition);
            }
            catch (MethodBodyNotExistException e)
            {
                Logger.Warn(e.Message);
            }
            catch (MemberNotExistException e)
            {
                Logger.Warn(e.Message);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return returnValue;
        }
    }
}