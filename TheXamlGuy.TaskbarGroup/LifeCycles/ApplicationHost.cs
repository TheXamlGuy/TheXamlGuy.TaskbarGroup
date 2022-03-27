using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TheXamlGuy.TaskbarGroup.Core;

namespace TheXamlGuy.TaskbarGroup
{
    public sealed class ApplicationHost : IHostedService
    {
        private readonly IMediator mediator;
        private readonly List<IInitializable> initializables = new();
        private bool isInitialized;

        public ApplicationHost(IWndProcMonitor wndProcMonitor,
            ITaskbar taskbar,
            IPointerMonitor pointerMonitor,
            ITaskbarButtonMonitor taskbarButtonMonitor,
            ITaskbarButtonShortcutMonitor taskbarButtonShortcutMonitor,
            IMediator mediator)
        {
            initializables.Add(wndProcMonitor);
            initializables.Add(taskbar);
            initializables.Add(pointerMonitor);
            initializables.Add(taskbarButtonMonitor);
            initializables.Add(taskbarButtonShortcutMonitor);

            this.mediator = mediator;
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

        private async Task StartupAsync()
        {
            if (!isInitialized)
            {
                mediator.Handle<TaskbarButtonFlyoutWindowActivation>();
                await Task.CompletedTask;
            }
        }
    }
}
