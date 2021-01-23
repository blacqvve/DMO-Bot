using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotTestForms
{
     public partial class TaskManagerForm : Form
     {
          private string taskName = "";
          public TaskManagerForm(string[] processNames)
          {
               InitializeComponent();
               foreach (string item in processNames)
               {
                    this.listBox1.Items.Add(item);
               }

          }

          public string GetName()
          {
               return this.taskName;
          }

          private void selectTaskButton_Click(object sender, EventArgs e)
          {
               this.taskName = listBox1.SelectedIndex >= 0 ? listBox1.SelectedItem.ToString() : "";

               if (!string.IsNullOrEmpty(taskName))
               {
                    Form1 parent = (Form1)Owner;
                    parent.TaskName = taskName;
                    Close();
               }
               else
               {
                    int num = (int)MessageBox.Show("No process selected. Please select digimon masters online process from list", "Info", MessageBoxButtons.OK, MessageBoxIcon.Hand);
               }
          }

          private void TaskManagerForm_FormClosing(object sender, FormClosingEventArgs e)
          {
               if (e.CloseReason == CloseReason.UserClosing)
               {
                    if (string.IsNullOrEmpty(taskName))
                    {
                         int num = (int)MessageBox.Show("No process selected. application will close now.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         Application.Exit();
                    }
               }
          }
     }
}

