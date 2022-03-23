using CommunityToolkit.Mvvm.ComponentModel;
using TheXamlGuy.TaskbarGroup.Core;

namespace TheXamlGuy.TaskbarGroup.Flyout
{
    public partial class TaskbarButtonGroupViewModel : ObservableCollectionViewModel<TaskbarButtonGroupItemViewModel>
    {
        public TaskbarButtonGroupViewModel(IMessenger messenger,
            IServiceFactory serviceFactory,
            IDisposer disposer) : base(messenger, serviceFactory, disposer)
        {
            Register<FileDropped>(OnFileDropped);
        }

        [ObservableProperty]
        private string name = "hello";

        private void OnFileDropped(FileDropped args)
        {
            Add();
        }
    }
}
