using System.Threading.Tasks;

namespace Soot.Dotnet.TestSnippets
{
    public class Asyncc
    {
        public async void StartAsync()
        {
            // var lel = await DoAsync();
            await DoSomething();
        }

        public async Task DoSomething()
        {
            await Task.Delay(100);
        }
    }
}