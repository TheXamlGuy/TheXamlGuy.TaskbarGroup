using System.Reflection;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public interface IMessageInvoker
    {
        void Invoke<TMessage>(object target, TMessage message, MethodInfo methodInfo);
    }
}