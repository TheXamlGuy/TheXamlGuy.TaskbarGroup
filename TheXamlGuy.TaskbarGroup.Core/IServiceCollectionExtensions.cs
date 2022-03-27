using Microsoft.Extensions.DependencyInjection;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public static class PointerLocationExtensions
    {
        public static bool IsWithinBounds(this PointerLocation args, Rect bounds)
        {
            if (args.X >= bounds.X
                && args.X <= bounds.X + bounds.Width
                && args.Y >= bounds.Y
                && args.Y <= bounds.Y + bounds.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAsyncHandler<TAsyncMessageHandle>(this IServiceCollection serviceCollection)
            where TAsyncMessageHandle : IAsyncMessageHandler
        {
            if (typeof(TAsyncMessageHandle).GetInterfaces().FirstOrDefault(x =>
                x.GetGenericTypeDefinition() == typeof(IAsyncMessageHandler<>)
                || x.GetGenericTypeDefinition() == typeof(IAsyncMessageHandler<>)) is { } messageHandler)
            {
                var messageArguments = messageHandler.GetGenericArguments();
                if (messageArguments is { Length: 1 })
                {
                    serviceCollection.AddTransient(typeof(IAsyncMessageHandler<>).MakeGenericType(messageArguments[0]), typeof(TAsyncMessageHandle));
                }
                else
                {
                    serviceCollection.AddTransient(typeof(IAsyncMessageHandler<,>).MakeGenericType(messageArguments[0], messageArguments[1]), typeof(TAsyncMessageHandle));
                }
            }

            return serviceCollection;
        }

        public static IServiceCollection AddHandler<TMessageHandle>(this IServiceCollection serviceCollection)
            where TMessageHandle : IMessageHandler
        {
            if (typeof(TMessageHandle).GetInterfaces().FirstOrDefault(x =>
                x.GetGenericTypeDefinition() == typeof(IMessageHandler<>)
                || x.GetGenericTypeDefinition() == typeof(IMessageHandler<,>)) is { } messageHandler)
            {
                var messageArguments = messageHandler.GetGenericArguments();

                if (messageArguments is { Length: 1 })
                {
                    serviceCollection.AddTransient(typeof(IMessageHandler<>).MakeGenericType(messageArguments[0]), typeof(TMessageHandle));
                }
                else
                {
                    serviceCollection.AddTransient(typeof(IMessageHandler<,>).MakeGenericType(messageArguments[0], messageArguments[1]), typeof(TMessageHandle));
                }
            }

            return serviceCollection;
        }

        public static IServiceCollection AddRequiredCore(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<IDisposer, Disposer>()
                .AddSingleton<IServiceFactory>(provider => new ServiceFactory(provider.GetService, (type, parameter) => ActivatorUtilities.CreateInstance(provider, type, parameter)))
                .AddSingleton<IMessageInvoker, MessageInvoker>()
                .AddSingleton<IMessenger, Messenger>()
                .AddSingleton<IMediator, Mediator>()
                .AddSingleton<IWndProcMonitor, WndProcMonitor>()
                .AddSingleton<ITaskbar, Taskbar>()
                .AddSingleton<ITaskbarList, TaskbarList>()
                .AddSingleton<IPointerMonitor, PointerMonitor>()
                .AddSingleton<ITaskbarButtonMonitor, TaskbarButtonMonitor>()
                .AddSingleton<ITaskbarButtonShortcutMonitor, TaskbarButtonShortcutMonitor>();
        }
    }
}
