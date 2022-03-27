using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public class WritableJsonConfigurationProvider : JsonConfigurationProvider
    {
        private readonly WritableJsonConfigurationFile writableJsonConfigurationFile;

        public WritableJsonConfigurationProvider(JsonConfigurationSource source) : base(source)
        {
            writableJsonConfigurationFile = new WritableJsonConfigurationFile();
        }

        public override void Load(Stream stream)
        {
            Data = writableJsonConfigurationFile.Parse(stream);
        }

        public override void Set(string key, string value)
        {
            var file = Source.FileProvider?.GetFileInfo(Source.Path ?? string.Empty);
            static Stream OpenRead(IFileInfo fileInfo)
            {
                if (fileInfo.PhysicalPath is not null)
                {
                    return new FileStream(fileInfo.PhysicalPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                }

                return fileInfo.CreateReadStream();
            }

            using Stream stream = OpenRead(file);
            writableJsonConfigurationFile.Write(key, value, stream);
        }
    }
}
