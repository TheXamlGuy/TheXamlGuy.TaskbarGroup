using WindowsShortcutFactory;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public class CreateShortcutHandler : IMessageHandler<CreateShortcut>
    {
        public void Handle(CreateShortcut message)
        {
            using var shortcut = new WindowsShortcut
            {
                Path = message.Path,
            };

            shortcut.Save(@"C:\temp\MyShortcut.lnk");
        }

    }
}
