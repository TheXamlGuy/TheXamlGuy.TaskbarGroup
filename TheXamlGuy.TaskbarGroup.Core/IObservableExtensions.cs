using System.Reactive.Linq;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public record FileDropped();

    public static class IObservableExtensions
    {
        public static IDisposable WeakSubscribe<TMessage>(this IObservable<TMessage> observable, IMessageInvoker invoker, Action<TMessage> actionDelegate)
        {
            var methodInfo = actionDelegate.Method;
            var weakReference = new WeakReference(actionDelegate.Target);
            IDisposable? subscription = null;

            subscription = observable.Subscribe(item =>
            {
                if (weakReference.Target is object target)
                {
                    invoker.Invoke(target, item, methodInfo);
                }
                else
                {
                    subscription?.Dispose();
                }
            });

            return subscription;
        }

    }
}
