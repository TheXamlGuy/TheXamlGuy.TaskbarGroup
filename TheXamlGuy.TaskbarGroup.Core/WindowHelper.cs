using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace TheXamlGuy.TaskbarGroup.Core
{
    public class WindowHelper
    {
        public static void MoveAndResize(HWND handle, int x, int y, int width, int height)
        {
            PInvoke.SetWindowPos(handle, new HWND(), x, y, width, height, 0);
        }

        public static void BringToForeground(HWND handle)
        {
            if (TryGetBounds(handle, out var bounds))
            {
                PInvoke.SetWindowPos(handle, new HWND(), bounds.left, bounds.top, bounds.right - bounds.left, bounds.bottom - bounds.top, SET_WINDOW_POS_FLAGS.SWP_SHOWWINDOW | SET_WINDOW_POS_FLAGS.SWP_NOACTIVATE);
            }
        }

        public static HWND Find(string windowName) => PInvoke.FindWindow(windowName, null);

        public static HWND Find(string windowName, HWND parentHandle)
        {
            return PInvoke.FindWindowEx(parentHandle, new HWND(), windowName, null);
        }

        public static unsafe bool TryGetBounds(IntPtr handle, out RECT rect)
        {
            fixed (RECT* lpRectLocal = &rect)
            {
                return PInvoke.GetWindowRect(new HWND(handle), lpRectLocal);
            }
        }
    }
}