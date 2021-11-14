using System;
using System.Runtime.InteropServices;
using Soot.Dotnet.TestSnippets.Types;

namespace Soot.Dotnet.TestSnippets.SubPart
{
    public class SubClass
    {
        public SubClass()
        {
            _i = 0;
        }
        
        public void Con()
        {
            _i = 0;
        }
        
        public string EinString;
        
        public string EinStringParam { get; set; }

        private int _i;

        /*public static void BinStat()
        {
            Console.WriteLine("haha statisch");
        }*/

        public int GetI()
        {
            Typ.StatischesInt = 234;
            return _i;
        }
        
        public int Z { get; set; }

        private NestedClass _l;

        public void Yo()
        {
            var l = new NestedClass(5, 90);
            _i = l.Calcen(1,2);
            Z = NestedClass.CalcenStatic(16);
            _l = new NestedClass(7, 4);
            _i = _l.Calc();
            _l.Calc();
            
            StaticConstr.MyFunc();
        }

        [StructLayout(LayoutKind.Sequential)]
        private class NestedClass
        {
            public NestedClass(int a, int b)
            {
                _a = a;
                _b = b;
            }

            static NestedClass()
            {
                Console.WriteLine("Hi static cctor");
            }

            private int _a;
            private readonly int _b;

            public int Calc()
            {
                int c = _a + _b;
                c += 40;
                return c;
            }

            public int Calcen(int zahl1, int zahl2)
            {
                return zahl1 + zahl2 + 10;
            }

            public static int CalcenStatic(int zahl3)
            {
                return zahl3 / 4;
            }

            [Obsolete(error: true, message: "ist halt so")]
            public void Alt(int z)
            {
                _a = z;
            }
        }

        public class StaticConstr
        {
            static StaticConstr()
            {
                Console.WriteLine("Hi static cctor");
            }

            public static void MyFunc()
            {
                Console.WriteLine("Yo Function");
            }
        }
    }
}