namespace TheXamlGuy.TaskbarGroup.Core
{
    public interface IDisposer
    {
        void Add(object subject, params object[] objects);

        void Dispose(object subject);

        void Remove(object subject, IDisposable disposer);

        TDisposable Replace<TDisposable>(object subject, IDisposable disposer, TDisposable replacement) where TDisposable : IDisposable;
    }
}