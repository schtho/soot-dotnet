namespace Soot.Dotnet.TestSnippets
{
    public class CallByRefVal
    {
        public void MyFunction(ref int x)
        {
            // do sth
        }

        public void MyFunction(int x)
        {
            // do sth
        }
        
        public string DoRefOutString(out string s)
        {
            s = null;
            return s;
        }
    }
}