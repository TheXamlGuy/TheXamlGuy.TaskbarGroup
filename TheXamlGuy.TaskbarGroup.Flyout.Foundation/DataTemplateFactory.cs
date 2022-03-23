using System;
using TheXamlGuy.TaskbarGroup.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;
using System.Reflection;

namespace TheXamlGuy.TaskbarGroup.Flyout.Foundation
{
    public class DataTemplateFactory : IDataTemplateFactory
    {
        private readonly IDataTemplateCollection datatemplateCollection;

        public DataTemplateFactory(IDataTemplateCollection datatemplateCollection)
        {
            this.datatemplateCollection = datatemplateCollection;
        }

        public virtual DataTemplate Create(Type dataType)
        {
            if (dataType is null) throw new ArgumentNullException(nameof(dataType));

            if (!datatemplateCollection.TryGetValue(dataType, out Type viewType))
            {
                var assembly = dataType.GetTypeInfo().Assembly;
                viewType = Type.GetType($"{dataType.FullName?.Replace("ViewModel", "View")}, {assembly.FullName}");
            }

            if (viewType is not null)
            {
                var xaml = $"<DataTemplate " +
                                "xmlns:foundation=\"using:TheXamlGuy.TaskbarGroup.Flyout.Foundation\" " +
                                "xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" " +
                                "xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" " +
                                $"xmlns:local=\"using:{viewType.Namespace}\">" +
                                    $"<local:{viewType.Name} />" +
                            "</DataTemplate>";

                return (DataTemplate)XamlReader.Load(xaml);
            }

            return new DataTemplate();
        }
    }
}
