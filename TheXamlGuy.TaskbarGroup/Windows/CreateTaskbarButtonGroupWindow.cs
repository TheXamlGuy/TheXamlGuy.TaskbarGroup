using System.Windows;
using System.Windows.Media;

namespace TheXamlGuy.TaskbarGroup
{
    public class CreateTaskbarButtonGroupWindow : Window
    {
        public CreateTaskbarButtonGroupWindow()
        {
            Height = 0;
            Width = 0;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            AllowsTransparency = true;
            Background = new SolidColorBrush(Colors.Transparent);
        }
    }
}
