using System;
using System.Runtime.InteropServices;

namespace Soot.Dotnet.TestSnippets.Members
{
    public class Native
    {
        // Import user32.dll (containing the function we need) and define
        // the method corresponding to the native function.
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

        public static void DoSth()
        {
            // Invoke the function as a regular managed method.
            MessageBox(IntPtr.Zero, "Command-line message box", "Attention!", 0);
        }
    }
}