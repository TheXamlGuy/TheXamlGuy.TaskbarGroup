using Microsoft.Toolkit.Wpf.UI.XamlHost;
using System.Windows;

namespace TheXamlGuy.TaskbarGroup
{
    public class XamlWindow<TXamlContent> : Window where TXamlContent : Windows.UI.Xaml.UIElement
    {
        private WindowsXamlHost? xamlHost;

        public XamlWindow()
        {
            Initialize();
        }

        public TXamlContent? XamlContent
        {
            get
            {
                if (xamlHost is null) return null;
                return xamlHost.GetUwpInternalObject() as TXamlContent;
            }
        }

        protected virtual WindowsXamlHost OnInitializing(WindowsXamlHost xamlHost)
        {
            xamlHost.InitialTypeName = typeof(TXamlContent).FullName;
            xamlHost.HorizontalAlignment = HorizontalAlignment.Stretch;
            xamlHost.VerticalAlignment = VerticalAlignment.Stretch;

            return xamlHost;
        }

        private void Initialize()
        {
            xamlHost = new WindowsXamlHost();
            OnInitializing(xamlHost);

            Content = xamlHost;
        }
    }
}
