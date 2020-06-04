

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;

namespace BotLib
{
  public class ScreenCapture
  {
    private readonly object gfxLock = new object();
    private IntPtr hRgn = IntPtr.Zero;
    private const int ERROR = 0;
    private const int NULLREGION = 1;
    private const int SIMPLEREGION = 2;
    private const int COMPLEXREGION = 3;
    private Graphics gfxBmp;
    private IntPtr hdcBitmap;

    [DllImport("user32.dll")]
    private static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);

    [DllImport("gdi32.dll")]
    public static extern bool DeleteDC([In] IntPtr hdc);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(HandleRef hWnd, out ScreenCapture.RECT lpRect);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateRectRgn(
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

    public void DisposeDC()
    {
    }

    public Bitmap GetScreenshot(
      IntPtr ihandle,
      int sizeX,
      int sizeY,
      int offsetX,
      int offsetY,
      int outsizeX,
      int outsizeY)
    {
      lock (this.gfxLock)
      {
        IntPtr num = ihandle;
        ScreenCapture.GetWindowRect(new HandleRef((object) null, num), out ScreenCapture.RECT _);
        Bitmap bitmap = new Bitmap(sizeX, sizeY, PixelFormat.Format32bppArgb);
        RectangleF rectangleF = new RectangleF();
        Region region = (Region) null;
        try
        {
          if (this.gfxBmp == null)
          {
            this.gfxBmp = Graphics.FromImage((Image) bitmap);
          }
          else
          {
            this.gfxBmp.Dispose();
            this.gfxBmp = Graphics.FromImage((Image) bitmap);
          }
          this.hdcBitmap = this.gfxBmp.GetHdc();
          ScreenCapture.PrintWindow(num, this.hdcBitmap, 0U);
          if (this.hRgn.Equals((object) IntPtr.Zero))
            this.hRgn = ScreenCapture.CreateRectRgn(offsetX, offsetY, offsetX + outsizeX, offsetY + outsizeY);
          ScreenCapture.GetWindowRgn(num, this.hRgn);
          while (this.hRgn.Equals((object) IntPtr.Zero))
            ScreenCapture.GetWindowRgn(num, this.hRgn);
          region = Region.FromHrgn(this.hRgn);
          rectangleF = new RectangleF(845f, 55f, 35f, 40f);
          this.gfxBmp.ReleaseHdc(this.hdcBitmap);
          return bitmap.Clone(Rectangle.Round(region.GetBounds(this.gfxBmp)), PixelFormat.Format24bppRgb);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.StackTrace);
          return (Bitmap) null;
        }
        finally
        {
          bitmap.Dispose();
          region.Dispose();
        }
      }
    }

    public void WriteBitmapToFile(string filename, Bitmap bitmap)
    {
      bitmap.Save(filename, ImageFormat.Jpeg);
    }

    public struct RECT
    {
      public int Left;
      public int Top;
      public int Right;
      public int Bottom;

      public RECT(int left, int top, int right, int bottom)
      {
        this.Left = left;
        this.Top = top;
        this.Right = right;
        this.Bottom = bottom;
      }

      public RECT(Rectangle r)
        : this(r.Left, r.Top, r.Right, r.Bottom)
      {
      }

      public int X
      {
        get
        {
          return this.Left;
        }
        set
        {
          this.Right -= this.Left - value;
          this.Left = value;
        }
      }

      public int Y
      {
        get
        {
          return this.Top;
        }
        set
        {
          this.Bottom -= this.Top - value;
          this.Top = value;
        }
      }

      public int Height
      {
        get
        {
          return this.Bottom - this.Top;
        }
        set
        {
          this.Bottom = value + this.Top;
        }
      }

      public int Width
      {
        get
        {
          return this.Right - this.Left;
        }
        set
        {
          this.Right = value + this.Left;
        }
      }

      public Point Location
      {
        get
        {
          return new Point(this.Left, this.Top);
        }
        set
        {
          this.X = value.X;
          this.Y = value.Y;
        }
      }

      public Size Size
      {
        get
        {
          return new Size(this.Width, this.Height);
        }
        set
        {
          this.Width = value.Width;
          this.Height = value.Height;
        }
      }

      public static implicit operator Rectangle(ScreenCapture.RECT r)
      {
        return new Rectangle(r.Left, r.Top, r.Width, r.Height);
      }

      public static implicit operator ScreenCapture.RECT(Rectangle r)
      {
        return new ScreenCapture.RECT(r);
      }

      public static bool operator ==(ScreenCapture.RECT r1, ScreenCapture.RECT r2)
      {
        return r1.Equals(r2);
      }

      public static bool operator !=(ScreenCapture.RECT r1, ScreenCapture.RECT r2)
      {
        return !r1.Equals(r2);
      }

      public bool Equals(ScreenCapture.RECT r)
      {
        return r.Left == this.Left && r.Top == this.Top && r.Right == this.Right && r.Bottom == this.Bottom;
      }

      public override bool Equals(object obj)
      {
        switch (obj)
        {
          case ScreenCapture.RECT r:
            return this.Equals(r);
          case Rectangle r:
            return this.Equals(new ScreenCapture.RECT(r));
          default:
            return false;
        }
      }

      public override int GetHashCode()
      {
        return ((Rectangle) this).GetHashCode();
      }

      public override string ToString()
      {
        return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", (object) this.Left, (object) this.Top, (object) this.Right, (object) this.Bottom);
      }
    }
  }
}
