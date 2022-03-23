using System.Reactive.Concurrency;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public class Messenger : IMessenger
    {
        public IEventAggregatorInvoker invoker;
        private readonly ConcurrentDictionary<Type, object> subjects = new();

        private IScheduler dispatcher;

        public Messenger(IEventAggregatorInvoker invoker)
        {
            var synchronizationContext = SynchronizationContext.Current;
            if (synchronizationContext is null) throw new NullReferenceException(nameof(synchronizationContext));

            this.invoker = invoker;

            dispatcher = new SynchronizationContextScheduler(synchronizationContext);
        }
        public ISubject<TMessage> GetSubject<TMessage>()
        {
            return (ISubject<TMessage>)subjects.GetOrAdd(typeof(TMessage), type => new BehaviorSubject<TMessage>(default));
        }

        public void Send<TMessage>() where TMessage : new()
        {
            Send(new TMessage());
        }

        public void Send<TMessage>(TMessage message)
        {
            GetSubject<TMessage>().OnNext(message);
        }

        public IDisposable Subscribe<TMessage>(Action<TMessage> actionDelegate, IScheduler? scheduler = null, Func<TMessage, bool>? where = null)
        {
            if (scheduler is null)
            {
                scheduler = Scheduler.Default;
            }

            if (where == null)
            {
                where = x => true;
            }

            return GetSubject<TMessage>().AsObservable().Skip(1).Where(where).ObserveOn(scheduler).WeakSubscribe(invoker, actionDelegate);
        }
    }
}
