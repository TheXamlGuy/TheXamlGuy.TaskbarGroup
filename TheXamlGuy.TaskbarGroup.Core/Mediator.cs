namespace TheXamlGuy.TaskbarGroup.Core
{
    public class Mediator : IMediator
    {
        private readonly IServiceFactory serviceFactory;

        public Mediator(IServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public void Handle<TEvent>() where TEvent : new()
        {
            Handle(new TEvent());
        }

        public TResponse Handle<TResponse, TRequest>(params object[] parameters) where TRequest : new()
        {
            return Handle<TResponse>(new TRequest(), parameters);
        }

        public void Handle(object request, params object[] parameters)
        {
            GetHandler(typeof(IMessageHandler<>).MakeGenericType(request.GetType()), parameters)
                .Handle((dynamic)request);
        }

        public TResponse Handle<TResponse>(object request, params object[] parameters)
        {
            return GetHandler(typeof(IMessageHandler<,>).MakeGenericType(typeof(TResponse), request.GetType()), parameters)
                .Handle((dynamic)request);
        }

        public Task HandleAsync<TEvent>() where TEvent : new()
        {
            return HandleAsync(new TEvent());
        }

        public Task<TResponse> HandleAsync<TResponse, TRequest>(params object[] parameters) where TRequest : new()
        {
            return HandleAsync<TResponse>(new TRequest(), parameters);
        }

        public Task HandleAsync(object request, CancellationToken cancellationToken, params object[] parameters)
        {
            return GetHandler(typeof(IAsyncMessageHandler<>).MakeGenericType(request.GetType()), parameters)
                .Handle((dynamic)request, cancellationToken);
        }

        public Task HandleAsync(object request, params object[] parameters)
        {
            return HandleAsync(request, CancellationToken.None, parameters);
        }

        public Task<TResponse> HandleAsync<TResponse>(object request, CancellationToken cancellationToken, params object[] parameters)
        {
            return GetHandler(typeof(IAsyncMessageHandler<,>).MakeGenericType(typeof(TResponse), request.GetType()), parameters)
                .Handle((dynamic)request, cancellationToken);
        }

        public Task<TResponse> HandleAsync<TResponse>(object request, params object[] parameters)
        {
            return HandleAsync<TResponse>(request, CancellationToken.None, parameters);
        }

        private dynamic GetHandler(Type type, params object[] parameters)
        {
            return parameters.Length == 0 ? serviceFactory.Create<object>(type) : serviceFactory.Create<object>(type, parameters);
        }
    }
}
