namespace Soot.Dotnet.TestSnippets
{
    /// <summary>
    /// typical Jimple example
    /// </summary>
    public class Jimple
    {
        internal static int Factorial(int x)
        {
            int result = 1;
            int i = 2;
            while (i <= x)
            {
                result *= i;
                i++;
            }
            return result;
        }
    }
}