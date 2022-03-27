using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public static class WritableJsonConfigurationExtensions
    {
        public static IConfigurationBuilder AddWritableJsonFile(this IConfigurationBuilder builder, 
            string path)
        {
            return AddWritableJsonFile(builder, null, path, false, false, null);
        }
        public static IConfigurationBuilder AddWritableJsonFile(this IConfigurationBuilder builder, 
            string path, 
            Stream createFromStream)
        {
            return AddWritableJsonFile(builder, null, path, false, false, createFromStream);
        }

        public static IConfigurationBuilder AddWritableJsonFile(this IConfigurationBuilder builder,
            string path, 
            bool optional)
        {
            return AddWritableJsonFile(builder, null, path, optional, false, null);
        }

        public static IConfigurationBuilder AddWritableJsonFile(this IConfigurationBuilder builder,
            string path,
            bool optional,
            Stream createFromStream)
        {
            return AddWritableJsonFile(builder, null, path, optional, false, createFromStream);
        }

        public static IConfigurationBuilder AddWritableJsonFile(this IConfigurationBuilder builder, 
            string path,
            bool optional,
            bool reloadOnChange)
        {
            return AddWritableJsonFile(builder, null, path, optional, reloadOnChange, null);
        }

        public static IConfigurationBuilder AddWritableJsonFile(this IConfigurationBuilder builder,
            string path,
            bool optional,
            bool reloadOnChange,
            Stream createFromStream)
        {
            return AddWritableJsonFile(builder, null, path, optional, reloadOnChange, createFromStream);
        }

        public static IConfigurationBuilder AddWritableJsonFile(this IConfigurationBuilder builder,
            IFileProvider? provider,
            string path, 
            bool optional,
            bool reloadOnChange, Stream? createFromStream)
        {
            return builder.AddWritableJsonFile(configuration =>
            {
                configuration.FileProvider = provider;
                configuration.Path = path;
                configuration.Optional = optional;
                configuration.ReloadOnChange = reloadOnChange;
                configuration.CreateFromSteam = createFromStream;
                configuration.ResolveFileProvider();
            });
        }


        public static IConfigurationBuilder AddWritableJsonFile(this IConfigurationBuilder builder,
            Action<WritableJsonConfigurationSource> configureSource) => builder.Add(configureSource);
    }
}
