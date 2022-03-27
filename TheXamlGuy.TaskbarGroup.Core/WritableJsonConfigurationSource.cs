using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public class WritableJsonConfigurationSource : JsonConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaultsWithSteam(builder);
            return new WritableJsonConfigurationProvider(this);
        }

        private void EnsureDefaultsWithSteam(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);

            if (FileProvider is PhysicalFileProvider physicalFileProvider)
            {
                var outputFile = System.IO.Path.Combine(physicalFileProvider.Root, Path);
                if (!File.Exists(outputFile) && CreateFromSteam is not null)
                {
                    using var fileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write);
                    CreateFromSteam.CopyTo(fileStream);
                }
            }
        }

        public Stream? CreateFromSteam { get; set; }
    }
}
