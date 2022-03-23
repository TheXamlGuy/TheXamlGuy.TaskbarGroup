namespace TheXamlGuy.TaskbarGroup.Core
{
    public interface IDispatcherTimerFactory
    {
        IDispatcherTimer Create(Action actionDelegate, TimeSpan interval);
    }
}
