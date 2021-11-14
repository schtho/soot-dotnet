using System;

namespace Soot.Dotnet.Decompiler.Exceptions
{
    public class MethodBodyNotExistException : Exception
    {
        public MethodBodyNotExistException() : base("Method Body does not exist!") { }
        
        public MethodBodyNotExistException(string className,
            string method) : base("Method Body of " + className + "." + method + " does not exist!")
        {
        }
    }
}