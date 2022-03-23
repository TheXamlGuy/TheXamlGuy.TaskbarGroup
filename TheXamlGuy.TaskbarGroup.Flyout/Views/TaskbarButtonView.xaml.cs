using TheXamlGuy.TaskbarGroup.Core;

namespace TheXamlGuy.TaskbarGroup.Flyout
{
    public sealed partial class TaskbarButtonView : IBindViewModel<TaskbarButtonViewModel>
    {
        public TaskbarButtonView()
        {
            InitializeComponent();
        }

        public TaskbarButtonViewModel ViewModel { get; set; }
    }
}
