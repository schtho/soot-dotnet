using System;
using System.Linq;
using Google.Protobuf;
using ICSharpCode.Decompiler.TypeSystem;
using Soot.Dotnet.Decompiler.Helper;
using Soot.Dotnet.Decompiler.Models.Cli;
using Soot.Dotnet.Decompiler.Models.Protobuf;

namespace Soot.Dotnet.Decompiler.Parser
{
    public partial class AssemblyParser
    {
        public CliByteArray GetTypeDefinition(string typeReflectionName)
        {
            var returnValue = new CliByteArray();
            var protoConverter = new ProtoConverter.ProtoConverter();
            
            var type = GetType(typeReflectionName);
            var typeDefinitionMessage = protoConverter.ToTypeDefinitionMessage(type, true);
            
            var protoArray = typeDefinitionMessage.ToByteArray();
            returnValue.SetArray(protoArray);
            return returnValue;
        }

        /// <summary>
        /// Get all Types of a given assembly
        /// </summary>
        /// <returns></returns>
        public CliByteArray GetAllTypeDefinitions()
        {
            var returnValue = new CliByteArray();
            var aacMessage = new AssemblyAllTypes();

            try
            {
                var protoConverter = new ProtoConverter.ProtoConverter();
                foreach (var type in Decompiler.TypeSystem.MainModule.TypeDefinitions) {
                    if (type.Name.Equals("<Module>"))
                        continue;

                    aacMessage.ListOfTypes.Add(protoConverter.ToTypeDefinitionMessage(type, true));
                }
                // TODO add support for multi assembly with .netmodule files, after editing ILSpy API
                
                // add all referenced types
                // TODO replace loop with sth like ICSharpCode.ILSpy.Metadata.TypeRefTableTreeNode or System.Reflection.Metadata.MetadataReader.GetTypeReference()
                foreach (var module in Decompiler.TypeSystem.Modules)
                {
                    foreach (var typeDefinition in module.TypeDefinitions)
                    {
                        if (typeDefinition.Name.Equals("<Module>"))
                            continue;
                        aacMessage.AllReferencedModuleTypes.Add(DefinitionUtils.ConvertCilToJvmNaming(DefinitionUtils.GetTypeFullname(typeDefinition)));
                    }
                }

                var protoArray = aacMessage.ToByteArray();
                returnValue.SetArray(protoArray);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return returnValue;
        }
        
        private ITypeDefinition GetType(string typeReflectionName)
        {
            typeReflectionName = DefinitionUtils.ConvertJvmToCilNaming(typeReflectionName);
            
            var type = Decompiler.TypeSystem.MainModule.TypeDefinitions
                .FirstOrDefault(x => x.ReflectionName.Equals(typeReflectionName));
            if (type == null)
                throw new Exception("Given type " + typeReflectionName + " does not exist in the assembly: " + AssemblyFileAbsolutePath + "!");
            return type;
        }
    }
}