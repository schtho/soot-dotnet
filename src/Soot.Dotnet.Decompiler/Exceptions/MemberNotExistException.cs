using System;

namespace Soot.Dotnet.Decompiler.Exceptions
{
    public class MemberNotExistException : Exception
    {
        public MemberNotExistException() : base("Member does not exist!") { }
        
        public MemberNotExistException(Member memberType, string memberName) 
            : base("Member (" + memberType + ") " + memberName + " does not exist!")
        {
        }
        
        public MemberNotExistException(Member memberType, string memberName, string declaringType) 
            : base("Member (" + memberType + ") " + memberName + " at Type " + declaringType + " does not exist!")
        {
        }
        
        public MemberNotExistException(Member memberType) 
            : base("Member (" + memberType + ") does not exist!")
        {
        }
        
        public enum Member
        {
            Method,
            Property,
            Event
        }
    }
}