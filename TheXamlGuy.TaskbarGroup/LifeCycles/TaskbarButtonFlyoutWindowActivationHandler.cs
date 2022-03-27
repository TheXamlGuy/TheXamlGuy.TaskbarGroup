using TheXamlGuy.TaskbarGroup.Core;

namespace TheXamlGuy.TaskbarGroup
{
    public class TaskbarButtonFlyoutWindowActivationHandler : IMessageHandler<TaskbarButtonFlyoutWindowActivation>
    {
        private readonly TaskbarButtonFlyoutWindow flyoutWindow;

        public TaskbarButtonFlyoutWindowActivationHandler(TaskbarButtonFlyoutWindow flyoutWindow)
        {
            this.flyoutWindow = flyoutWindow;
        }

        public void Handle(TaskbarButtonFlyoutWindowActivation message)
        {
            flyoutWindow.Show();
        }
    }
}
