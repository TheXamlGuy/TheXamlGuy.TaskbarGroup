using System.Diagnostics;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public class IconStorageHandler : IMessageHandler<IconStorage>
    {
        public void Handle(IconStorage message)
        {
            Debug.WriteLine("Store icon");
        }
    }
}
