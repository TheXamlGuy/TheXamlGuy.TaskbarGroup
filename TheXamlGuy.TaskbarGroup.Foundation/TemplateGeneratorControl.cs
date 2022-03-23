using System.Windows;
using System.Windows.Controls;

namespace TheXamlGuy.TaskbarGroup.Foundation
{
    internal sealed class TemplateGeneratorControl : ContentControl
    {
        internal static readonly DependencyProperty FactoryProperty =
            DependencyProperty.Register("Factory", typeof(Func<object>),
                typeof(TemplateGeneratorControl), new PropertyMetadata(null,
                    OnFactoryPropertyChanged));

        private static void OnFactoryPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is TemplateGeneratorControl sender && args.NewValue is not null)
            {
                var factory = (Func<object>)args.NewValue;
                sender.Content = factory();
            }
        }
    }
}
