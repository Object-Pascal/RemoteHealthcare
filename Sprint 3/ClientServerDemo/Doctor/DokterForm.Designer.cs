namespace Doctor
{
    partial class DokterForm
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
            this.AvailableLabel = new System.Windows.Forms.Label();
            this.SelectedLabel = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.listView2 = new System.Windows.Forms.ListView();
            this.SelectBtn = new System.Windows.Forms.Button();
            this.deselectBtn = new System.Windows.Forms.Button();
            this.LayoutPanelClient = new System.Windows.Forms.FlowLayoutPanel();
            this.PatientLabel = new System.Windows.Forms.Label();
            this.BroadcastTextBox = new System.Windows.Forms.TextBox();
            this.BroadcastBtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.LayoutPanelClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // AvailableLabel
            // 
            this.AvailableLabel.AutoSize = true;
            this.AvailableLabel.Location = new System.Drawing.Point(30, 20);
            this.AvailableLabel.Name = "AvailableLabel";
            this.AvailableLabel.Size = new System.Drawing.Size(126, 15);
            this.AvailableLabel.TabIndex = 0;
            this.AvailableLabel.Text = "Beschikbare Patienten:";
            // 
            // SelectedLabel
            // 
            this.SelectedLabel.AutoSize = true;
            this.SelectedLabel.Location = new System.Drawing.Point(494, 20);
            this.SelectedLabel.Name = "SelectedLabel";
            this.SelectedLabel.Size = new System.Drawing.Size(136, 15);
            this.SelectedLabel.TabIndex = 1;
            this.SelectedLabel.Text = "Geselecteerde Patienten:";
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(28, 38);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(305, 100);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // listView2
            // 
            this.listView2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(492, 38);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(306, 100);
            this.listView2.TabIndex = 3;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // SelectBtn
            // 
            this.SelectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectBtn.Location = new System.Drawing.Point(362, 58);
            this.SelectBtn.Name = "SelectBtn";
            this.SelectBtn.Size = new System.Drawing.Size(105, 27);
            this.SelectBtn.TabIndex = 4;
            this.SelectBtn.Text = "Selecteer";
            this.SelectBtn.UseVisualStyleBackColor = true;
            this.SelectBtn.Click += new System.EventHandler(this.SelectBtn_Click);
            // 
            // deselectBtn
            // 
            this.deselectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.deselectBtn.Location = new System.Drawing.Point(362, 91);
            this.deselectBtn.Name = "deselectBtn";
            this.deselectBtn.Size = new System.Drawing.Size(105, 27);
            this.deselectBtn.TabIndex = 5;
            this.deselectBtn.Text = "Deselecteer";
            this.deselectBtn.UseVisualStyleBackColor = true;
            // 
            // LayoutPanelClient
            // 
            this.LayoutPanelClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanelClient.AutoScroll = true;
            this.LayoutPanelClient.AutoScrollMinSize = new System.Drawing.Size(30, 100);
            this.LayoutPanelClient.Controls.Add(this.button2);
            this.LayoutPanelClient.Location = new System.Drawing.Point(28, 183);
            this.LayoutPanelClient.Name = "LayoutPanelClient";
            this.LayoutPanelClient.Padding = new System.Windows.Forms.Padding(2);
            this.LayoutPanelClient.Size = new System.Drawing.Size(787, 187);
            this.LayoutPanelClient.TabIndex = 6;
            // 
            // PatientLabel
            // 
            this.PatientLabel.AutoSize = true;
            this.PatientLabel.Location = new System.Drawing.Point(30, 165);
            this.PatientLabel.Name = "PatientLabel";
            this.PatientLabel.Size = new System.Drawing.Size(60, 15);
            this.PatientLabel.TabIndex = 7;
            this.PatientLabel.Text = "Patienten:";
            // 
            // BroadcastTextBox
            // 
            this.BroadcastTextBox.Location = new System.Drawing.Point(33, 395);
            this.BroadcastTextBox.Name = "BroadcastTextBox";
            this.BroadcastTextBox.Size = new System.Drawing.Size(376, 23);
            this.BroadcastTextBox.TabIndex = 8;
            this.BroadcastTextBox.Text = "Typ uw uitzendbericht:";
            // 
            // BroadcastBtn
            // 
            this.BroadcastBtn.Location = new System.Drawing.Point(424, 395);
            this.BroadcastBtn.Name = "BroadcastBtn";
            this.BroadcastBtn.Size = new System.Drawing.Size(75, 23);
            this.BroadcastBtn.TabIndex = 9;
            this.BroadcastBtn.Text = "Uitzenden";
            this.BroadcastBtn.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Window;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(5, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 82);
            this.button2.TabIndex = 15;
            this.button2.Text = "name: Kirsten Cox\r\nleeftijd: 20 \r\ngeslacht: vrouw\r\nBPM : -";
            this.button2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // DokterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(821, 509);
            this.Controls.Add(this.BroadcastBtn);
            this.Controls.Add(this.BroadcastTextBox);
            this.Controls.Add(this.PatientLabel);
            this.Controls.Add(this.LayoutPanelClient);
            this.Controls.Add(this.deselectBtn);
            this.Controls.Add(this.SelectBtn);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.SelectedLabel);
            this.Controls.Add(this.AvailableLabel);
            this.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DokterForm";
            this.Text = "Dokter";
            this.LayoutPanelClient.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AvailableLabel;
        private System.Windows.Forms.Label SelectedLabel;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.Button SelectBtn;
        private System.Windows.Forms.Button deselectBtn;
        private System.Windows.Forms.FlowLayoutPanel LayoutPanelClient;
        private System.Windows.Forms.Label PatientLabel;
        private System.Windows.Forms.TextBox BroadcastTextBox;
        private System.Windows.Forms.Button BroadcastBtn;
        private System.Windows.Forms.Button button2;
    }
}

