using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;

namespace TheXamlGuy.TaskbarGroup.Flyout.Controls
{
    public class TaskbarButtonFlyout : ContentControl
    {
        public static readonly DependencyProperty TemplateSettingsProperty =
              DependencyProperty.Register(nameof(TemplateSettings),
                  typeof(TaskbarButtonFlyoutTemplateSettings), typeof(TaskbarButtonFlyout),
                  new PropertyMetadata(null));

        private UIElement child;
        private Border container;

        public TaskbarButtonFlyout()
        {
            DefaultStyleKey = typeof(TaskbarButtonFlyout);
            TemplateSettings = new TaskbarButtonFlyoutTemplateSettings();
        }

        public TaskbarButtonFlyoutTemplateSettings TemplateSettings
        {
            get => (TaskbarButtonFlyoutTemplateSettings)GetValue(TemplateSettingsProperty);
            set => SetValue(TemplateSettingsProperty, value);
        }

        public bool IsOpen { get; private set; }

        public void Close()
        {
            if(container is not null)
            {
                container.Child = null;
            }
        }

        protected override void OnApplyTemplate()
        {
            container = GetTemplateChild("Container") as Border;
            if (container != null)
            {
                child = container.Child;
                container.Child = null;
            }
        }

        public void ShowAt(TaskbarButtonFlyoutPlacement taskbarPlacement)
        {
            VisualStateManager.GoToState(this, "DefaultPlacement", true);

            child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            var width = child.DesiredSize.Width - 1;
            var height = child.DesiredSize.Height - 1;

            TemplateSettings.SetValue(TaskbarButtonFlyoutTemplateSettings.HeightProperty, height);
            TemplateSettings.SetValue(TaskbarButtonFlyoutTemplateSettings.WidthProperty, width);

            TemplateSettings.SetValue(TaskbarButtonFlyoutTemplateSettings.NegativeHeightProperty, -height);
            TemplateSettings.SetValue(TaskbarButtonFlyoutTemplateSettings.NegativeWidthProperty, -width);

            VisualStateManager.GoToState(this, $"{taskbarPlacement}Placement", true);

            container.Child = child;
        }
    }
}
