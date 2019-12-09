namespace ClientGUI
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
            this.btnLoadSessions = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Addpanels = new System.Windows.Forms.Button();
            this.StopStart = new System.Windows.Forms.Button();
            this.AddRoute = new System.Windows.Forms.Button();
            this.sendToPanelBtn = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbRecieveMessage = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadSessions
            // 
            this.btnLoadSessions.Location = new System.Drawing.Point(4, 5);
            this.btnLoadSessions.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoadSessions.Name = "btnLoadSessions";
            this.btnLoadSessions.Size = new System.Drawing.Size(87, 19);
            this.btnLoadSessions.TabIndex = 0;
            this.btnLoadSessions.Text = "Load Sessions";
            this.btnLoadSessions.UseVisualStyleBackColor = true;
            this.btnLoadSessions.Click += new System.EventHandler(this.Button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(9, 10);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(622, 387);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbRecieveMessage);
            this.tabPage1.Controls.Add(this.Addpanels);
            this.tabPage1.Controls.Add(this.StopStart);
            this.tabPage1.Controls.Add(this.AddRoute);
            this.tabPage1.Controls.Add(this.sendToPanelBtn);
            this.tabPage1.Controls.Add(this.btnLoadSessions);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(614, 361);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.TabPage1_Click);
            // 
            // Addpanels
            // 
            this.Addpanels.Location = new System.Drawing.Point(6, 59);
            this.Addpanels.Name = "Addpanels";
            this.Addpanels.Size = new System.Drawing.Size(75, 23);
            this.Addpanels.TabIndex = 4;
            this.Addpanels.Text = "AddPanels";
            this.Addpanels.UseVisualStyleBackColor = true;
            this.Addpanels.Click += new System.EventHandler(this.Addpanels_Click);
            // 
            // StopStart
            // 
            this.StopStart.Location = new System.Drawing.Point(95, 29);
            this.StopStart.Margin = new System.Windows.Forms.Padding(2);
            this.StopStart.Name = "StopStart";
            this.StopStart.Size = new System.Drawing.Size(87, 24);
            this.StopStart.TabIndex = 3;
            this.StopStart.Tag = "";
            this.StopStart.Text = "Stop/Start";
            this.StopStart.UseVisualStyleBackColor = true;
            this.StopStart.Click += new System.EventHandler(this.Button1_Click_2);
            // 
            // AddRoute
            // 
            this.AddRoute.Location = new System.Drawing.Point(4, 29);
            this.AddRoute.Name = "AddRoute";
            this.AddRoute.Size = new System.Drawing.Size(86, 23);
            this.AddRoute.TabIndex = 2;
            this.AddRoute.Text = "Add Route";
            this.AddRoute.UseVisualStyleBackColor = true;
            this.AddRoute.Click += new System.EventHandler(this.AddRoute_Click);
            // 
            // sendToPanelBtn
            // 
            this.sendToPanelBtn.AccessibleDescription = "sendToPanelBtn";
            this.sendToPanelBtn.AccessibleName = "";
            this.sendToPanelBtn.Location = new System.Drawing.Point(95, 5);
            this.sendToPanelBtn.Margin = new System.Windows.Forms.Padding(2);
            this.sendToPanelBtn.Name = "sendToPanelBtn";
            this.sendToPanelBtn.Size = new System.Drawing.Size(87, 19);
            this.sendToPanelBtn.TabIndex = 1;
            this.sendToPanelBtn.Text = "Send to Panel";
            this.sendToPanelBtn.UseVisualStyleBackColor = true;
            this.sendToPanelBtn.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(614, 361);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbRecieveMessage
            // 
            this.tbRecieveMessage.AccessibleDescription = "tbRecieveMessage";
            this.tbRecieveMessage.AccessibleName = "tbRecieveMessage";
            this.tbRecieveMessage.Location = new System.Drawing.Point(471, 5);
            this.tbRecieveMessage.Multiline = true;
            this.tbRecieveMessage.Name = "tbRecieveMessage";
            this.tbRecieveMessage.Size = new System.Drawing.Size(138, 122);
            this.tbRecieveMessage.TabIndex = 5;
            this.tbRecieveMessage.TextChanged += new System.EventHandler(this.tbRecieveMessage_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 406);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoadSessions;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button sendToPanelBtn;
        private System.Windows.Forms.Button AddRoute;
        private System.Windows.Forms.Button StopStart;
        private System.Windows.Forms.Button Addpanels;
        private System.Windows.Forms.TextBox tbRecieveMessage;
    }
}

