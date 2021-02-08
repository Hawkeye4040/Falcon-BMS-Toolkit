using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Falcon.Core
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class Win32
    {
        #region Fields

        //  Win32 parameter constants:

        // ReSharper disable once IdentifierTypo
        // ReSharper disable once InconsistentNaming
        public const int HWND_BROADCAST = 0xffff;

        #endregion

        #region Methods

        //  Win32 APIs relating to Inter-process Communication:

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int RegisterWindowMessage(string message);

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        #endregion
    }
}