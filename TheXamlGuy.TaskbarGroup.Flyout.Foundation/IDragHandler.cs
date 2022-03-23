using TheXamlGuy.TaskbarGroup.Core;
using Windows.UI.Xaml;

namespace TheXamlGuy.TaskbarGroup.Flyout.Foundation
{
    public interface IDragHandler<TTarget> : IAsyncMessageHandler<Drag<TTarget>> where TTarget : UIElement
    {

    }
}
