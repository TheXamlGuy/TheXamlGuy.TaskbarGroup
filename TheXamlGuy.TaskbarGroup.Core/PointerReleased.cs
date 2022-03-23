namespace TheXamlGuy.TaskbarGroup.Core
{
    public record PointerDragReleased(PointerLocation Location, PointerButton Button = PointerButton.Left);

    public record PointerReleased(PointerLocation Location,  PointerButton Button = PointerButton.Left);
}
