using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public static class FileConfigurationExtensions
    {
        public static IConfigurationBuilder SetBasePath(this IConfigurationBuilder builder, 
            string basePath, bool createDirectory)
        {
            if (!Directory.Exists(basePath) && createDirectory)
            {
                Directory.CreateDirectory(basePath);
            }

            return builder.SetFileProvider(new PhysicalFileProvider(basePath));
        }
    }
}
