using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TheXamlGuy.TaskbarGroup.Core;

namespace TheXamlGuy.TaskbarGroup
{
    public sealed class ApplicationHost : IHostedService
    {
        private readonly TaskbarButtonFlyoutWindow flyoutWindow;
        private readonly IEnumerable<IInitializable> initializables;
        private readonly IMediator mediator;
        private bool isInitialized;

        public ApplicationHost(IEnumerable<IInitializable> initializables,
            IMessenger messenger,
            IMediator mediator,
            TaskbarButtonFlyoutWindow flyoutWindow)
        {
            this.initializables = initializables;
            this.mediator = mediator;
            this.flyoutWindow = flyoutWindow;

            messenger.Subscribe<TaskbarButtonInvoked>(OnTaskbarButtonInvoked);
            messenger.Subscribe<TaskbarButtonDragEnter>(OnTaskbarButtonDragEnter);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await InitializeAsync();
            await StartupAsync();

            isInitialized = true;
        }


        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
        private async Task InitializeAsync()
        {
            if (!isInitialized)
            {
                foreach (var initializable in initializables)
                {
                    initializable.Initialize();
                }

                await Task.CompletedTask;
            }
        }

        private void OnTaskbarButtonDragEnter(TaskbarButtonDragEnter args)
        {
            Application.Current.Dispatcher.Invoke(() => Open(args.Button));
        }

        private void OnTaskbarButtonInvoked(TaskbarButtonInvoked args)
        {
            Application.Current.Dispatcher.Invoke(() => Open(args.Button));
        }

        private void Open(TaskbarButton button)
        {
            mediator.Handle(new TaskbarButtonFlyoutActivation(button));
        }

        private async Task StartupAsync()
        {
            if (!isInitialized)
            {
                flyoutWindow.Show();
                await Task.CompletedTask;
            }
        }
    }
}
