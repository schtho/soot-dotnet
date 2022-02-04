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
            typeReflectionName = DefinitionUtils.ConvertJvmToCilNaming(typeReflectionName);
            
            var returnValue = new CliByteArray();
            try
            {
                var declaringType = GetType(typeReflectionName);

                var propertyDefinition = declaringType.Properties.FirstOrDefault(x => x.Name.Equals(propertyName));
                if (propertyDefinition == null)
                    throw new MemberNotExistException(MemberNotExistException.Member.Property, propertyName);

                var methodDefinition = isSetter switch
                {
                    true when propertyDefinition.CanSet => propertyDefinition.Setter,
                    false when propertyDefinition.CanGet => propertyDefinition.Getter,
                    _ => null
                };

                if (!(methodDefinition is {HasBody: true}))
                    throw new MethodBodyNotExistException(typeReflectionName, propertyDefinition.Name, isSetter);

                var methodBody = ExtractMethodBody(methodDefinition);
                returnValue = ProtoConverter.ProtoConverter.ConvertMethodBody(methodBody);
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