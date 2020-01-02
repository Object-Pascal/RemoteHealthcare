namespace ClientGUI
{
    partial class ClientScreen
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
            this.tbMessageHistory = new System.Windows.Forms.TextBox();
            this.txtSendMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbMessageHistory
            // 
            this.tbMessageHistory.Location = new System.Drawing.Point(11, 318);
            this.tbMessageHistory.Margin = new System.Windows.Forms.Padding(2);
            this.tbMessageHistory.Multiline = true;
            this.tbMessageHistory.Name = "tbMessageHistory";
            this.tbMessageHistory.ReadOnly = true;
            this.tbMessageHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMessageHistory.Size = new System.Drawing.Size(301, 88);
            this.tbMessageHistory.TabIndex = 20;
            // 
            // txtSendMessage
            // 
            this.txtSendMessage.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtSendMessage.Location = new System.Drawing.Point(11, 411);
            this.txtSendMessage.Margin = new System.Windows.Forms.Padding(2);
            this.txtSendMessage.Name = "txtSendMessage";
            this.txtSendMessage.Size = new System.Drawing.Size(301, 20);
            this.txtSendMessage.TabIndex = 19;
            this.txtSendMessage.Text = "Stuur bericht ...";
            this.txtSendMessage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtSendMessage_KeyUp);
            this.txtSendMessage.MouseEnter += new System.EventHandler(this.TxtSendMessage_Enter);
            this.txtSendMessage.MouseLeave += new System.EventHandler(this.TxtSendMessage_Leave);
            // 
            // ClientScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 441);
            this.Controls.Add(this.tbMessageHistory);
            this.Controls.Add(this.txtSendMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ClientScreen";
            this.Text = "ClientForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbMessageHistory;
        private System.Windows.Forms.TextBox txtSendMessage;
    }
}