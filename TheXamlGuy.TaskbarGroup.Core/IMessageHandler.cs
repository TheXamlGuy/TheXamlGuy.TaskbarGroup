namespace TheXamlGuy.TaskbarGroup.Core
{
    public interface IMessageHandler
    {

    }
    public interface IMessageHandler<TMessage> : IMessageHandler
    {
        void Handle(TMessage message);
    }

    public interface IMessageHandler<TReturn, TMessage> : IMessageHandler
    {
        TReturn Handle(TMessage message);
    }
}
