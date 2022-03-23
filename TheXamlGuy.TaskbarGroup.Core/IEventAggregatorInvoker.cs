using System.Reflection;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public interface IEventAggregatorInvoker
    {
        void Invoke<TMessage>(object target, TMessage message, MethodInfo methodInfo);
    }
}