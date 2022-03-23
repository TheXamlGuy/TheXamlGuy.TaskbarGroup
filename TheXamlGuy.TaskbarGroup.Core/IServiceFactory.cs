namespace TheXamlGuy.TaskbarGroup.Core
{
    public interface IServiceFactory
    {
        T Create<T>(params object[] parameters);

        T Create<T>(Type type);
        T Create<T>();
    }
}