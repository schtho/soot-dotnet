namespace Soot.Dotnet.TestSnippets.Types
{
    public class Interfac : IEinInter
    {
        public string EineMethode()
        {
            return "Hi";
        }
    }

    public interface IEinInter
    {
        public string EineMethode();
    }

    public class DoInter
    {
        public static void Action()
        {
            IEinInter i = new Interfac();
            var x = i.EineMethode();
        }
    }
}