using System;
using System.Collections.Generic;
using System.Linq;

namespace Soot.Dotnet.TestSnippets.Members
{
    /// <summary>
    /// different types of methods such as anonymous functions
    /// </summary>
    public class Methods
    {
        public void Anonym()
        {
            var lst = new List<int>{1,23,45432,432,123};
            var lst2 = lst.Select(x => x++);
        }

        // void Finalize() not possible!
        public void finalize()
        {
            
        }

        public unsafe void MyUnsafeMethod()
        {
            int i = 0;
            int* p = &i;
        }

        ~Methods()
        {
            Console.WriteLine("Ciao");
        }
    }
}