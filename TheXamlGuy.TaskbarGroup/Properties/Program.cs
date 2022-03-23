using System;

namespace TheXamlGuy.TaskbarGroup
{
    public class Program
    {
        [STAThread()]
        public static void Main()
        {
            using (new Flyout.App())
            {
                var app = new App();
                app.Run();
            }
        }
    }
}
