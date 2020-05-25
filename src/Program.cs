using Statiq.App;
using Statiq.Common;
using Statiq.Web;
using Statiq.Images;
using System.Threading.Tasks;

namespace Blog
{
    class Program
    {
        private static async Task<int> Main(string[] args) =>
            await Bootstrapper
                .Factory
                .CreateWeb(args)
                .AddSetting(WebKeys.OptimizeContentFileNames, false)
                .AddSetting(WebKeys.GenerateSitemap, false)
                .BuildPipeline("ResizeTitleImages", builder => {
                    builder.WithInputReadFiles("assets/images/blog/*.{jpg,png,gif}");
                    builder.WithInputModules(new MutateImage()
                        // .Resize(600, 300).OutputAsJpeg()
                        // .And()
                        // .Resize(600, 450).OutputAsJpeg()
                        // .And()
                        // .Resize(800, 300).OutputAsJpeg()
                        // .And()
                        // .Resize(800, 600).OutputAsJpeg()
                        // .And()
                        // .Resize(960, 720).OutputAsJpeg()
                        // .And()
                        .Resize(960, 720).OutputAsJpeg()
                        .And()
                        .Resize(1920 , 1440).OutputAsJpeg()
                    );
                    builder.WithOutputWriteFiles();
                })
                .BuildPipeline("FormatContentImages", builder => {
                    builder.WithInputReadFiles("assets/images/blog/content/*.{jpg,png,gif}");
                    builder.WithInputModules(new MutateImage()
                        // .OutputAsJpeg()
                    );
                    builder.WithOutputWriteFiles();
                })
                .RunAsync();
    }
}
