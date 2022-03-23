using System;
using System.Windows;
using System.Windows.Interop;
using TheXamlGuy.TaskbarGroup.Core;
using Windows.Win32.Foundation;

namespace TheXamlGuy.TaskbarGroup.Foundation
{
    public static class WindowExtensions
    {
        public static IntPtr GetHandle(this Window window)
        {
            return new WindowInteropHelper(window).Handle;
        }

        public static void MoveAndResize(this Window window, int x, int y, int width, int height)
        {
            var handle = window.GetHandle();
            WindowHelper.MoveAndResize(new HWND(handle), x, y, width, height);
        }

        public static void BringToForeground(this Window window)
        {
            var handle = window.GetHandle();
            WindowHelper.BringToForeground(new HWND(handle));
        }

    }
}
