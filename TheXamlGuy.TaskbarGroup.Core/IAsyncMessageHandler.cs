namespace TheXamlGuy.TaskbarGroup.Core
{
    public interface IAsyncMessageHandler
    {

    }

    public interface IAsyncMessageHandler<TMessage> : IAsyncMessageHandler
    {
        Task Handle(TMessage message, CancellationToken canellationToken = default);
    }

    public interface IAsyncMessageHandler<TReturn, TMessage> : IAsyncMessageHandler
    {
        Task<TReturn> Handle(TMessage message, CancellationToken cancellationToken = default);
    }
}
