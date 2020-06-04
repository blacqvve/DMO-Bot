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
