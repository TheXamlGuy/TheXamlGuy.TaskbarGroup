using System.Reflection;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public static class AssemblyExtensions
    {
        public static Stream ExtractResource(this Assembly assembly, string filename)
        {
            var resourceName = $"{assembly.GetName().Name.Replace("-", "_")}.{filename}";
            return assembly.GetManifestResourceStream(resourceName);
        }
    }
}
