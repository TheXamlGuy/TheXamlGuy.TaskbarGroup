namespace TheXamlGuy.TaskbarGroup.Core
{
    public interface ITaskbar : IInitializable, IDisposable
    {
        TaskbarState GetCurrentState();

        IntPtr GetHandle();
    }
}