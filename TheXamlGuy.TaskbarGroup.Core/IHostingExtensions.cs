using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public static class IHostingExtensions
    {
        public static IHostBuilder ConfigureDataTemplates(this IHostBuilder hostBuilder, Action<IDataTemplateBuilder> builderDelegate)
        {
            hostBuilder.ConfigureServices((hostBuilderContext, serviceCollection) =>
            {
                var builder = new DataTemplateBuilder();
                builderDelegate?.Invoke(builder);

                serviceCollection.AddSingleton(builder.DataTemplates);
            });

            return hostBuilder;
        }
    }
}
