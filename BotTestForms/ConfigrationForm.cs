using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BotLib;

namespace BotTestForms
{
     public partial class ConfigrationForm : Form
     {
          Utility util = new Utility();
          private string[] keys;
          public ConfigrationForm()
          {
               keys = new string[] { "Select Key", "TAB", "1", "2", "3", "4", "5", "6", "7", "8", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8" };
               InitializeComponent();
               var comboBoxes = util.GetAllFormControls(this, typeof(ComboBox));
               foreach (ComboBox item in comboBoxes)
               {
                    item.Items.AddRange(keys);
                    item.SelectedIndex = 0;
               }
              

          }
          private void saveConfigButton_Click(object sender, EventArgs e)
          {
               string[] conf = new string[13] { "ATTACK:" + this.comboBox2.SelectedItem,
        "SKILL1:" + this.comboBox3.SelectedItem,
        "SKILL2:" + this.comboBox4.SelectedItem,
        "PICKUP:" + this.comboBox7.SelectedItem,
        "CONSUMABLE1:" + this.comboBox5.SelectedItem,
        "CONSUMABLE2:" + this.comboBox6.SelectedItem,
        "TARGET:" + this.comboBox1.SelectedItem,
        "MOVEMENT_INTERVAL:1",
        "MOVEMENT_DURATION:1" ,
        "HP_PERCENT:1" ,
        "DS_PERCENT:1" ,
        "CHANGE_T:1",
        "RETURN_ORIGIN:1"};
               if (Configuration.configure(conf))
               {
                    int num = (int)MessageBox.Show("Configs saved with success!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Close();
               }
               else
               {
                    int num1 = (int)MessageBox.Show("Error applying configuration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
               }
          }
          
     }
}
