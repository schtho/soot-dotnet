using System;

namespace Soot.Dotnet.TestSnippets.Members
{
    /// <summary>
    /// Produce IL Code with delegates
    /// </summary>
    public class Delegat
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

    public delegate void MyDelegate(string s);
}