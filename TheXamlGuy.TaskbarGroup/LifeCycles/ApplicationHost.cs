using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TheXamlGuy.TaskbarGroup.Core;

namespace TheXamlGuy.TaskbarGroup
{
    public sealed class ApplicationHost : IHostedService
    {
        private readonly TaskbarButtonFlyoutWindow flyoutWindow;
        private readonly IEnumerable<IInitializable> initializables;
        private bool isInitialized;

        public ApplicationHost(IEnumerable<IInitializable> initializables,
            TaskbarButtonFlyoutWindow flyoutWindow)
        {
            this.initializables = initializables;
            this.flyoutWindow = flyoutWindow;
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
                flyoutWindow.Show();
                await Task.CompletedTask;
            }
        }
    }
}
