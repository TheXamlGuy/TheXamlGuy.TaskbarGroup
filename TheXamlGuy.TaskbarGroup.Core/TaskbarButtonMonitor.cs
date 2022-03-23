using Windows.Win32.Foundation;
using UIAutomationClient;
using System.Diagnostics;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public class TaskbarButtonMonitor : ITaskbarButtonMonitor
    {
        private readonly IDispatcherTimer dispatcherTimer;
        private readonly IDispatcherTimerFactory dispatcherTimerFactory;
        private readonly IServiceFactory serviceFactory;
        private readonly IMessenger messenger;
        private readonly Dictionary<string, TaskbarButton> taskbarButtons = new();
        private RECT taskbarBoundsCache;
        private IUIAutomationCondition? taskListCondition;
        private IUIAutomationElement? taskListElement;
        private HWND taskListHandle;

        public TaskbarButtonMonitor(IMessenger messenger, 
            IDispatcherTimerFactory dispatcherTimerFactory,
            IServiceFactory serviceFactory)
        {
            this.messenger = messenger;
            this.dispatcherTimerFactory = dispatcherTimerFactory;
            this.serviceFactory = serviceFactory;

            dispatcherTimer = dispatcherTimerFactory.Create(OnDispatcher, TimeSpan.FromMilliseconds(500));
        }

        public void Initialize()
        {
            var clientUIAutomation = new CUIAutomation();
            taskListCondition = clientUIAutomation.CreateTrueCondition();

            var trayHandle = WindowHelper.Find("Shell_TrayWnd");

            var rebarHandle = WindowHelper.Find("ReBarWindow32", trayHandle);
            var taskHandle = WindowHelper.Find("MSTaskSwWClass", rebarHandle);
            taskListHandle = WindowHelper.Find("MSTaskListWClass", taskHandle);

            taskListElement = clientUIAutomation.ElementFromHandle(taskListHandle);

            if (WindowHelper.TryGetBounds(taskListHandle, out var bounds))
            {
                taskbarBoundsCache = bounds;
            }

            dispatcherTimer.Start();
            UpdateTaskbarButtons();
        }

        private bool CheckDirtyTaskbarRegion()
        {
            if (WindowHelper.TryGetBounds(taskListHandle, out var bounds))
            {
                var width = taskbarBoundsCache.right - taskbarBoundsCache.left;
                var height = taskbarBoundsCache.bottom - taskbarBoundsCache.top;

                var deltaWidth = bounds.right - bounds.left;
                var deltaHeight = bounds.bottom - bounds.top;

                if (width != deltaWidth || height != deltaHeight)
                {
                    taskbarBoundsCache = bounds;
                    return true;
                }
            }

            return false;
        }

        private Dictionary<string, tagRECT> FindTaskbarButtons()
        {
            var taskElements = taskListElement?.FindAll(TreeScope.TreeScope_Descendants | TreeScope.TreeScope_Children, taskListCondition);

            var buttons = new Dictionary<string, tagRECT>();
            if (taskElements is not null)
            {
                for (int index = 0; index <= taskElements.Length - 1; index++)
                {
                    var taskUIElement = taskElements.GetElement(index);
                    var name = taskUIElement.CurrentName;
                    var rect = taskUIElement.CurrentBoundingRectangle;

                    buttons.Add(name, rect);
                }
            }

            return buttons;
        }

        private void OnDispatcher()
        {
            dispatcherTimer.Stop();

            if (CheckDirtyTaskbarRegion())
            {
                UpdateTaskbarButtons();
            }

            dispatcherTimer.Start();
        }

        private void UpdateTaskbarButtons()
        {
            if (taskListElement is null)
            {
                return;
            }

            var buttons = FindTaskbarButtons();

            foreach (var buttonToRemove in taskbarButtons.Where(taskbarButton => !buttons.ContainsKey(taskbarButton.Key)))
            {
                var key = buttonToRemove.Key;
                var button = buttonToRemove.Value;

                Debug.WriteLine($"{key} button removed");

                taskbarButtons.Remove(key);
                messenger.Send(new TaskbarButtonRemoved(button));

                button.Dispose();
            }

            foreach (var button in buttons)
            {
                var name = button.Key;
                var bounds = button.Value;

                var buttonBounds = new TaskbarButtonBounds(bounds.left,
                    bounds.top,
                    bounds.right - bounds.left,
                    bounds.bottom - bounds.top);

                if (taskbarButtons.TryGetValue(name, out var taskbarButton))
                {
                    Debug.WriteLine($"{name} button updated");

                    taskbarButtons[name].Bounds = buttonBounds;
                    messenger.Send(new TaskbarButtonUpdated(taskbarButtons[name]));
                }
                else
                {
                    Debug.WriteLine($"{name} button added");

                    taskbarButtons.Add(name, serviceFactory.Create<TaskbarButton>(name, buttonBounds));
                    messenger.Send(new TaskbarButtonCreated(taskbarButtons[name]));
                }
            }
        }
    }
}
