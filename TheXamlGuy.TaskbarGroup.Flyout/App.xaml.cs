using Microsoft.Toolkit.Win32.UI.XamlHost;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace TheXamlGuy.TaskbarGroup.Flyout
{
    public sealed partial class App : XamlApplication
    {
        public App()
        {
            Initialize();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            (Window.Current as object as IWindowPrivate).TransparentBackground = true;
            base.OnLaunched(args);
        }
    }
}
