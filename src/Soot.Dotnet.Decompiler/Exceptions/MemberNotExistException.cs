using System;

namespace Soot.Dotnet.Decompiler.Exceptions
{
    public class MemberNotExistException : Exception
    {
        public MemberNotExistException() : base("Member does not exist!") { }
        
        public MemberNotExistException(Member memberType, string memberName) 
            : base(memberType + " " + memberName + " does not exist!")
        {
        }
        
        public MemberNotExistException(Member memberType, string memberName, string declaringType) 
            : base(memberType + " " + memberName + " at Type " + declaringType + " does not exist!")
        {
        }
        
        public MemberNotExistException(Member memberType, string memberName, int peToken, string declaringType) 
            : base(memberType + " " + memberName + " (PE Token: 0x" + peToken.ToString("x8") + ") at Type " + declaringType + " does not exist!")
        {
        }
        
        public MemberNotExistException(Member memberType) 
            : base(memberType + " does not exist!")
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