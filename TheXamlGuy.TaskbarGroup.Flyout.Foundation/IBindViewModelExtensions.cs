using TheXamlGuy.TaskbarGroup.Core;
using Windows.UI.Xaml;

namespace TheXamlGuy.TaskbarGroup.Flyout.Foundation
{
    public static class IBindViewModelExtensions
    {
        public static void Bind<TViewModel>(this IBindViewModel<TViewModel> view) where TViewModel : class
        {
            if (view is FrameworkElement frameworkElement)
            {
                void DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
                {
                    var viewModelProperty = view.GetType().GetProperty("ViewModel");
                    if (viewModelProperty is not null)
                    {
                        viewModelProperty.SetMethod.Invoke(sender, new[] { sender.DataContext });
                    }
                }

                frameworkElement.DataContextChanged += DataContextChanged;
            }
        }

        public static void Bind<TViewModel>(this IBindViewModel<TViewModel> view, object viewModel) where TViewModel : class
        {
            if (view is FrameworkElement frameworkElement)
            {
                var viewModelProperty = view.GetType().GetProperty("ViewModel");
                if (viewModelProperty is not null)
                {
                    viewModelProperty.SetValue(frameworkElement, viewModel);
                }
            }
        }
    }
}
