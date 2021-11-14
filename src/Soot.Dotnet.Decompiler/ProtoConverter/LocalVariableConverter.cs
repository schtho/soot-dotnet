using ICSharpCode.Decompiler.IL;
using Soot.Dotnet.Decompiler.Models.Protobuf;

namespace Soot.Dotnet.Decompiler.ProtoConverter
{
    public partial class ProtoConverter
    {
        /// <summary>
        /// Convert Variable Definitions to proto message
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        private IlVariableMsg ToIlVariableMsg(ILVariable variable)
        {
            var v = new IlVariableMsg
            {
                Name = variable.Name,
                Type = ToTypeDefinitionMessage(variable.Type),
                HasInitialValue = variable.HasInitialValue,
                VariableKind = EnumConverter.ToIlVariableKindEnum(variable.Kind)
            };
            // rename "result" variable in try filter block container, because one method body
            if (_isTryFilter && variable.Name.Equals("result"))
                v.Name = "resultOfFilter";
            return v;
        }
    }
}