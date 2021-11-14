using System;
using Soot.Dotnet.TestSnippets.IL;
using Soot.Dotnet.TestSnippets.Types;

namespace Soot.Dotnet.TestSnippets
{
    /// <summary>
    /// This project is to produce different types of IL Code parts or ILSpy AST
    /// Can be analyzed by a compiler
    /// </summary>
    internal static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var t = new Try();
            // t.TryCatch();
            Inherit.inh();
        }
    }
}