using Statiq.App;
using Statiq.Common;
using Statiq.Web;
using Statiq.Images;
using System.Threading.Tasks;

namespace Blog
{
    public static class ConfigurationExtensions {
        private const string Subdomain = "www";
        private const string Domain = "developmomentum.com";

        public static Bootstrapper ConfigureLinks(this Bootstrapper bootstrapper) {
            return bootstrapper
                .AddSetting(SiteKeys.Subdomain, Subdomain)
                .AddSetting(SiteKeys.Domain, Domain)
                .AddSetting(Keys.Host, $"{Subdomain}.{Domain}")
                .AddSetting(Keys.LinksUseHttps, true)
                .AddSetting(Keys.LinkHideExtensions, false)
                .AddSetting(Keys.LinkHideIndexPages, false);
        }
    }
    
    class Program
    {
        private static async Task<int> Main(string[] args) =>
            await Bootstrapper
                .Factory
                .CreateWeb(args)
                .ConfigureLinks()
                .AddSetting(WebKeys.OptimizeContentFileNames, false)
                .AddSetting(WebKeys.GenerateSitemap, true)
                .AddSetting(WebKeys.GatherHeadingsLevel, 3)
                .BuildPipeline("ResizeTitleImages", builder => {
                    builder.WithInputReadFiles("assets/images/blog/*.{jpg,png,gif}");
                    builder.WithInputModules(new MutateImage()
                        .Resize(960, 720).OutputAsJpeg()
                        .And()
                        .Resize(1920 , 1440).OutputAsJpeg()
                    );
                    builder.WithOutputWriteFiles();
                })
                .BuildPipeline("FormatContentImages", builder => {
                    builder.WithInputReadFiles("assets/images/blog/content/*.{jpg,png,gif}");
                    builder.WithInputModules(new MutateImage());
                    builder.WithOutputWriteFiles();
                })
                .RunAsync();
    }
}
