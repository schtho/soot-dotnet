using ICSharpCode.Decompiler.CSharp;
using Soot.Dotnet.Decompiler.Helper;

namespace Soot.Dotnet.Decompiler.Parser
{
    /// <summary>
    /// This class extracts definitions and CIL code with the help of decompiler/disassembler and converts with the help
    /// of ProtoConverter to a proto message
    /// </summary>
    public partial class AssemblyParser
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        
        private string AssemblyFileAbsolutePath { get; }
        private bool DebugMode { get; }
        private CSharpDecompiler Decompiler { get; }
        private MethodBodyDisassembler Disassembler { get; }

        public AssemblyParser(string assemblyFileAbsolutePath, bool debugMode = false)
        {
            AssemblyFileAbsolutePath = assemblyFileAbsolutePath;
            DebugMode = debugMode;
            Decompiler = IlSpyUtils.GetDecompiler(AssemblyFileAbsolutePath);
            Disassembler = IlSpyUtils.GetDisassembler(AssemblyFileAbsolutePath);
        }

    }
}