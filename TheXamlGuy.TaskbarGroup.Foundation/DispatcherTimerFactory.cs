using System;
using TheXamlGuy.TaskbarGroup.Core;

namespace TheXamlGuy.TaskbarGroup.Foundation
{
    public class DispatcherTimerFactory : IDispatcherTimerFactory
    {
        public IDispatcherTimer Create(Action actionDelegate, TimeSpan interval)
        {
            return new DispatcherTimer(actionDelegate, interval);
        }
    }
}
