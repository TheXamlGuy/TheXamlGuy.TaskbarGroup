using Microsoft.Extensions.DependencyInjection;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRequiredCore(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<IDisposer, Disposer>()
                .AddSingleton<IServiceFactory>(provider => new ServiceFactory(provider.GetService, (type, parameter) => ActivatorUtilities.CreateInstance(provider, type, parameter)))
                .AddSingleton<IEventAggregatorInvoker, EventAggregatorInvoker>()
                .AddSingleton<IMessenger, Messenger>()
                .AddSingleton<IMediator, Mediator>()
                .AddSingleton<IInitializable, WndProcMonitor>()
                .AddSingleton<ITaskbar, Taskbar>()
                .AddSingleton<IInitializable, TaskbarMonitor>()
                .AddSingleton<IInitializable, PointerMonitor>()
                .AddSingleton<IInitializable, TaskbarButtonMonitor>();
        }
    }
}
