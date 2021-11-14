using System.Collections;
using System.Reflection.Metadata;
using ICSharpCode.Decompiler.TypeSystem;
using Soot.Dotnet.Decompiler.Models.Protobuf;

namespace Soot.Dotnet.Decompiler.ProtoConverter
{
    public partial class ProtoConverter
    {
        /// <summary>
        /// Convert attributes of types and methods (named, fixed) to proto message
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        private AttributeDefinition ToAttributeDefinition(IAttribute attribute)
        {
            var a = new AttributeDefinition
            {
                AttributeType = ToTypeDefinitionMessage(attribute.AttributeType)
            };
            if (attribute.Constructor != null) 
                a.Constructor = attribute.Constructor.Name;
            foreach (var fixedArgument in attribute.FixedArguments)
                a.FixedArguments.Add(ToAttributeArgumentDefinition(fixedArgument));
            foreach (var namedArguments in attribute.NamedArguments)
                a.FixedArguments.Add(ToAttributeArgumentDefinition(namedArguments));
            return a;
        }

        private AttributeArgumentDefinition ToAttributeArgumentDefinition(CustomAttributeTypedArgument<IType> argument)
        {
            return ToAttributeArgumentDefinition(argument.Type, argument.Value);
        }

        private AttributeArgumentDefinition ToAttributeArgumentDefinition(
            CustomAttributeNamedArgument<IType> argument)
        {
            return ToAttributeArgumentDefinition(argument.Type, argument.Value, argument.Name);
        }

        private AttributeArgumentDefinition ToAttributeArgumentDefinition(IType type, object value, string name = null)
        {
            var aad = new AttributeArgumentDefinition
            {
                Type = ToTypeDefinitionMessage(type), 
                Name = name ?? ""
            };
            
            switch (value)
            {
                case IList valArr:
                    foreach (var val in valArr)
                    {
                        switch (val)
                        {
                            case string v:
                                aad.ValueString.Add(v);
                                break;
                            case int v:
                                aad.ValueInt32.Add(v);
                                break;
                            case uint v:
                                aad.ValueInt32.Add((int) v);
                                break;
                            case bool v:
                                aad.ValueInt32.Add(v ? 1 : 0);
                                break;
                            case byte v:
                                aad.ValueInt32.Add(v);
                                break;
                            case sbyte v:
                                aad.ValueInt32.Add(v);
                                break;
                            case char v:
                                aad.ValueInt32.Add(v);
                                break;
                            case double v:
                                aad.ValueDouble.Add(v);
                                break;
                            case float v:
                                aad.ValueFloat.Add(v);
                                break;
                            case short v:
                                aad.ValueInt32.Add(v);
                                break;
                            case ushort v:
                                aad.ValueInt32.Add(v);
                                break;
                            case long v:
                                aad.ValueInt64.Add(v);
                                break;
                            case ulong v:
                                aad.ValueInt64.Add((long) v);
                                break;
                            default:
                                // object
                                // System.Type
                                // Enum
                                aad.ValueString.Add(value.ToString());
                                break;
                        }
                    }
                    break;
                case string v:
                    aad.ValueString.Add(v);
                    break;
                case int v:
                    aad.ValueInt32.Add(v);
                    break;
                case uint v:
                    aad.ValueInt32.Add((int) v);
                    break;
                case bool v:
                    aad.ValueInt32.Add(v ? 1 : 0);
                    break;
                case byte v:
                    aad.ValueInt32.Add(v);
                    break;
                case sbyte v:
                    aad.ValueInt32.Add(v);
                    break;
                case char v:
                    aad.ValueInt32.Add(v);
                    break;
                case double v:
                    aad.ValueDouble.Add(v);
                    break;
                case float v:
                    aad.ValueFloat.Add(v);
                    break;
                case short v:
                    aad.ValueInt32.Add(v);
                    break;
                case ushort v:
                    aad.ValueInt32.Add(v);
                    break;
                case long v:
                    aad.ValueInt64.Add(v);
                    break;
                case ulong v:
                    aad.ValueInt64.Add((long) v);
                    break;
                default:
                    // object
                    // System.Type
                    // Enum
                    // empty
                    aad.ValueString.Add(value != null ? value.ToString() : "");
                    break;
            }
            return aad;
        }
    }
}