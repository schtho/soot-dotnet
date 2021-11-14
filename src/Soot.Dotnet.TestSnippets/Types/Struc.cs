using System;

namespace Soot.Dotnet.TestSnippets.Types
{
    /// <summary>
    /// structs
    /// </summary>
    public class Struc
    {
        public static void DoSth(string str)
        {
            S s = default;
            S s2 = new S();
            S s3 = new S(5);
            DoSthStruct(ref s, s2);
            S s4;
            s4.Test = 2;
        }
        
        public struct S
        {
            public S(int i)
            {
                Test = 0;
            }

            public int Test;
        }
        
        public static void DoSthStruct(ref S s, S s2)
        {
            Console.WriteLine(s);
            Console.WriteLine(s2);
        }

        static void DoSth2()
        {
            Console.WriteLine("Hello World!");
            MyCoordinates s = default;
            s.X = 2;
        }
    
        public struct MyCoordinates
        {
            public double X { get; set; }
            public double Y { get; set; }
        }
        
    }
}