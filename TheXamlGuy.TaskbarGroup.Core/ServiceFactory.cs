namespace TheXamlGuy.TaskbarGroup.Core
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly Func<Type, object> factory;

        private readonly Func<Type, object[], object> factoryWithParameters;

        public ServiceFactory(Func<Type, object> factory, Func<Type, object[], object> factoryWithParameters)
        {
            this.factory = factory;
            this.factoryWithParameters = factoryWithParameters;
        }

        public T Create<T>(params object[] parameters)
        {
            return (T)factoryWithParameters(typeof(T), parameters);
        }

        public T Create<T>(Type type)
        {
            return (T)factory(type);
        }

        public T Create<T>()
        {
            return (T)factory(typeof(T));
        }
    }
}