using System;
using System.Collections.Generic;

namespace Soot.Dotnet.TestSnippets.IL
{
    public class Try
    {
        public int TryCatch(int[] a, bool b, string c)
        {
            try
            {
                Console.WriteLine("Alles gut");
                return 123;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Arg Null Fehler");
            }
            catch (ArgumentException e)
            {
                // Console.WriteLine(e);
                Console.WriteLine("Argument Fehler");
                // throw new AggregateException();
                throw new Exception();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ein Fehler e");
                // rethrow
                throw;
            }
            finally
            {
                Console.WriteLine("final countdown");
            }

            return 0;
        }

        public void TryFilter()
        {
            try
            {
                Console.WriteLine("Yey");
            }
            catch (System.Runtime.InteropServices.COMException ex) when ((uint)ex.ErrorCode == 0x80070005)
            {
                Console.WriteLine("Zugang verboten!");
            }
        }
        
        public void TryFilterExtended()
        {
            try
            {
                Console.WriteLine("Yey");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Arg Fehler");
            }
            catch (System.Runtime.InteropServices.COMException ex) when ((uint) ex.ErrorCode == 0x80070005)
            {
                Console.WriteLine("Zugang verboten!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Sonstige Fehler e" + e.Message);
            }
        }

        // ("ConsoleApp1InnerClass.Try+<TryFault>d__0");
        /// ("MoveNext");
        static IEnumerable<string> TryFault(string fileName)
        {
            using (var file = System.IO.File.OpenText(fileName))
            {
                string s;
                while ((s = file.ReadLine()) != null)
                {
                    yield return s;
                }
            }
        }
        
        public void TryCatch(int i) 
        {
            try 
            {
                i = 2;
            } 
            catch (SystemException e) 
            {
                i = 3;
            } 
            finally 
            {
                i = 4;
            }
        }
    }
}