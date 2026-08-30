using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace polyester_virus.Windows
{
    public static partial class WindowMover
    {
        internal partial class Native
        {
            [LibraryImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static partial bool EnumWindows(EnumWindowsProc lpEnumFunc, nint lParam);

            [LibraryImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static partial bool IsWindowVisible(nint hwnd);

            [LibraryImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static partial bool GetWindowRect(nint hwnd, out Rect lpRect);

            [LibraryImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static partial bool SetWindowPos(
                nint hwnd,
                nint hwndInsertAfter,
                int X, int Y, int cx, int cy, uint uFlags);
        }

        internal delegate bool EnumWindowsProc(nint hwnd, nint lParam);

        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOZORDER = 0x0004;
        const uint SWP_NOACTIVATE = 0x0010;

        public static void MoveWindowsRandomly()
        {
            List<(nint Handle, int Width, int Height)> windows = new();

            Native.EnumWindows((hwnd, _) =>
            {
                if (!Native.IsWindowVisible(hwnd))
                    return true;

                if (!Native.GetWindowRect(hwnd, out Rect rect))
                    return true;

                int width = rect.Right - rect.Left;
                int height = rect.Bottom - rect.Top;

                if (width <= 0 || height <= 0)
                    return true;

                windows.Add((hwnd, width, height));
                return true;
            }, nint.Zero);

            Rectangle desktop = SystemInformation.VirtualScreen;

            foreach ((nint Handle, int Width, int Height) in windows)
            {
                int maxX = Math.Max(desktop.Left, desktop.Right - Width);
                int maxY = Math.Max(desktop.Top, desktop.Bottom - Height);

                int x = Random.Shared.Next(desktop.Left, maxX + 1);
                int y = Random.Shared.Next(desktop.Top, maxY + 1);

                Native.SetWindowPos(
                    Handle,
                    nint.Zero,
                    x,
                    y,
                    0,
                    0,
                    SWP_NOSIZE | SWP_NOZORDER | SWP_NOACTIVATE
                );
            }
        }
    }
}
