using System;
using TheXamlGuy.TaskbarGroup.Core;

namespace TheXamlGuy.TaskbarGroup.Foundation
{
    public class DispatcherTimer : IDispatcherTimer
    {
        private readonly System.Windows.Threading.DispatcherTimer timer;
        private readonly Action actionDelegate;

        public DispatcherTimer(Action actionDelegate, TimeSpan interval)
        {
            timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = interval
            };

            timer.Tick += OnTick;
            this.actionDelegate = actionDelegate;
        }

        private void OnTick(object? sender, EventArgs args)
        {
            actionDelegate?.Invoke();
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}
