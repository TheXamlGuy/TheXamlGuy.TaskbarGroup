namespace TheXamlGuy.TaskbarGroup.Core
{
    public interface ITaskbarButton : IDisposable
    {
        Rect Rect { get; }

        string Name { get; }
    }
}
