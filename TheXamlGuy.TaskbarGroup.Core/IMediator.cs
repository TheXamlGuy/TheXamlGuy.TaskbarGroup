
namespace TheXamlGuy.TaskbarGroup.Core
{
    public interface IMediator
    {
        void Handle(object request, params object[] parameters);
        void Handle<TEvent>() where TEvent : new();
        TResponse Handle<TResponse, TRequest>(params object[] parameters) where TRequest : new();
        TResponse Handle<TResponse>(object request, params object[] parameters);
        Task HandleAsync(object request, CancellationToken cancellationToken, params object[] parameters);
        Task HandleAsync(object request, params object[] parameters);
        Task HandleAsync<TEvent>() where TEvent : new();
        Task<TResponse> HandleAsync<TResponse, TRequest>(params object[] parameters) where TRequest : new();
        Task<TResponse> HandleAsync<TResponse>(object request, CancellationToken cancellationToken, params object[] parameters);
        Task<TResponse> HandleAsync<TResponse>(object request, params object[] parameters);
    }
}