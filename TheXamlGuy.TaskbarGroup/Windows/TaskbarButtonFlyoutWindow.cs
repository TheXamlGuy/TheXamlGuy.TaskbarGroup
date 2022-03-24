using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using TheXamlGuy.TaskbarGroup.Flyout.Controls;

namespace TheXamlGuy.TaskbarGroup
{
    public class TaskbarButtonFlyoutWindow : TransparentXamlWindow<TaskbarButtonFlyout>
    {
        public TaskbarButtonFlyoutWindow()
        {
            Deactivated += OnDeactivated;
            Topmost = true;
            Width = 258;
            Height = 258;

            Dispatcher.BeginInvoke(new Action(() =>
            {
                Hide();
            }), DispatcherPriority.ContextIdle, null);
        }

        private void OnDeactivated(object? sender, EventArgs args)
        {
            if (XamlContent is TaskbarButtonFlyout flyout)
            {
                flyout.Close();
                Hide();
            }
        }
    }
}
