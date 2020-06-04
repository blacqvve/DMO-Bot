namespace BotTestForms
{
     partial class TaskManagerForm
     {
          /// <summary>
          /// Required designer variable.
          /// </summary>
          private System.ComponentModel.IContainer components = null;

          /// <summary>
          /// Clean up any resources being used.
          /// </summary>
          /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
          protected override void Dispose(bool disposing)
          {
               if (disposing && (components != null))
               {
                    components.Dispose();
               }
               base.Dispose(disposing);
          }

          #region Windows Form Designer generated code

          /// <summary>
          /// Required method for Designer support - do not modify
          /// the contents of this method with the code editor.
          /// </summary>
          private void InitializeComponent()
          {
               this.groupBox1 = new System.Windows.Forms.GroupBox();
               this.listBox1 = new System.Windows.Forms.ListBox();
               this.selectTaskButton = new System.Windows.Forms.Button();
               this.groupBox1.SuspendLayout();
               this.SuspendLayout();
               // 
               // groupBox1
               // 
               this.groupBox1.BackColor = System.Drawing.Color.Ivory;
               this.groupBox1.Controls.Add(this.listBox1);
               this.groupBox1.Location = new System.Drawing.Point(12, 24);
               this.groupBox1.Name = "groupBox1";
               this.groupBox1.Size = new System.Drawing.Size(249, 378);
               this.groupBox1.TabIndex = 1;
               this.groupBox1.TabStop = false;
               this.groupBox1.Text = "Select Task";
               // 
               // listBox1
               // 
               this.listBox1.FormattingEnabled = true;
               this.listBox1.Location = new System.Drawing.Point(6, 19);
               this.listBox1.Name = "listBox1";
               this.listBox1.Size = new System.Drawing.Size(237, 329);
               this.listBox1.TabIndex = 0;
               // 
               // selectTaskButton
               // 
               this.selectTaskButton.Location = new System.Drawing.Point(91, 415);
               this.selectTaskButton.Name = "selectTaskButton";
               this.selectTaskButton.Size = new System.Drawing.Size(75, 23);
               this.selectTaskButton.TabIndex = 1;
               this.selectTaskButton.Text = "Select Task";
               this.selectTaskButton.UseVisualStyleBackColor = true;
               this.selectTaskButton.Click += new System.EventHandler(this.selectTaskButton_Click);
               // 
               // TaskManagerForm
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.BackColor = System.Drawing.SystemColors.WindowText;
               this.ClientSize = new System.Drawing.Size(273, 450);
               this.Controls.Add(this.selectTaskButton);
               this.Controls.Add(this.groupBox1);
               this.Name = "TaskManagerForm";
               this.Text = "Select Digimon Game Task";
               this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaskManagerForm_FormClosing);
               this.groupBox1.ResumeLayout(false);
               this.ResumeLayout(false);

          }

          #endregion

          private System.Windows.Forms.GroupBox groupBox1;
          private System.Windows.Forms.ListBox listBox1;
          private System.Windows.Forms.Button selectTaskButton;
     }
}