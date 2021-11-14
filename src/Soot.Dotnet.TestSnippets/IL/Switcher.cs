using System;

namespace Soot.Dotnet.TestSnippets.IL
{
    /// <summary>
    /// Switch Instructions
    /// </summary>
    public class Switcher
    {
        public void DoSw(string s)
        {
            bool lel = true;
            if (lel)
            {
                int x = 0;
                Console.WriteLine(x);
            }
                
            switch (s)
            {
                case "lel": Console.WriteLine("jo1");
                    break;
                case "lol": Console.WriteLine("lol");
                    break;
                default:
                    Console.WriteLine("no");
                    break;
            }
        }

        public void SwInt(int i)
        {
            bool? lol;
            switch (i)
            {
                case 11:
                    Console.WriteLine(10.0);
                    // Initobj
                    lol = null;
                    break;
                case 21:
                    Console.WriteLine("line");
                    // ldloca
                    lol = true;
                    if (lol.Value)
                        Console.WriteLine("");
                    break;
            }

            float x = i;

        }

        public void SwIntE(En e)
        {
            switch (e)
            {
                case En.Das:
                    int x = 0;
                    break;
                case En.Dies:
                    int y = 1;
                    break;
            }
        }

        // SwitchInstruction OpCode
        public void SwInstOpCode(int x)
        {
            switch (x)
            {
                case 0:
                    Console.WriteLine("line 0");
                    return;
                case 1:
                {
                    Console.WriteLine("line 1");
                    return;
                }
                /*case 2:
                {
                    Console.WriteLine("line 2");
                    return;
                }*/
                case 3:
                {
                    Console.WriteLine("line 3");
                    return;
                }
                default:
                {
                    Console.WriteLine("line d");
                    return;
                }
            }
        }

        public enum En
        {
            Dies,
            Das
        }
    }
}