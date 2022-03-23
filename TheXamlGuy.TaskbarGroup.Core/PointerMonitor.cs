using Windows.Win32;
using Windows.Win32.UI.WindowsAndMessaging;
using Windows.Win32.Foundation;
using System.Diagnostics.CodeAnalysis;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public class PointerMonitor : IPointerMonitor
    {
        private readonly IMessenger messenger;
        private bool isDisposed;
        private bool isPointerPressed;
        private HOOKPROC? mouseEventDelegate;
        private UnhookWindowsHookExSafeHandle? mouseHandle;
        private bool isPointerDrag;

        public PointerMonitor(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        ~PointerMonitor()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public unsafe void Initialize()
        {
            InitializeHook();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                RemoveHook();
                isDisposed = true;
            }
        }

        private unsafe void InitializeHook()
        {
            mouseEventDelegate = new HOOKPROC(MouseProc);
            mouseHandle = PInvoke.SetWindowsHookEx(WINDOWS_HOOK_ID.WH_MOUSE_LL, mouseEventDelegate, PInvoke.GetModuleHandle("user32.dll"), 0);
        }

        private unsafe bool TryGetPointer(out POINT point)
        {
            fixed (POINT* lpPointLocal = &point)
            {
                return PInvoke.GetPhysicalCursorPos(lpPointLocal);
            }
        }

        private bool TryGetPointerLocation([MaybeNullWhen(false)]out PointerLocation location)
        {
            if (TryGetPointer(out POINT point))
            {
                location = new PointerLocation(point.x, point.y);
                return true;

            }

            location = null;
            return false;
        }

        private LRESULT MouseProc(int nCode, WPARAM wParam, LPARAM lParam)
        {
            if (nCode >= 0)
            {
       
                if (TryGetPointerLocation(out var location))
                {
                    switch ((uint)wParam.Value)
                    {
                        case (uint)WndProcMessages.WM_MOUSEMOVE:
                            SendPointerMoved(location);
                            break;
                        case (uint)WndProcMessages.WM_LBUTTONUP:
                            SendPointerReleased(location, PointerButton.Left);
                            break;
                        case (uint)WndProcMessages.WM_MBUTTONUP:
                            SendPointerReleased(location, PointerButton.Middle);
                            break;
                        case (uint)WndProcMessages.WM_RBUTTONUP:
                            SendPointerReleased(location, PointerButton.Right);
                            break;
                        case (uint)WndProcMessages.WM_LBUTTONDOWN:
                            SendPointerPressed(location, PointerButton.Left);
                            break;
                        case (uint)WndProcMessages.WM_MBUTTONDOWN:
                            SendPointerPressed(location, PointerButton.Middle);
                            break;
                        case (uint)WndProcMessages.WM_RBUTTONDOWN:
                            SendPointerPressed(location, PointerButton.Right);
                            break;
                    }
                }
            }

            return PInvoke.CallNextHookEx(mouseHandle, nCode, wParam, lParam);
        }

        private unsafe void RemoveHook()
        {
            if (mouseHandle is not null && mouseHandle.DangerousGetHandle() != IntPtr.Zero)
            {
                PInvoke.UnhookWindowsHookEx((HHOOK)mouseHandle.DangerousGetHandle());
            }
        }

        private void SendPointerMoved(PointerLocation location)
        {
            if (isPointerPressed)
            {
                if (!isPointerDrag)
                {
                    isPointerDrag = true;
                }

                messenger.Send(new PointerDrag(location));
            }

            messenger.Send(new PointerMoved(location));
        }

        private void SendPointerPressed(PointerLocation location, PointerButton button)
        {
            isPointerPressed = true;
            messenger.Send(new PointerPressed(location, button));
        }

        private void SendPointerReleased(PointerLocation location, PointerButton button)
        {
            if (isPointerPressed)
            {
                if (isPointerDrag)
                {
                    isPointerDrag = false;
                    messenger.Send(new PointerDragReleased(location, button));
                }

                isPointerPressed = false;
                messenger.Send(new PointerReleased(location, button));
            }
        }
    }
}
