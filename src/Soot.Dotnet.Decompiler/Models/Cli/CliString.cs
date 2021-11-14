using System;
using System.Runtime.InteropServices;

namespace Soot.Dotnet.Decompiler.Models.Cli
{
    /// <summary>
    /// Model for marshaling a string through C++ NativeHost
    /// This model is also implemented in Soot.Dotnet.NativeHost
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CliString
    {
        public IntPtr Message;
        
        public override string ToString()
        {
            var message = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? Marshal.PtrToStringUni(Message)
                : Marshal.PtrToStringUTF8(Message);
            return message;
        }
    }
}