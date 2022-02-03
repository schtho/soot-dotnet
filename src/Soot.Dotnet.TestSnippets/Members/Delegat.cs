using System;

namespace Soot.Dotnet.TestSnippets.Members
{
    public delegate void MyDelegate(string s);
    
    /// <summary>
    /// Produce IL Code with delegates
    /// </summary>
    public class MyDelegateInvoke
    {
        public static void DoSth()
        {
            var md = new MyDelegate(MyFunction);
            md("Test");
        }
        
        public static void MyFunction(string s)
        {
            Console.WriteLine(s);
        }
    }
}