using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;

namespace Soot.Dotnet.Decompiler.Helper
{
    public static class IlSpyUtils
    {
        internal static CSharpDecompiler GetDecompiler(string assemblyFileAbsolutePath)
        {
            var decompiler = new CSharpDecompiler(assemblyFileAbsolutePath, new DecompilerSettings
            {
                ThrowOnAssemblyResolveErrors = false, ThrowExpressions = false
            });
            return decompiler;
        }

        internal static MethodBodyDisassembler GetDisassembler(string assemblyFileAbsolutePath)
        {
            var disassembler = new MethodBodyDisassembler(assemblyFileAbsolutePath, new DecompilerSettings
            {
                ThrowOnAssemblyResolveErrors = false, ThrowExpressions = false
            });
            return disassembler;
        }
    }
}