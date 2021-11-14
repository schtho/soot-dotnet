using System;
using System.Reflection;

namespace Soot.Dotnet.Decompiler.Helper
{
    public static class AssemblyUtils
    {
        /// <summary>
        /// Check if given file with absolute path is an assembly
        /// </summary>
        /// <param name="assemblyFileAbsolutePath">Path to the assembly file</param>
        /// <returns></returns>
        public static bool FileIsAssembly(string assemblyFileAbsolutePath)
        {
            try
            {
                if (!assemblyFileAbsolutePath.EndsWith(".exe") && !assemblyFileAbsolutePath.EndsWith(".dll")) 
                    return false;
                // Attempt to resolve the assembly
                AssemblyName.GetAssemblyName(assemblyFileAbsolutePath);
                // Nothing blew up, so it's an assembly
                return true;
            }
            catch(Exception)
            {
                // Something went wrong, it is not an assembly (specifically a 
                // BadImageFormatException will be thrown if it could be found
                // but it was NOT a valid assembly
                return false;
            }   
        }

    }
}