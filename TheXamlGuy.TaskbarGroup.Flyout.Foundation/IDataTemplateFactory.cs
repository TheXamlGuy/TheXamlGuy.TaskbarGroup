using System;
using Windows.UI.Xaml;

namespace TheXamlGuy.TaskbarGroup.Flyout.Foundation
{
    public interface IDataTemplateFactory
    {
        DataTemplate Create(Type type);
    }
}
