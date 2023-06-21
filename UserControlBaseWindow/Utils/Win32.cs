using System;
using System.Runtime.InteropServices;

namespace UserControlBaseWindow.Utils
{
    public static class Win32
    {
        private const int WM_GETMINMAXINFO = 0x0024;

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X, Y;

            public POINT(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left, Top, Right, Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            public int dwFlags = 0;
        }

        public enum MonitorOptions : uint
        {
            MONITOR_DEFAULTTONULL = 0x00000000,
            MONITOR_DEFAULTTOPRIMARY = 0x00000001,
            MONITOR_DEFAULTTONEAREST = 0x00000002
        }

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr handle, MonitorOptions dwFlags);


        [DllImport("user32.dll")]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        public static IntPtr WindowProc(IntPtr hwnd, int message, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (message)
            {
                case WM_GETMINMAXINFO:
                    WmGetMinMaxInfo(hwnd, lParam);
                    break;
                default:
                    break;
            }
            return IntPtr.Zero;
        }

        public static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            var minmaxInfo = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            IntPtr monitor = MonitorFromWindow(hwnd, MonitorOptions.MONITOR_DEFAULTTONEAREST);
            if (monitor != IntPtr.Zero)
            {
                var monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitor, monitorInfo);

                RECT rcWork = monitorInfo.rcWork, rcMonitor = monitorInfo.rcMonitor;
                minmaxInfo.ptMaxPosition.X = Math.Abs(rcWork.Left - rcMonitor.Left);
                minmaxInfo.ptMaxPosition.Y = Math.Abs(rcWork.Top - rcMonitor.Top);
                minmaxInfo.ptMaxSize.X = Math.Abs(rcWork.Right - rcWork.Left);
                minmaxInfo.ptMaxSize.Y = Math.Abs(rcWork.Bottom - rcWork.Top);
            }
            Marshal.StructureToPtr(minmaxInfo, lParam, true);
        }
    }
}
