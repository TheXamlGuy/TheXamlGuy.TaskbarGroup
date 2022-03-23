namespace TheXamlGuy.TaskbarGroup.Core
{
    public interface IAsyncMessageHandler<TMessage>
    {
        Task Handle(TMessage message, CancellationToken canellationToken = default);
    }

    public interface IAsyncMessageHandler<TReturn, TMessage>
    {
        Task<TReturn> Handle(TMessage message, CancellationToken cancellationToken = default);
    }
}
