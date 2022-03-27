using Windows.Win32;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public partial class Taskbar : ITaskbar
    {
        private readonly IDisposer disposer;
        private readonly IMessenger messenger;
        private bool isDrag;
        private bool isWithinBounds;

        public Taskbar(IMessenger messenger,
            IDisposer disposer)
        {
            this.messenger = messenger;
            this.disposer = disposer;
        }

        public void Dispose()
        {
            disposer.Dispose(this);
            GC.SuppressFinalize(this);
        }

        public TaskbarState GetCurrentState()
        {
            var handle = GetHandle();
            var state = new TaskbarState
            {
                Screen = Screen.FromHandle(handle)
            };

            var appBarData = PInvoke.GetAppBarData(handle);
            PInvoke.GetAppBarPosition(ref appBarData);

            state.Rect = appBarData.rect.ToRect();
            state.Placement = (TaskbarPlacement)appBarData.uEdge;

            return state;
        }

        public IntPtr GetHandle()
        {
            return WindowHelper.Find("Shell_TrayWnd");
        }

        public void Initialize()
        {
            disposer.Add(this, messenger.Subscribe<WndProc>(OnWndProc));
            disposer.Add(this, messenger.Subscribe<PointerReleased>(OnPointerReleased));
            disposer.Add(this, messenger.Subscribe<PointerMoved>(OnPointerMoved));
            disposer.Add(this, messenger.Subscribe<PointerDrag>(OnPointerDrag));
        }

        private void OnPointerDrag(PointerDrag args)
        {
            if (isWithinBounds)
            {
                if (isDrag)
                {
                    messenger.Send<TaskbarDragOver>();
                }
                else
                {
                    messenger.Send<TaskbarDragEnter>();
                }

                isDrag = true;
            }
            else
            {
                isDrag = false;
            }
        }

        private void OnPointerMoved(PointerMoved args)
        {
            var taskbarHandle = GetHandle();
            if (WindowHelper.TryGetBounds(taskbarHandle, out var rect))
            {
                if (args.Location.IsWithinBounds(rect))
                {
                    if (isWithinBounds)
                    {
                        return;
                    }

                    isWithinBounds = true;
                    messenger.Send<TaskbarEnter>();
                }
                else
                {
                    isDrag = false;
                    isWithinBounds = false;
                }
            }
        }

        private void OnPointerReleased(PointerReleased args)
        {
            if (isDrag)
            {
                isDrag = false;
            }
        }

        private void OnWndProc(WndProc args)
        {
            if (args.Message == PInvoke.WM_TASKBARCREATED || args.Message == (int)WndProcMessages.WM_SETTINGCHANGE && (int)args.WParam == PInvoke.SPI_SETWORKAREA)
            {
                messenger.Send<TaskbarChanged>();
            }
        }
    }
}