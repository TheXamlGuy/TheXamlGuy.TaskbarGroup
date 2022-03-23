using System.Windows;

namespace TheXamlGuy.TaskbarGroup.Foundation
{
    public interface IDataTemplateFactory
    {
        DataTemplate? Create(Type type);
    }
}
