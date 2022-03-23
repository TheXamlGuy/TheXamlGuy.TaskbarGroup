using TheXamlGuy.TaskbarGroup.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TheXamlGuy.TaskbarGroup.Flyout.Foundation
{
    public class TemplateSelector : DataTemplateSelector, ITemplateSelector
    {
        private readonly DataTemplateFactory dataTemplateFactory;

        public TemplateSelector(DataTemplateFactory dataTemplateFactory)
        {
            this.dataTemplateFactory = dataTemplateFactory;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is not null)
            {   
                var dataType = item.GetType();
                var dataTemplate = dataTemplateFactory.Create(dataType);
                if (dataTemplate is not null)
                {
                    return dataTemplate;
                }
            }

            return base.SelectTemplateCore(item, container);
        }
    }
}
