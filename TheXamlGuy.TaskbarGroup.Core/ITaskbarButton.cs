namespace TheXamlGuy.TaskbarGroup.Core
{
    public interface ITaskbarButton : IDisposable
    {
        TaskbarButtonBounds Bounds { get; }

        string Name { get; }
    }
}
