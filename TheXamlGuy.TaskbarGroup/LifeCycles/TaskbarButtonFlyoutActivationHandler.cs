using TheXamlGuy.TaskbarGroup.Core;
using TheXamlGuy.TaskbarGroup.Flyout;
using TheXamlGuy.TaskbarGroup.Flyout.Controls;
using TheXamlGuy.TaskbarGroup.Flyout.Foundation;
using TheXamlGuy.TaskbarGroup.Foundation;

namespace TheXamlGuy.TaskbarGroup
{
    public class TaskbarButtonFlyoutActivationHandler : IMessageHandler<TaskbarButtonFlyoutActivation>
    {
        private readonly TaskbarButtonFlyoutWindow window;
        private readonly TaskbarButtonViewModel taskbarButtonViewModel;
        private readonly TaskbarButtonView taskbarButtonView;
        private readonly ITaskbar taskbar;

        public TaskbarButtonFlyoutActivationHandler(ITaskbar taskbar,
            TaskbarButtonViewModel taskbarButtonViewModel,
            TaskbarButtonView taskbarButtonView,
            TaskbarButtonFlyoutWindow window)
        {
            this.taskbar = taskbar;
            this.taskbarButtonViewModel = taskbarButtonViewModel;
            this.taskbarButtonView = taskbarButtonView;
            this.window = window;
        }

        public void Handle(TaskbarButtonFlyoutActivation message)
        {
            var button = message.Button;
            var dpiX = window.DpiX();
            var dpiY = window.DpiY();

            var taskbarState = taskbar.GetCurrentState();

            var placement = TaskbarButtonFlyoutPlacement.Bottom;
            switch (taskbarState.Placement)
            {
                case TaskbarPlacement.Left:
                    placement = TaskbarButtonFlyoutPlacement.Left;
                    break;

                case TaskbarPlacement.Top:
                    placement = TaskbarButtonFlyoutPlacement.Top;
                    break;

                case TaskbarPlacement.Right:
                    placement = TaskbarButtonFlyoutPlacement.Right;
                    break;

                case TaskbarPlacement.Bottom:
                    placement = TaskbarButtonFlyoutPlacement.Bottom;
                    window.Left = ((button.Bounds.X + (button.Bounds.Width / 2)) / dpiX) - (window.Width / 2);
                    window.Top = (button.Bounds.Y / dpiY) - window.Height;
                    break;
            }

            if (window.XamlContent is TaskbarButtonFlyout flyout)
            {
                flyout.Margin = new Windows.UI.Xaml.Thickness(6);

                taskbarButtonView.Bind(taskbarButtonViewModel);
                flyout.Content = taskbarButtonView;

                flyout.ShowAt(placement);
            }

            window.Show();
            window.Activate();
        }
    }
}
