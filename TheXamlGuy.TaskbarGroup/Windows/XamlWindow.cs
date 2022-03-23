using Microsoft.Toolkit.Wpf.UI.XamlHost;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace TheXamlGuy.TaskbarGroup
{
    public class XamlWindow<TXamlContent> : Window where TXamlContent : Windows.UI.Xaml.UIElement
    {
        private bool isDpiChanging;
        private WindowsXamlHost? xamlHost;

        public XamlWindow()
        {
            Initialize();
            DpiChanged += OnDpiChanged;
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
    }
}
