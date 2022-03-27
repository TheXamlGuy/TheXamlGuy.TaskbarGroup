using System;
using System.Diagnostics;
using System.Windows.Threading;
using TheXamlGuy.TaskbarGroup.Core;
using TheXamlGuy.TaskbarGroup.Flyout.Controls;

namespace TheXamlGuy.TaskbarGroup
{
    public class TaskbarButtonFlyoutWindow : TransparentXamlWindow<TaskbarButtonFlyout>
    {
        private readonly IMediator mediator;

        public TaskbarButtonFlyoutWindow(IMessenger messenger,
            IMediator mediator)
        {
            this.mediator = mediator;

            Topmost = true;
            Width = 258;
            Height = 258;

            Dispatcher.BeginInvoke(new Action(() =>
            {
                Hide();
            }), DispatcherPriority.ContextIdle, null);

            messenger.Subscribe<TaskbarDragEnter>(OnTaskbarDragEnter);
            messenger.Subscribe<TaskbarButtonInvoked>(OnTaskbarButtonInvoked);
            messenger.Subscribe<TaskbarButtonDragEnter>(OnTaskbarButtonDragEnter);
        }

        private void OnTaskbarDragEnter(TaskbarDragEnter obj)
        {
            Debug.WriteLine("fff");
        }

        protected override void OnDeactivated(EventArgs args)
        {
            if (XamlContent is TaskbarButtonFlyout flyout)
            {
                flyout.Close();
                Hide();
            }

            base.OnDeactivated(args);
        }

        private void Open(TaskbarButton button)
        {
            mediator.Handle(new TaskbarButtonFlyoutActivation(button));
        }

        private void OnTaskbarButtonDragEnter(TaskbarButtonDragEnter args)
        {
            Dispatcher.Invoke(() => Open(args.Button));
        }

        private void OnTaskbarButtonInvoked(TaskbarButtonInvoked args)
        {
            Dispatcher.Invoke(() => Open(args.Button));
        }
    }
}
