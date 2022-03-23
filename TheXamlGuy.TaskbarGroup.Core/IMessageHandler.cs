namespace TheXamlGuy.TaskbarGroup.Core
{
    public interface IMessageHandler<TMessage>
    {
        void Handle(TMessage message);
    }

    public interface IMessageHandler<TReturn, TMessage>
    {
        TReturn Handle(TMessage message);
    }
}
