namespace TheXamlGuy.TaskbarGroup.Core
{
    public class TaskbarList : ITaskbarList
    {
        private readonly ITaskbar taskbar;

        public TaskbarList(ITaskbar taskbar)
        {
            this.taskbar = taskbar;
        }

        public IntPtr GetHandle()
        {
            var trayHandle = taskbar.GetHandle();

            var rebarHandle = WindowHelper.Find("ReBarWindow32", trayHandle);
            var taskHandle = WindowHelper.Find("MSTaskSwWClass", rebarHandle);
            return WindowHelper.Find("MSTaskListWClass", taskHandle);
        }
    }
}
