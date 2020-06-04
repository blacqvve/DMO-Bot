namespace BotTestForms
{
     partial class Form1
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
               this.startBot = new System.Windows.Forms.Button();
               this.button2 = new System.Windows.Forms.Button();
               this.btnSave = new System.Windows.Forms.Button();
               this.readConfigButton = new System.Windows.Forms.Button();
               this.SuspendLayout();
               // 
               // startBot
               // 
               this.startBot.Location = new System.Drawing.Point(414, 169);
               this.startBot.Name = "startBot";
               this.startBot.Size = new System.Drawing.Size(75, 23);
               this.startBot.TabIndex = 0;
               this.startBot.Text = "StartBot";
               this.startBot.UseVisualStyleBackColor = true;
               this.startBot.Click += new System.EventHandler(this.button1_Click);
               // 
               // button2
               // 
               this.button2.Location = new System.Drawing.Point(205, 169);
               this.button2.Name = "button2";
               this.button2.Size = new System.Drawing.Size(75, 23);
               this.button2.TabIndex = 1;
               this.button2.Text = "button2";
               this.button2.UseVisualStyleBackColor = true;
               this.button2.Click += new System.EventHandler(this.button2_Click);
               // 
               // btnSave
               // 
               this.btnSave.Location = new System.Drawing.Point(205, 12);
               this.btnSave.Name = "btnSave";
               this.btnSave.Size = new System.Drawing.Size(75, 23);
               this.btnSave.TabIndex = 2;
               this.btnSave.Text = "Save Button";
               this.btnSave.UseVisualStyleBackColor = true;
               this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
               // 
               // readConfigButton
               // 
               this.readConfigButton.Location = new System.Drawing.Point(414, 12);
               this.readConfigButton.Name = "readConfigButton";
               this.readConfigButton.Size = new System.Drawing.Size(75, 23);
               this.readConfigButton.TabIndex = 3;
               this.readConfigButton.Text = "Read Button";
               this.readConfigButton.UseVisualStyleBackColor = true;
               this.readConfigButton.Click += new System.EventHandler(this.readConfigButton_Click);
               // 
               // Form1
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(800, 450);
               this.Controls.Add(this.readConfigButton);
               this.Controls.Add(this.btnSave);
               this.Controls.Add(this.button2);
               this.Controls.Add(this.startBot);
               this.Name = "Form1";
               this.Text = "Form1";
               this.ResumeLayout(false);

          }

        #endregion

        private System.Windows.Forms.Button startBot;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnSave;
          private System.Windows.Forms.Button readConfigButton;
     }
}

