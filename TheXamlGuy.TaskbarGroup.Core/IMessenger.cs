using System.Reactive.Concurrency;
using System.Reactive.Subjects;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public interface IMessenger
    {
        void Send<TMessage>() where TMessage : new();

        void Send<TMessage>(TMessage message);

        IDisposable Subscribe<TMessage>(Action<TMessage> actionDelegate, IScheduler? scheduler = null, Func<TMessage, bool>? where = null);
    }
}