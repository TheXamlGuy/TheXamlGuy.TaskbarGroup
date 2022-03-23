using System.Collections.ObjectModel;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public class ObservableCollectionViewModel<TItemViewModel> :
          ObservableCollection<TItemViewModel>, IDisposable
          where TItemViewModel : class
    {
        private readonly IDisposer disposer;
        private readonly IMessenger messenger;
        private readonly IServiceFactory serviceFactory;

        public ObservableCollectionViewModel(IMessenger messenger,
            IServiceFactory serviceFactory,
            IDisposer disposer)
        {
            this.messenger = messenger;
            this.serviceFactory = serviceFactory;
            this.disposer = disposer;
        }

        public bool IsInitialized { get; protected set; }

        public void Add()
        {
            var item = serviceFactory.Create<TItemViewModel>();
            disposer.Add(this, item);

            base.Add(item);
        }

        public void Add(object parameter, params object[] parameters)
        {
            var item = serviceFactory.Create<TItemViewModel>(new[] { parameter }.Concat(parameters));
            disposer.Add(this, item);

            base.Add(item);
        }

        public new void Clear()
        {
            foreach (var item in this)
            {
                disposer.Dispose(item);
            }

            base.Clear();
        }

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

        public void Insert<TItem>(params object[] parameters) where TItem : TItemViewModel
        {
            var item = serviceFactory.Create<TItem>(parameters);
            disposer.Add(this, item);

            base.Add(item);
        }

        public new void Insert(int index, TItemViewModel item)
        {
            disposer.Add(this, item);
            base.Insert(index, item);
        }

        public void Insert<TItem>(int index, params object[] parameters) where TItem : TItemViewModel
        {
            var item = serviceFactory.Create<TItem>(parameters);
            disposer.Add(this, item);

            base.Insert(index, item);
        }

        public void Insert(TItemViewModel item)
        {
            base.Insert(0, item);
            disposer.Add(item);
        }

        public new void Remove(TItemViewModel item)
        {
            disposer.Dispose(item);
            base.Remove(item);
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
