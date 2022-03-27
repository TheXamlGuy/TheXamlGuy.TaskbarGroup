using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
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
            host = new HostBuilder()
                .UseContentRoot(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                            "TheXamlGuy", "TaskbarGroup"), true)
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
                .AddHandler<TaskbarButtonFlyoutWindowActivationHandler>()
                .AddHandler<TaskbarButtonFlyoutActivationHandler>()
                .AddSingleton<TaskbarButtonFlyoutWindow>()
                .AddTransient<TaskbarButtonView>()
                .AddTransient<TaskbarButtonViewModel>()
                .AddTransient<TaskbarButtonGroupView>()
                .AddHandler<TaskbarButtonGroupDragHandler>()
                .AddHandler<TaskbarButtonGroupDropHandler>()
                .AddTransient<TaskbarButtonGroupViewModel>()
                .AddHandler<IconStorageHandler>();
        }
    }
}
