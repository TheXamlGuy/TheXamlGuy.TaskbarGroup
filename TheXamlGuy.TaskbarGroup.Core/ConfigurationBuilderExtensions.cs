using Microsoft.Extensions.Configuration;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddWritableConfiguration(this IConfigurationBuilder builder)
        {
            return builder.Add(new WritableJsonConfigurationSource());
        }
    }
}
