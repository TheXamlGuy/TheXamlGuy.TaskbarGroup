using TheXamlGuy.TaskbarGroup.Core;
using Windows.UI.Xaml;

namespace TheXamlGuy.TaskbarGroup.Flyout.Foundation
{
    public interface IDropHandler<TTarget> : IMessageHandler<Drop<TTarget>> where TTarget : UIElement
    {

    }
}
