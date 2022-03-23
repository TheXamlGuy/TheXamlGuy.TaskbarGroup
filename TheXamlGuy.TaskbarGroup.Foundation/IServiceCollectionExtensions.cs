using Microsoft.Extensions.DependencyInjection;
using TheXamlGuy.TaskbarGroup.Core;

namespace TheXamlGuy.TaskbarGroup.Foundation
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRequiredFoundation(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<TemplateSelector>()
                .AddSingleton<IDataTemplateCollection>(new DataTemplateCollection(new Dictionary<Type, Type>()))
                .AddSingleton<DataTemplateFactory>()
                .AddSingleton<IDispatcherTimerFactory, DispatcherTimerFactory>()
                .AddTransient<FileDropTarget>();
        }
    }
}
