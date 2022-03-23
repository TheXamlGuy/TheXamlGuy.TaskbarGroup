using System.Windows;

namespace TheXamlGuy.TaskbarGroup.Foundation
{
    public static class TemplateGenerator
    {
        public static DataTemplate CreateDataTemplate(Func<object> factory)
        {
            var frameworkElementFactory = new FrameworkElementFactory(typeof(TemplateGeneratorControl));
            frameworkElementFactory.SetValue(TemplateGeneratorControl.FactoryProperty, factory);

            var dataTemplate = new DataTemplate(typeof(DependencyObject))
            {
                VisualTree = frameworkElementFactory
            };

            return dataTemplate;
        }
    }
}
