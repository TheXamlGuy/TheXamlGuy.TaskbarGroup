using Windows.UI.Xaml;

namespace TheXamlGuy.TaskbarGroup.Flyout.Foundation
{
    public record class Drop<TTarget>(DragEventArgs DropEventArgs) where TTarget : UIElement
    {
        public TTarget Target { get; }
    }
}
