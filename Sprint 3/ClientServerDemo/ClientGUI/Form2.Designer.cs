namespace ClientGUI
{
    partial class Form2
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
            this.btnSelectSession = new System.Windows.Forms.Button();
            this.lstbSessions = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnSelectSession
            // 
            this.btnSelectSession.Location = new System.Drawing.Point(12, 198);
            this.btnSelectSession.Name = "btnSelectSession";
            this.btnSelectSession.Size = new System.Drawing.Size(438, 103);
            this.btnSelectSession.TabIndex = 0;
            this.btnSelectSession.Text = "Selecteer Sessie";
            this.btnSelectSession.UseVisualStyleBackColor = true;
            this.btnSelectSession.Click += new System.EventHandler(this.BtnSelectSession_Click);
            // 
            // lstbSessions
            // 
            this.lstbSessions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstbSessions.FormattingEnabled = true;
            this.lstbSessions.ItemHeight = 16;
            this.lstbSessions.Location = new System.Drawing.Point(12, 12);
            this.lstbSessions.Name = "lstbSessions";
            this.lstbSessions.Size = new System.Drawing.Size(438, 180);
            this.lstbSessions.TabIndex = 1;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 313);
            this.Controls.Add(this.lstbSessions);
            this.Controls.Add(this.btnSelectSession);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.Text = "Sessies";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelectSession;
        private System.Windows.Forms.ListBox lstbSessions;
    }
}