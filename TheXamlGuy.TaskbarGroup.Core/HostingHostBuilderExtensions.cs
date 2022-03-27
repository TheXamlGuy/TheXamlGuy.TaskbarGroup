using Microsoft.Extensions.Hosting;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public static class HostingHostBuilderExtensions
    {
        public static IHostBuilder UseContentRoot(this IHostBuilder hostBuilder, string contentRoot, bool createDirectory)
        {
            if (!Directory.Exists(contentRoot) && createDirectory)
            {
                Directory.CreateDirectory(contentRoot);
            }

            return hostBuilder.UseContentRoot(contentRoot);
        }
    }
}
