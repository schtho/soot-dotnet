namespace Soot.Dotnet.TestSnippets
{
    /// <summary>
    /// Standard stuff such as "if"
    /// </summary>
    public class Standards
    {
        private static void If(int x)
        {
            int number = 0;
            if (x == 3)
                number = 5;
            else
                number = 10;
        }
        
        public static int MyProp { get; set; }
    }
}