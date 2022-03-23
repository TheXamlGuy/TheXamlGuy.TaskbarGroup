using System.Windows;
using System.Windows.Controls;
using TheXamlGuy.TaskbarGroup.Core;

namespace TheXamlGuy.TaskbarGroup.Foundation
{
    public class TemplateSelector : DataTemplateSelector, ITemplateSelector
    {
        private readonly DataTemplateFactory dataTemplateFactory;

        public TemplateSelector(DataTemplateFactory dataTemplateFactory)
        {
            this.dataTemplateFactory = dataTemplateFactory;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is not null)
            {
                var dataType = item.GetType();
                var dataTemplate = dataTemplateFactory.Create(dataType);
                if (dataTemplate is not null)
                {
                    dataTemplate.Seal();
                    return dataTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
