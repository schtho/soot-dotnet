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
            var aacMessage = new AssemblyAllTypes();
            
            try
            {
                var decompiler = IlSpyUtils.GetDecompiler(AssemblyFileAbsolutePath);
                var protoConverter = new ProtoConverter.ProtoConverter();
                foreach (var type in decompiler.TypeSystem.MainModule.TypeDefinitions) {
                    if (type.Name.Equals("<Module>"))
                        continue;
                    
                    if (!type.ReflectionName.Equals(DefinitionUtils.ConvertJvmToCilNaming(typeReflectionName)))
                        continue;
                    aacMessage.ListOfTypes.Add(protoConverter.ToTypeDefinitionMessage(type, true));
                    break;
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
            var type = Decompiler.TypeSystem.MainModule.TypeDefinitions
                .FirstOrDefault(x => x.ReflectionName.Equals(typeReflectionName));
            if (type == null)
                throw new Exception("Given type " + typeReflectionName + " does not exist in the assembly: " + AssemblyFileAbsolutePath + "!");
            return type;
        }
        
        public CliByteArray GetAllTypeDefinitions()
        {
            var returnValue = new CliByteArray();
            var aacMessage = new AssemblyAllTypes();

            try
            {
                var decompiler = IlSpyUtils.GetDecompiler(AssemblyFileAbsolutePath);
                var protoConverter = new ProtoConverter.ProtoConverter();
                foreach (var type in decompiler.TypeSystem.MainModule.TypeDefinitions) {
                    if (type.Name.Equals("<Module>"))
                        continue;
                    aacMessage.ListOfTypes.Add(protoConverter.ToTypeDefinitionMessage(type, true));
                }
                // TODO add support for multi assembly with .netmodule files, after editing ILSpy API

                // add all referenced types
                // TODO replace loop with sth like ICSharpCode.ILSpy.Metadata.TypeRefTableTreeNode or System.Reflection.Metadata.MetadataReader.GetTypeReference()
                foreach (var module in decompiler.TypeSystem.Modules)
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
    }
}