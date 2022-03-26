using System;
using System.Threading;

namespace TheXamlGuy.TaskbarGroup
{
    public class Program
    {
        [STAThread()]
        public static void Main()
        {
            using (new Mutex(true, "TheXamlGuy.TaskbarGroup", out var createdNew))
            {
                if (createdNew)
                {
                    using (new Flyout.App())
                    {
                        var app = new App();
                        app.Run();
                    }
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
