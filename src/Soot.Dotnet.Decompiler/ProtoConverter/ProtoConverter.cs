using ICSharpCode.Decompiler.IL;
using Soot.Dotnet.Decompiler.Models.Protobuf;
using Block = ICSharpCode.Decompiler.IL.Block;

namespace Soot.Dotnet.Decompiler.ProtoConverter
{
    /// <summary>
    /// This class converts ILSpy objects, such as IlFunction or BlockContainer or Cil Instructions to a proto message
    /// This partial class is split into several files
    /// </summary>
    public partial class ProtoConverter
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly bool _isTryFilter;

        public ProtoConverter() { }

        private ProtoConverter(bool isTryFilter)
        {
            _isTryFilter = isTryFilter;
        }
        
        private IlBlock ToIlBlockMessage(Block block)
        {
            var b = new IlBlock {BlockName = block.Label};
            foreach (var instruction in block.Instructions) 
                b.ListOfIlInstructions.Add(VisitIlAst(instruction));
            return b;
        }
        
        private IlBlockContainerMsg ToIlBlockContainerMessage(BlockContainer bc)
        {
            var msg = new IlBlockContainerMsg();
            foreach (var block in bc.Blocks) 
                msg.Blocks.Add(ToIlBlockMessage(block));
            return msg;
        }

        /// <summary>
        /// Function -> BlockContainer -> Block -> Instruction
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        public IlFunctionMsg ToIlFunctionMessage(ILFunction function)
        {
            var msg = new IlFunctionMsg { Body = ToIlBlockContainerMessage((BlockContainer) function.Body) };
            // Convert variable definitions to proto message record
            foreach (var variable in function.Variables) 
                msg.Variables.Add(ToIlVariableMsg(variable));
            return msg;
        }

        /// <summary>
        /// Usage in Try/Catch/Finally Instructions to remove leave block x instruction, because these BlockContainer
        /// are part of the main BlockContainer
        /// </summary>
        /// <param name="container"></param>
        private static void CleanUpBlockContainer(ref BlockContainer container)
        {
            if (container?.Blocks == null || container.Blocks.Count == 0)
                return;
            
            // if void method remove "leave block x" instruction
            if (container.Blocks.Last().Instructions.Last() is Leave i && i.TargetLabel.Equals(container.Blocks.Last().Label))
                container.Blocks.Last().Instructions.RemoveAt(container.Blocks.Last().Instructions.Count-1);
        }

        
    }
}