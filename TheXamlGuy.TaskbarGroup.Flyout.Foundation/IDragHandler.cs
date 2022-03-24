using TheXamlGuy.TaskbarGroup.Core;
using Windows.UI.Xaml;

namespace TheXamlGuy.TaskbarGroup.Flyout.Foundation
{
    public interface IDragHandler<TTarget> : IMessageHandler<Drag<TTarget>> where TTarget : UIElement
    {

    }
}
