using System;
using System.Runtime.CompilerServices;

namespace Soot.Dotnet.TestSnippets
{
    public class Sync
    {
        public void S()
        {
            object lockk = new object();

            lock (lockk) 
            { 
                // do stuff
                Console.WriteLine("Lol");
            }
        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void S2()
        {
            Console.WriteLine("Lol2");
        }
    }
}