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
          public string TaskName;
          private  bool _isActive = false;


          private  TaskManagerForm _taskManager;
          private Thread _activeTread;

          private static IntPtr _windowHandle;
          private static IntPtr _childWindowHandle;
          private Process p;
          public Form1()
          {
               InitializeComponent();
               Process[] processes = Process.GetProcesses();
               string[] pnames = new string[processes.Length];
               for (int index = 0; index < pnames.Length; ++index)
                    pnames[index] = processes[index].ProcessName;
               _taskManager = new TaskManagerForm(pnames);
               ConfigrationForm confForm = new ConfigrationForm();
               confForm.Show();

               try
               {
                    this.p = Process.GetProcessesByName("GDMO")[0];
                    this.p.WaitForInputIdle();
                    _windowHandle = this.p.MainWindowHandle;
                    _childWindowHandle = WindowsAPI.FindWindowEx(Form1._windowHandle, IntPtr.Zero, "Edit", (string)null);
               }
               catch (Exception ex1)
               {
                    new Thread(new ThreadStart(FindTaskAsync)).Start();
                    int num1 = (int)MessageBox.Show("DMO Process not found, select one from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    _taskManager.Show();
               }
          }
          private async void FindTaskAsync()
          {
               try
               {
                    int tries = 0;
                    while (string.IsNullOrEmpty(TaskName))
                    {
                         await Task.Delay(1000);
                         ++tries;
                         if (tries > 30)
                              TaskName = "ERR";
                    }
                    this.p = Process.GetProcessesByName(TaskName)[0];
                    this.p.WaitForInputIdle();
                    Form1._windowHandle = this.p.MainWindowHandle;
                    Form1._childWindowHandle = WindowsAPI.FindWindowEx(Form1._windowHandle, IntPtr.Zero, "Edit", (string)null);
               }
               catch (Exception ex)
               {
                    int num = (int)MessageBox.Show("DMO Process not found, or no Administrator permissions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    Application.Exit();
               }
          }
          private async void Hunt()
          {
               while (_isActive)
               {
                    WindowsAPI.SendMessage(Form1._windowHandle, 257U, (IntPtr)9, IntPtr.Zero);
                    WindowsAPI.SendMessage(Form1._windowHandle, 257U, 0x70, IntPtr.Zero);
                   await Task.Delay(300);
               }
          }
          private void button1_Click(object sender, EventArgs e)
          {
               Thread thread1 = new Thread(new ThreadStart(Hunt));
               _isActive = true;
               _activeTread = thread1;
               thread1.Start();
          }
          private void button2_Click(object sender, EventArgs e)
          {
               _isActive = false;
               _activeTread.Abort();
          }
          private void btnSave_Click(object sender, EventArgs e)
          {
               Configuration.SaveToFile(Configuration.configrationString);
          }

          private void readConfigButton_Click(object sender, EventArgs e)
          {
               Configuration.ReadFromFile();
               int num = (int)MessageBox.Show(string.Join(",", Configuration.configrationString), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
     }
}
