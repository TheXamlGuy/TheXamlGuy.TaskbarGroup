using CommunityToolkit.Mvvm.ComponentModel;
using TheXamlGuy.TaskbarGroup.Core;

namespace TheXamlGuy.TaskbarGroup.Flyout
{
    public partial class TaskbarButtonGroupViewModel : ObservableCollectionViewModel<TaskbarButtonGroupItemViewModel>
    {
        [ObservableProperty]
        private string name = "hello";

        public TaskbarButtonGroupViewModel(IMessenger messenger,
            IServiceFactory serviceFactory,
            IMediator mediator,
            IDisposer disposer) : base(messenger, serviceFactory, disposer)
        {
            Register<FileDropped>(OnFileDropped);
            Mediator = mediator;
        }

        public IMediator Mediator { get; }

        private void OnFileDropped(FileDropped args)
        {
            Add();
        }
    }
}
