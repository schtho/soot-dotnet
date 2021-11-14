using System;
using System.Runtime.CompilerServices;

namespace Soot.Dotnet.TestSnippets.Types
{
    /// <summary>
    /// Types such as int or arrays
    /// </summary>
    public class Typ
    {

        public void DoArr(in string[] s)
        {
            var array = new int[] { 1, 3, 5, 6 };
            var elem = array[3];
        }

        public void DoMultArr(ref string[,,] strings)
        {
            // ldtoken - dup only in CIL, not ILspy
            // ldtoken - stloc fldHandle(ldmembertoken 8B4B2444E57AED8C2D05A1293255DA1B048C63224317D4666230760935FA4A18)
            var array2D = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
            var elem = array2D[0,1];

            var arr1 = new[] {1, 2, 3, 4};
            var s = Unsafe.SizeOf<Enumm.MyCustomAttribute>();

            int[][] arr3 = new int[10][];
            var elem3 = arr3[1][1];
        }

        public void DoTuple()
        {
            Tuple<string, int> tup = new Tuple<string, int>("yo", 1);
            var (t1, t2) = tup;
            t1 = "y";
        }
        
        public void Arr()
        {
            var l = new int[20];
            var k = l.Length;
            l[11] = 34;
            k = l[13];
        }

        public static int StatischesInt;

    }
    
    
    
}