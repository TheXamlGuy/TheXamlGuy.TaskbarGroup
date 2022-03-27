using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace TheXamlGuy.TaskbarGroup.Core
{
    internal static class SystemInformationHelper
    {
        private const int SPI_GETWORKAREA = 48;

        public static Windows.Foundation.Rect VirtualScreen => GetVirtualScreen();
        public static Windows.Foundation.Rect WorkingArea => GetWorkingArea();

        private static Windows.Foundation.Rect GetVirtualScreen()
        {
            var size = new Size(PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_CXSCREEN), PInvoke.GetSystemMetrics(SYSTEM_METRICS_INDEX.SM_CYSCREEN));
            return new Windows.Foundation.Rect(0, 0, size.Width, size.Height);
        }

        private static Windows.Foundation.Rect GetWorkingArea()
        {
            var rect = new RECT();

            SystemParametersInfo(SPI_GETWORKAREA, 0, ref rect, 0);
            return new Windows.Foundation.Rect(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool SystemParametersInfo(int nAction, int nParam, ref RECT rc, int nUpdate);
    }
}