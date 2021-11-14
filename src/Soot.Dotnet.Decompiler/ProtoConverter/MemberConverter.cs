using ICSharpCode.Decompiler.TypeSystem;
using Soot.Dotnet.Decompiler.Models.Protobuf;

namespace Soot.Dotnet.Decompiler.ProtoConverter
{
    /**
     * Members like Fields, Methods, Events
     */
    public partial class ProtoConverter
    {
        private FieldDefinition ToFieldDefinition(IField field)
        {
            var f = new FieldDefinition
            {
                Accessibility = EnumConverter.ToAccessibilityEnum(field.Accessibility),
                IsAbstract = field.IsAbstract,
                IsConst = field.IsConst,
                IsOverride = field.IsOverride,
                IsSealed = field.IsSealed,
                IsStatic = field.IsStatic,
                IsVirtual = field.IsVirtual,
                IsReadOnly = field.IsReadOnly,
                IsExplicitInterfaceImplementation = field.IsExplicitInterfaceImplementation,
                Type = ToTypeDefinitionMessage(field.Type),
                TypeKind = EnumConverter.ToTypeKindEnum(field.Type.Kind),
                Name = field.Name,
                FullName =  field.FullName,
                DeclaringType = ToTypeDefinitionMessage(field.DeclaringType)
            };
            return f;
        }

        private MethodDefinition ToMethodDefinition(IMethod method, bool fullCopy = false)
        {
            var m = new MethodDefinition
            {
                Name = method.Name,
                HasBody = method.HasBody,
                IsAbstract = method.IsAbstract,
                IsAccessor = method.IsAccessor,
                IsConstructor = method.IsConstructor,
                IsDestructor = method.IsDestructor,
                IsStatic = method.IsStatic,
                IsVirtual = method.IsVirtual,
                IsOperator = method.IsOperator,
                IsSealed = method.IsSealed,
                ReturnType = ToTypeDefinitionMessage(method.ReturnType),
                IsExplicitInterfaceImplementation = method.IsExplicitInterfaceImplementation,
                Accessibility = EnumConverter.ToAccessibilityEnum(method.Accessibility),
                FullName = method.FullName,
                DeclaringType = ToTypeDefinitionMessage(method.DeclaringType)
            };
            
            foreach (var parameter in method.Parameters)
            {
                var p = new ParameterDefinition
                {
                    Type = ToTypeDefinitionMessage(parameter.Type),
                    ParameterName = parameter.Name,
                    IsIn = parameter.IsIn,
                    IsOut = parameter.IsOut,
                    IsRef = parameter.IsRef,
                    IsOptional = parameter.IsOptional
                };
                m.Parameter.Add(p);
                if (parameter.Type is PointerType)
                    m.IsUnsafe = true;
            }

            if (!fullCopy) return m;
            
            var attributes = method.GetAttributes();
            if (attributes == null) return m;
            foreach (var attribute in attributes)
            {
                m.Attributes.Add(ToAttributeDefinition(attribute));
                var attrString = attribute.AttributeType.ReflectionName;
                if (attrString.Contains("System.Runtime.InteropServices.DllImportAttribute") ||
                    attrString.Contains("System.Runtime.CompilerServices")) 
                    m.IsExtern = true;
            }

            return m;
        }

        private PropertyDefinition ToPropertyDefinition(IProperty property)
        {
            var p = new PropertyDefinition
            {
                Accessibility = EnumConverter.ToAccessibilityEnum(property.Accessibility),
                CanGet =  property.CanGet,
                CanSet = property.CanSet,
                IsAbstract = property.IsAbstract,
                IsOverride = property.IsOverride,
                IsSealed = property.IsSealed,
                IsStatic = property.IsStatic,
                IsVirtual = property.IsVirtual,
                IsExplicitInterfaceImplementation = property.IsExplicitInterfaceImplementation,
                Type = ToTypeDefinitionMessage(property.ReturnType),
                TypeKind = EnumConverter.ToTypeKindEnum(property.ReturnType.Kind),
                Name = property.Name
            };

            var attributes = property.GetAttributes();
            foreach (var attribute in attributes)
            {
                p.Attributes.Add(ToAttributeDefinition(attribute));
                var attrString = attribute.AttributeType.ReflectionName;
                if (attrString.Contains("System.Runtime.InteropServices.DllImportAttribute") ||
                    attrString.Contains("System.Runtime.CompilerServices")) 
                    p.IsExtern = true;
            }

            if (property.CanGet)
                p.Getter = ToMethodDefinition(property.Getter, true);
            if (property.CanSet)
                p.Setter = ToMethodDefinition(property.Setter, true);
            
            return p;
        }

        private EventDefinition ToEventDefinition(IEvent @event)
        {
            var e = new EventDefinition
            {
                Accessibility = EnumConverter.ToAccessibilityEnum(@event.Accessibility),
                Name = @event.Name,
                FullName = @event.FullName
            };
            if (@event.CanAdd)
            {
                e.CanAdd = true;
                e.AddAccessorMethod = ToMethodDefinition(@event.AddAccessor);
            }
            if (@event.CanInvoke)
            {
                e.CanInvoke = true;
                e.InvokeAccessorMethod = ToMethodDefinition(@event.InvokeAccessor);
            }
            // ReSharper disable once InvertIf
            if (@event.CanRemove)
            {
                e.CanRemove = true; 
                e.RemoveAccessorMethod = ToMethodDefinition(@event.RemoveAccessor);
            }
            
            return e;
        }
    }
}