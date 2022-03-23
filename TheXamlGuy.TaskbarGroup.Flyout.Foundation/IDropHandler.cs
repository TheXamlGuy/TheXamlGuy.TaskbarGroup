using TheXamlGuy.TaskbarGroup.Core;
using Windows.UI.Xaml;

namespace TheXamlGuy.TaskbarGroup.Flyout.Foundation
{
    public interface IDropHandler<TTarget> : IAsyncMessageHandler<Drop<TTarget>> where TTarget : UIElement
    {

    }
}
