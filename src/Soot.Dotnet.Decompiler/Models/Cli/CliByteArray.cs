using System;
using System.Runtime.InteropServices;

namespace Soot.Dotnet.Decompiler.Models.Cli
{
    /// <summary>
    /// Model for marshaling a byte array through C++ NativeHost
    /// This model is also implemented in Soot.Dotnet.NativeHost
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CliByteArray
    {
        public int Length;
        
        public IntPtr ByteArrayData;

        public void SetArray(byte[] data)
        {
            ByteArrayData = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(byte))*data.Length);
            Marshal.Copy(data, 0, ByteArrayData, data.Length);
            Length = data.Length;
        }
        
        public byte[] GetArray()
        {
            byte[] managedArray = new byte[Length];
            Marshal.Copy(ByteArrayData, managedArray, 0, Length);
            return managedArray;
        }
    }
}