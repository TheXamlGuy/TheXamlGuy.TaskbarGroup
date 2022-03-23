using Microsoft.Toolkit.Wpf.UI.XamlHost;
using System.Windows;
using System.Windows.Media;

namespace TheXamlGuy.TaskbarGroup
{
    public class TransparentXamlWindow<TXamlContent> : XamlWindow<TXamlContent> where TXamlContent : Windows.UI.Xaml.UIElement
    {
        public TransparentXamlWindow() => PrepareDefaultWindow();

        protected override WindowsXamlHost OnInitializing(WindowsXamlHost xamlHost)
        {
            return base.OnInitializing(xamlHost);
        }

        private void PrepareDefaultWindow()
        {
            ShowInTaskbar = false;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            AllowsTransparency = true;
            Background = new SolidColorBrush(Colors.Transparent);
        }
    }
}
