using System.Reflection;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public class MessageInvoker : IMessageInvoker
    {
        public void Invoke<TMessage>(object target, TMessage message, MethodInfo methodInfo)
        {
            if (message is null) throw new ArgumentNullException(nameof(message));

            methodInfo.Invoke(target, new object[] { message });
        }
    }
}