using System;

namespace Soot.Dotnet.TestSnippets.IL
{
    public class LocAlloc
    {
        static void DoAlloc()
        {
            int length = 3;
            Span<int> numbers = stackalloc int[length];
            for (var i = 0; i < length; i++)
            {
                numbers[i] = i;
            }
        }
    }
}