using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using TheXamlGuy.TaskbarGroup.Core;

namespace TheXamlGuy.TaskbarGroup.Flyout.Foundation
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRequiredFlyoutFoundation(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<TemplateSelector>()
                .AddSingleton<IDataTemplateCollection>(new DataTemplateCollection(new Dictionary<Type, Type>()))
                .AddSingleton<DataTemplateFactory>();
        }
    }
}
