using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using TheXamlGuy.TaskbarGroup.Core;
using TheXamlGuy.TaskbarGroup.Flyout.Controls;

namespace TheXamlGuy.TaskbarGroup
{
    public class TaskbarButtonFlyoutWindow : TransparentXamlWindow<TaskbarButtonFlyout>
    {
        private readonly IMediator mediator;
        private bool isDpiChanging;

        public TaskbarButtonFlyoutWindow(IMessenger messenger,
            IMediator mediator)
        {
            this.mediator = mediator;

            DpiChanged += OnDpiChanged;
            Deactivated += OnDeactivated;
            Topmost = true;
            Width = 258;
            Height = 258;

            messenger.Subscribe<TaskbarButtonInvoked>(OnTaskbarButtonInvoked);
            messenger.Subscribe<TaskbarButtonDragEnter>(OnTaskbarButtonDragEnter);
        }

        private void OnDeactivated(object? sender, EventArgs args)
        {
            if (XamlContent is TaskbarButtonFlyout flyout)
            {
                flyout.Close();
            }
        }

        private async void OnDpiChanged(object sender, DpiChangedEventArgs args)
        {
            if (isDpiChanging) return;

            isDpiChanging = true;

            await Dispatcher.Invoke(async () =>
            {
                Visibility = Visibility.Visible;
                await Dispatcher.BeginInvoke(new Action(() =>
                {
                    VisualTreeHelper.SetRootDpi(this, args.OldDpi);
                }), DispatcherPriority.ContextIdle, null);

                await Dispatcher.BeginInvoke(new Action(() =>
                {
                    VisualTreeHelper.SetRootDpi(this, args.NewDpi);
                }), DispatcherPriority.ContextIdle, null);

                await Dispatcher.BeginInvoke(new Action(() =>
                {
                    Visibility = Visibility.Hidden;
                    isDpiChanging = false;
                }), DispatcherPriority.ContextIdle, null);
            });
        }

        private void OnTaskbarButtonDragEnter(TaskbarButtonDragEnter args)
        {
            Dispatcher.Invoke(() => Open(args.Button));
        }

        private void OnTaskbarButtonInvoked(TaskbarButtonInvoked args)
        {
            Dispatcher.Invoke(() => Open(args.Button));
        }

        private void Open(TaskbarButton button)
        {
            mediator.Handle(new TaskbarButtonFlyoutActivation(button));
        }
    }
}
