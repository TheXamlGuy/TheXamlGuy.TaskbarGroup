using CommunityToolkit.Mvvm.ComponentModel;

namespace TheXamlGuy.TaskbarGroup.Core
{
    [INotifyPropertyChanged]
    public partial class ObservableViewModel : IDisposable
    {
        private readonly IDisposer disposer;
        private readonly IMessenger messenger;

        public ObservableViewModel(IMessenger messenger,
            IServiceFactory serviceFactory,
            IDisposer disposer)
        {
            this.messenger = messenger;
            this.disposer = disposer;
        }

        public bool IsInitialized { get; protected set; }

        public void Dispose()
        {
            OnDisposing();

            disposer.Dispose(this);
            GC.SuppressFinalize(this);
        }

        public void Initialize()
        {
            if (IsInitialized)
            {
                return;
            }

            IsInitialized = true;
            OnInitialize();
        }
        protected virtual void OnDisposing()
        {
        }

        protected virtual void OnInitialize()
        {

        }

        protected void Publish<TEvent>(TEvent @event)
        {
            messenger.Send(@event);
        }

        protected void Publish<TEvent>() where TEvent : new()
        {
            messenger.Send<TEvent>();
        }

        protected void Register<TEvent>(Action<TEvent> action)
        {
            disposer.Add(this, messenger.Subscribe(action));
        }

        protected void Register<TEvent>(Action<TEvent> action, Func<TEvent, bool> condition)
        {
            disposer.Add(this, messenger.Subscribe(action, null, condition));
        }
    }
}
