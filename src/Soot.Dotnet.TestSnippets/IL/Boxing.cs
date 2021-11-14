namespace Soot.Dotnet.TestSnippets.IL
{
    public class Boxing
    {
        public void Obj()
        {
            int test = 123;
            object n = test;
            int t3 = (int) n;
            
            object lol = "123432";
            var b = lol is string;
        }
    }
}