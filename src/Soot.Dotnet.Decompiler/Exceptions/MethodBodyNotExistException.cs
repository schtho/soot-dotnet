using System;
using Soot.Dotnet.Decompiler.Models.Protobuf;

namespace Soot.Dotnet.Decompiler.Exceptions
{
    public class MethodBodyNotExistException : Exception
    {
        public MethodBodyNotExistException() : base("Method Body does not exist!")
        {
        }

        public MethodBodyNotExistException(string className,
            string method) : base("Method Body of " + className + "." + method + " does not exist!")
        {
        }

        public MethodBodyNotExistException(string className, string method, int peToken) : base("Method Body of " +
            className + "." + method + " (PE Token: " + peToken + ") does not exist!")
        {
        }
        
        public MethodBodyNotExistException(string className, string property, bool isSetter) : base("Method Body of property " +
            className + "." + property + " (Setter: " + isSetter + ") does not exist!")
        {
        }
        
        public MethodBodyNotExistException(string className, string eventName, EventAccessorType eventType) : base("Method Body of event " +
            className + "." + eventName + " (EventType: " + eventType + ") does not exist!")
        {
        }
        
        
    }
}