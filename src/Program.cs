using Statiq.App;
using Statiq.Web;
using System.Threading.Tasks;

namespace Blog
{
    class Program
    {
        private static async Task<int> Main(string[] args) =>
            await Bootstrapper
                .Factory
                .CreateWeb(args)
                .RunAsync();
    }
}
