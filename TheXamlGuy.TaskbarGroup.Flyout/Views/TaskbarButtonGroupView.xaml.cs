using TheXamlGuy.TaskbarGroup.Core;
using TheXamlGuy.TaskbarGroup.Flyout.Foundation;

namespace TheXamlGuy.TaskbarGroup.Flyout
{
    public sealed partial class TaskbarButtonGroupView : IBindViewModel<TaskbarButtonGroupViewModel>
    {
        public TaskbarButtonGroupView()
        {
            InitializeComponent();
            this.Bind();
        }

        public TaskbarButtonGroupViewModel ViewModel { get; set; }
    }
}
