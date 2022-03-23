using System.Reflection;
using System.Windows;
using TheXamlGuy.TaskbarGroup.Core;

namespace TheXamlGuy.TaskbarGroup.Foundation
{
    public class DataTemplateFactory : IDataTemplateFactory
    {
        private readonly IDataTemplateCollection datatemplateCollection;
        private readonly IServiceFactory serviceFactory;

        public DataTemplateFactory(IDataTemplateCollection datatemplateCollection,
            IServiceFactory serviceFactory)
        {
            this.datatemplateCollection = datatemplateCollection;
            this.serviceFactory = serviceFactory;
        }

        public virtual DataTemplate? Create(Type dataType)
        {
            if (dataType is null) throw new ArgumentNullException(nameof(dataType));

            if (!datatemplateCollection.TryGetValue(dataType, out Type? viewType))
            {
                var assembly = dataType.GetTypeInfo().Assembly;
                viewType = Type.GetType($"{dataType.FullName?.Replace("ViewModel", "View")}, {assembly.FullName}");
            }

            if (viewType is not null)
            {
                var view = serviceFactory.Create<object>(viewType);
                if (view is not null)
                {
                    return TemplateGenerator.CreateDataTemplate(() => view);
                }
            }

            return null;
        }
    }
}
