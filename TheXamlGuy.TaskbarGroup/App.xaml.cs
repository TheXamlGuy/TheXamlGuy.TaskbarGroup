using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;
using System.Windows;
using TheXamlGuy.TaskbarGroup.Core;
using TheXamlGuy.TaskbarGroup.Flyout;
using TheXamlGuy.TaskbarGroup.Flyout.Foundation;
using TheXamlGuy.TaskbarGroup.Foundation;

namespace TheXamlGuy.TaskbarGroup
{
    public partial class App : Application
    {
        private IHost? host;

        protected override async void OnStartup(StartupEventArgs args)
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);

            host = Host.CreateDefaultBuilder()
                    .ConfigureAppConfiguration(config =>
                    {
                        config.SetBasePath(appLocation);
                    })
                    .ConfigureServices(ConfigureServices)
                    .Build();

            await host.StartAsync();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddHostedService<ApplicationHost>()
                .AddRequiredCore()
                .AddRequiredFoundation()
                .AddRequiredFlyoutFoundation()
                .AddTransient<IMessageHandler<TaskbarButtonFlyoutActivation>, TaskbarButtonFlyoutActivationHandler>()
                .AddSingleton<TaskbarButtonFlyoutWindow>()
                .AddTransient<TaskbarButtonView>()
                .AddTransient<TaskbarButtonViewModel>()
                .AddTransient<TaskbarButtonGroupView>()
                .AddTransient<TaskbarButtonGroupViewModel>();
        }
    }
}
