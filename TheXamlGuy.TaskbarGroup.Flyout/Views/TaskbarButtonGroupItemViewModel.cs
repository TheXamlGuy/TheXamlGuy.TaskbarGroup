using TheXamlGuy.TaskbarGroup.Core;

namespace TheXamlGuy.TaskbarGroup.Flyout
{
    public class TaskbarButtonGroupItemViewModel : ObservableViewModel
    {
        public TaskbarButtonGroupItemViewModel(IMessenger messenger,
            IServiceFactory serviceFactory,
            IDisposer disposer) : base(messenger, serviceFactory, disposer)
        {
        }

        public string Name { get; set; }
    }
}
