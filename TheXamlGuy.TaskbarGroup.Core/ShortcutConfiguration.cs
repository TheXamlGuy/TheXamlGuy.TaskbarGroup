namespace TheXamlGuy.TaskbarGroup.Core
{
    public class ShortcutConfiguration
    {
        private string? _shortcutDirectory;

        public string? ShortcutDirectory
        {
            get => !string.IsNullOrEmpty(_shortcutDirectory) ? Environment.ExpandEnvironmentVariables(_shortcutDirectory) : null;
            set => _shortcutDirectory = value;
        }
    }
}
