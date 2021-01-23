using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
namespace BotLib
{
     public class WindowsAPI
     {
          private const uint WM_KEYDOWN = 256;
          private const uint WM_KEYUP = 257;
          private const uint WM_SYSCOMMAND = 24;
          private const uint SC_CLOSE = 83;
          private const uint WM_ACTIVATEAPP = 28;
          private const uint WM_NCACTIVATE = 134;
          private const uint WM_ACTIVATE = 6;
          private const uint WM_IME_SETCONTEXT = 641;
          private const uint WM_IME_NOTIFY = 642;
          private const uint WM_CHAR = 258;
          private const uint WM_GETTEXTLENGTH = 14;
          private const uint EM_GETSEL = 176;
          private const uint EM_GETRECT = 178;
          private const uint EM_GETFONT = 49;
          private const uint EM_LINEFROMCHAR = 201;
          private const uint EM_CHARFROMPOS = 533;
          private const uint EM_POSFROMCHAR = 532;
          private const uint EM_LINELENGTH = 193;
          private const uint WM_SETTEXT = 12;
          private const uint WM_WINDOWPOSCHANGING = 70;
          private const uint SWP_NOSIZE = 1;
          private const uint SWP_NOMOVE = 2;
          private const uint SWP_NOZORDER = 4;
          private const uint SWP_NOREDRAW = 8;
          private const uint SWP_NOACTIVATE = 16;
          private const uint SWP_FRAMECHANGED = 32;
          private const uint SWP_SHOWWINDOW = 64;
          private const uint SWP_HIDEWINDOW = 128;
          private const uint SWP_NOCOPYBITS = 256;
          private const uint SWP_NOOWNERZORDER = 512;
          private const uint SWP_NOSENDCHANGING = 1024;

          [DllImport("user32")]
          [return: MarshalAs(UnmanagedType.Bool)]
          private static extern bool EnumChildWindows(
     IntPtr window,
     EnumWindowProc callback,
     IntPtr lParam);

          [DllImport("User32.dll")]
          private static extern int SetForegroundWindow(IntPtr point);

          [DllImport("User32.dll")]
          public static extern IntPtr PostMessage(
            IntPtr hWnd,
            uint Msg,
            bool wParam,
            IntPtr lParam);

          [DllImport("User32.dll")]
          public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, bool wParam, uint lParam);

          [DllImport("User32.dll")]
          public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

          [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
          public static extern void mouse_event(
            uint dwFlags,
            uint dx,
            uint dy,
            uint cButtons,
            uint dwExtraInfo);

          [DllImport("user32.dll")]
          public static extern IntPtr SendMessage(
            IntPtr hWnd,
            uint Msg,
            IntPtr wParam,
            IntPtr lParam);

          [DllImport("user32.dll")]
          public static extern IntPtr SendMessage(
            IntPtr hWnd,
            uint Msg,
            uint wParam,
            IntPtr lParam);

          [DllImport("user32.dll")]
          public static extern IntPtr FindWindowEx(
            IntPtr parentHandle,
            IntPtr childAfter,
            string className,
            string windowTitle);

          [DllImport("user32.dll")]
          [return: MarshalAs(UnmanagedType.Bool)]
          public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

          [DllImport("user32.dll", SetLastError = true)]
          private static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int X,
            int Y,
            int cx,
            int cy,
            uint uFlags);

          [DllImport("user32.dll")]
          [return: MarshalAs(UnmanagedType.Bool)]
          private static extern bool SetCursorPos(int x, int y);

          [DllImport("user32.dll")]
          private static extern bool GetCursorPos(ref Point lpPoint);

          [DllImport("user32.dll")]
          private static extern IntPtr GetLastActivePopup(IntPtr hWnd);

          [DllImport("user32.dll", SetLastError = true)]
          private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

          [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
          public static extern int BitBlt(
            IntPtr hDC,
            int x,
            int y,
            int nWidth,
            int nHeight,
            IntPtr hSrcDC,
            int xSrc,
            int ySrc,
            int dwRop);
     }
      delegate bool EnumWindowProc(IntPtr hwnd, IntPtr lParam);

     public struct RECT
     {
          public int Left;
          public int Top;
          public int Right;
          public int Bottom;
     }
}
