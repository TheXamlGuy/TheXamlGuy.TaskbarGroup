using Windows.Win32;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public class TaskbarMonitor : ITaskbarMonitor
    {
        private const int SPI_SETWORKAREA = 0x002F;

        private readonly uint WM_TASKBARCREATED = PInvoke.RegisterWindowMessage("TaskbarCreated");
        private readonly IMessenger messenger;

        public TaskbarMonitor(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public void Initialize()
        {
            messenger.Subscribe<WndProc>(OnWndProc);
        }

        private void OnWndProc(WndProc args)
        {
            if (args.Message == WM_TASKBARCREATED || args.Message == (int)WndProcMessages.WM_SETTINGCHANGE && (int)args.WParam == SPI_SETWORKAREA)
            {
                messenger.Send<TaskbarChanged>();
            }
        }
    }
}