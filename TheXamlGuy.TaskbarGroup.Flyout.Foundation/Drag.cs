using Windows.UI.Xaml;

namespace TheXamlGuy.TaskbarGroup.Flyout.Foundation
{
    public record class Drag<TTarget>(DragEventArgs DragEventArgs) where TTarget : UIElement;
}
