using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BotLib;
using Message = BotLib.Message;

namespace BotTestForms
{
     public partial class Form1 : Form
     {
          
          private static bool isActive = false;
          private static bool isActiveInventory = false;
          private static bool isHunting = false;
          private static bool jitterIsActive = false;
          private static bool antiMacroIsActive = false;
          private static bool isInventoryCalibrating = false;
          private static bool isAutomaticResourceManagementActive = false;
          private static bool isAutomaticResourceManagementActiveMem = false;
          private static bool isAutomaticResourceManagementCalibrating = false;
          private static bool isAutomaticHPCalibrating = false;
          private static bool isManualTargetCalibrating = false;
          private static bool isHuntingManually = false;
          private static int currentMouseX = 0;
          private static int currentMouseY = 0;
          public static string processName = "";
          private static List<ResourceManagement> rmList = new List<ResourceManagement>();
          private static bool isCalibrating = false;
          private static bool isKorean = false;
          private static bool isDigiMemActive = false;
          private Bitmap currentTarget = (Bitmap)null;
          private MemoryStream targetImg = new MemoryStream();
          private readonly object bmpLock = new object();
          private ScreenCapture scTarget = new ScreenCapture();
          private ScreenCapture scInventory = new ScreenCapture();
          private ScreenCapture scResources = new ScreenCapture();
          private ScreenCapture scDetector = new ScreenCapture();
          private ScreenCapture scSolver = new ScreenCapture();
          private ScreenCapture scManualTarget = new ScreenCapture();
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
          private static Point manualTargetLocation;
          private Message msg;

          private static TaskManagerForm taskmgr;

          private static IntPtr h;
          private static IntPtr child;
          private static Color manualPixel;
          private Process p;
          public Form1()
          {
               Message m = new Message("test");
               InitializeComponent();
               Process[] processes = Process.GetProcesses();
               string[] pnames = new string[processes.Length];
               for (int index = 0; index < pnames.Length; ++index)
                    pnames[index] = processes[index].ProcessName;
               Form1.taskmgr = new TaskManagerForm(pnames);
               ConfigrationForm confForm = new ConfigrationForm();
               confForm.Show();
               
               try
               {
                    this.p = Process.GetProcessesByName("GDMO")[0];
                    this.p.WaitForInputIdle();
                    Form1.h = this.p.MainWindowHandle;
                    Form1.child = WindowsAPI.FindWindowEx(Form1.h, IntPtr.Zero, "Edit", (string)null);
                    if (!(m.message != "null"))
                         return;
                    int num = (int)MessageBox.Show(string.Format(m.message, (object)Environment.NewLine), "Notice", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
               }
               catch (Exception ex1)
               {
                    new Thread(new ThreadStart(FindTaskAsync)).Start();
                    int num1 = (int)MessageBox.Show("DMO Process not found, select one from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    Form1.taskmgr.Show();
                    if (!(m.message != "null"))
                         return;
                    int num2 = (int)MessageBox.Show(string.Format(m.message, (object)Environment.NewLine), "Notice", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
               }
          }
          private async void FindTaskAsync()
          {
               try
               {
                    int tries = 0;
                    while (Form1.processName == "")
                    {
                         await Task.Delay(1000);
                         ++tries;
                         if (tries > 30)
                              Form1.processName = "ERR";
                    }
                    this.p = Process.GetProcessesByName(Form1.processName)[0];
                    this.p.WaitForInputIdle();
                    Form1.h = this.p.MainWindowHandle;
                    Form1.child = WindowsAPI.FindWindowEx(Form1.h, IntPtr.Zero, "Edit", (string)null);
               }
               catch (Exception ex1)
               {
                    Exception ex = ex1;
                    int num = (int)MessageBox.Show("DMO Process not found, or no Administrator permissions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    Application.Exit();
               }
          }
          private async void iterateRTB1(object param)
          {
               string[] lines = (string[])param;
               while (isActive)
               {
                    string[] strArray = lines;
                    for (int index = 0; index < strArray.Length; ++index)
                    {
                         string s = strArray[index];
                         if (isHunting && !processTarget(isHuntingManually))
                         {
                              if (s.Contains("TAB"))
                                   WindowsAPI.SendMessage(Form1.h, 257U, (IntPtr)9, IntPtr.Zero);
                              if (s.Equals(Configuration.PICKUP_KEY))
                                   WindowsAPI.SendMessage(Form1.h, 257U, Convert.ToUInt32(s.ToCharArray()[0]), IntPtr.Zero);
                         }
                         else if (!s.Equals(""))
                         {
                              if (s.Contains("delay"))
                                   await Task.Delay(int.Parse(s.Split(':')[1]));
                              else if (s.Equals("F1"))
                                   WindowsAPI.SendMessage(Form1.h, 257U, (IntPtr)112, IntPtr.Zero);
                              else if (s.Equals("F2"))
                                   WindowsAPI.SendMessage(Form1.h, 257U, (IntPtr)113, IntPtr.Zero);
                              else if (s.Contains("TAB"))
                              {
                                   WindowsAPI.SendMessage(Form1.h, 257U, (IntPtr)9, IntPtr.Zero);
                              }
                              else
                              {
                                   WindowsAPI.SendMessage(Form1.h, 257U, Convert.ToUInt32(s.ToCharArray()[0]), IntPtr.Zero);
                                   s = (string)null;
                              }
                         }
                    }
                    strArray = (string[])null;
               }
               Application.ExitThread();
          }
          private async void iterate1()
          {

               while (isActive)
               {

                    WindowsAPI.SendMessage(Form1.h, 257U, (IntPtr)9, IntPtr.Zero);
                    Thread.Sleep(100);
               }
               Application.ExitThread();
          }
          private bool processTarget(bool isManual)
          {
               if (isManual)
               {
                    Color colorAt = GetColorAt(manualTargetLocation, this.scManualTarget);
                    return Math.Abs((int)Form1.manualPixel.R - (int)colorAt.R) < 5 && Math.Abs((int)Form1.manualPixel.G - (int)colorAt.G) < 5 && Math.Abs((int)Form1.manualPixel.B - (int)colorAt.B) < 5;
               }
               RECT lpRect;
               if (WindowsAPI.GetWindowRect(Form1.h, out lpRect))
               {
                    int num1 = lpRect.Right - lpRect.Left;
                    int num2 = lpRect.Bottom - lpRect.Top;
                    Bitmap bitmap1 = new Bitmap(35, 40, PixelFormat.Format24bppRgb);
                    int width = bitmap1.Width;
                    int height = bitmap1.Height;
                    try
                    {
                         double num3 = 0.06;
                         switch (num2)
                         {
                              case 799:
                                   num3 = 0.051;
                                   break;
                              case 931:
                                   num3 = 0.06;
                                   break;
                              case 939:
                                   num3 = 0.06;
                                   break;
                              case 1040:
                                   num3 = 0.067;
                                   break;
                         }
                         double num4 = 0.526;
                         switch (num1)
                         {
                              case 1374:
                                   num4 = 0.449;
                                   break;
                              case 1608:
                                   num4 = 0.526;
                                   break;
                              case 1616:
                                   num4 = 0.526;
                                   break;
                              case 1802:
                                   num4 = 0.59;
                                   break;
                         }
                         bitmap1 = this.scTarget.GetScreenshot(Form1.h, 1111, 686, (int)(num4 * (double)num1), (int)(num3 * (double)num2), 35, 40);
                         int x = bitmap1.Width / 2;
                         int y = bitmap1.Height / 2;
                         if (this.targetImg.Length == 0L)
                         {
                              bitmap1.Save((Stream)this.targetImg, ImageFormat.Jpeg);
                              this.currentTarget = bitmap1.Clone(new Rectangle(0, 0, 35, 40), PixelFormat.Format24bppRgb);
                              Bitmap bitmap2 = bitmap1.Clone(new Rectangle(0, 0, 35, 40), PixelFormat.Format24bppRgb);
                         }
                         Color pixel1 = this.currentTarget.GetPixel(this.currentTarget.Width / 2, this.currentTarget.Height / 2);
                         Color pixel2 = bitmap1.GetPixel(x, y);
                         return Math.Abs((int)pixel1.R - (int)pixel2.R) < 5 && Math.Abs((int)pixel1.G - (int)pixel2.G) < 5 && Math.Abs((int)pixel1.B - (int)pixel2.B) < 5;
                    }
                    catch (Exception ex)
                    {
                         int num3 = (int)MessageBox.Show(ex.Message);
                         Console.WriteLine(ex.StackTrace);
                    }
                    finally
                    {
                         bitmap1?.Dispose();
                    }
               }
               return false;
          }
          private void button1_Click(object sender, EventArgs e)
          {
               Thread thread1 = new Thread(new ThreadStart(this.iterate1));
               isActive = true;
               thread1.Start();
          }

          public static Color GetColorAt(Point location, ScreenCapture sc)
          {
               RECT lpRect;
               WindowsAPI.GetWindowRect(Form1.h, out lpRect);
               Bitmap screenshot = sc.GetScreenshot(Form1.h, 4100, 2800, location.X - lpRect.Left, location.Y - lpRect.Top, location.X - lpRect.Left + 10, location.Y - lpRect.Top + 10);
               Color pixel = screenshot.GetPixel(0, 0);
               screenshot.Dispose();
               return pixel;
          }

          private void button2_Click(object sender, EventArgs e)
          {
               Thread thred = Thread.CurrentThread;
               thred.Abort();
          }

          private void btnSave_Click(object sender, EventArgs e)
          {
               Configuration.SaveToFile(Configuration.configrationString);
          }

          private void readConfigButton_Click(object sender, EventArgs e)
          {
               Configuration.ReadFromFile();
               int num = (int)MessageBox.Show(string.Join(",",Configuration.configrationString), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
     }
}
