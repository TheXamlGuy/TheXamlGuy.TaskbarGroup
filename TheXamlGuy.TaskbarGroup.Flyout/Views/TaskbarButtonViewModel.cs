using TheXamlGuy.TaskbarGroup.Core;
using TheXamlGuy.TaskbarGroup.Flyout.Foundation;

namespace TheXamlGuy.TaskbarGroup.Flyout
{
    public class TaskbarButtonViewModel : ObservableViewModel
    {
        public TaskbarButtonViewModel(IMessenger messenger,
            IServiceFactory serviceFactory,
            IDisposer disposer,
            TemplateSelector templateSelector,
            TaskbarButtonGroupViewModel taskbarButtonGroupViewModel) : base(messenger, serviceFactory, disposer)
        {
            TemplateSelector = templateSelector;
            TaskbarButtonGroupViewModel = taskbarButtonGroupViewModel;
        }
        
        public TemplateSelector TemplateSelector { get; }

        public TaskbarButtonGroupViewModel TaskbarButtonGroupViewModel { get; }
    }
}
