namespace TheXamlGuy.TaskbarGroup.Core
{
    public class TaskbarButtonShortcutMonitor : ITaskbarButtonShortcutMonitor
    {
        private readonly IMessenger messenger;
        private FileSystemWatcher? _watcher;
        private readonly TaskbarButtonConfiguration configuration;

        public TaskbarButtonShortcutMonitor(
            IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public void Initialize()
        {
            //_watcher = new FileSystemWatcher(configuration.PinnedShortcutDirectory)
            //{
            //    NotifyFilter = NotifyFilters.FileName,
            //    Filter = "*.ink",
            //    IncludeSubdirectories = true,
            //    EnableRaisingEvents = true
            //};

            //_watcher.Changed += OnChanged;
        }

        private void OnChanged(object sender, FileSystemEventArgs args)
        {
            if (args.ChangeType is WatcherChangeTypes.Created)
            {
                messenger.Send<TaskButtonShortcutRemoved>();
            }

            if (args.ChangeType is WatcherChangeTypes.Deleted)
            {
                messenger.Send<TaskButtonShortcutRemoved>();
            }
        }
    }
}
