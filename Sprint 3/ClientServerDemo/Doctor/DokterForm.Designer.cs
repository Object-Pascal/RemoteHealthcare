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
            this.selectedListView = new System.Windows.Forms.ListView();
            this.SelectBtn = new System.Windows.Forms.Button();
            this.deselectBtn = new System.Windows.Forms.Button();
            this.LayoutPanelClient = new System.Windows.Forms.FlowLayoutPanel();
            this.PatientLabel = new System.Windows.Forms.Label();
            this.BroadcastTextBox = new System.Windows.Forms.TextBox();
            this.BroadcastBtn = new System.Windows.Forms.Button();
            this.availableListBox = new System.Windows.Forms.ListBox();
            this.selectedListBox = new System.Windows.Forms.ListBox();
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
            this.SelectedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectedLabel.AutoSize = true;
            this.SelectedLabel.Location = new System.Drawing.Point(494, 20);
            this.SelectedLabel.Name = "SelectedLabel";
            this.SelectedLabel.Size = new System.Drawing.Size(136, 15);
            this.SelectedLabel.TabIndex = 1;
            this.SelectedLabel.Text = "Geselecteerde Patienten:";
            // 
            // selectedListView
            // 
            this.selectedListView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedListView.HideSelection = false;
            this.selectedListView.Location = new System.Drawing.Point(492, 38);
            this.selectedListView.Name = "selectedListView";
            this.selectedListView.Size = new System.Drawing.Size(0, 0);
            this.selectedListView.TabIndex = 3;
            this.selectedListView.UseCompatibleStateImageBehavior = false;
            // 
            // SelectBtn
            // 
            this.SelectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectBtn.Location = new System.Drawing.Point(362, 54);
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
            this.deselectBtn.Location = new System.Drawing.Point(362, 87);
            this.deselectBtn.Name = "deselectBtn";
            this.deselectBtn.Size = new System.Drawing.Size(105, 27);
            this.deselectBtn.TabIndex = 5;
            this.deselectBtn.Text = "Deselecteer";
            this.deselectBtn.UseVisualStyleBackColor = true;
            this.deselectBtn.Click += new System.EventHandler(this.DeselectBtn_Click);
            // 
            // LayoutPanelClient
            // 
            this.LayoutPanelClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanelClient.AutoScroll = true;
            this.LayoutPanelClient.AutoScrollMinSize = new System.Drawing.Size(30, 100);
            this.LayoutPanelClient.Location = new System.Drawing.Point(28, 220);
            this.LayoutPanelClient.Name = "LayoutPanelClient";
            this.LayoutPanelClient.Padding = new System.Windows.Forms.Padding(2);
            this.LayoutPanelClient.Size = new System.Drawing.Size(775, 269);
            this.LayoutPanelClient.TabIndex = 6;
            // 
            // PatientLabel
            // 
            this.PatientLabel.AutoSize = true;
            this.PatientLabel.Location = new System.Drawing.Point(26, 210);
            this.PatientLabel.Name = "PatientLabel";
            this.PatientLabel.Size = new System.Drawing.Size(60, 15);
            this.PatientLabel.TabIndex = 7;
            this.PatientLabel.Text = "Patienten:";
            // 
            // BroadcastTextBox
            // 
            this.BroadcastTextBox.Font = new System.Drawing.Font("Nirmala UI Semilight", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BroadcastTextBox.ForeColor = System.Drawing.Color.Gray;
            this.BroadcastTextBox.Location = new System.Drawing.Point(29, 161);
            this.BroadcastTextBox.Name = "BroadcastTextBox";
            this.BroadcastTextBox.Size = new System.Drawing.Size(388, 23);
            this.BroadcastTextBox.TabIndex = 8;
            this.BroadcastTextBox.Text = "Typ het uitzendbericht:";
            this.BroadcastTextBox.Enter += new System.EventHandler(this.BroadcastTextBox_Enter);
            this.BroadcastTextBox.Leave += new System.EventHandler(this.BroadcastTextBox_Leave);
            // 
            // BroadcastBtn
            // 
            this.BroadcastBtn.Location = new System.Drawing.Point(420, 161);
            this.BroadcastBtn.Name = "BroadcastBtn";
            this.BroadcastBtn.Size = new System.Drawing.Size(87, 23);
            this.BroadcastBtn.TabIndex = 9;
            this.BroadcastBtn.Text = "Uitzenden";
            this.BroadcastBtn.UseVisualStyleBackColor = true;
            this.BroadcastBtn.Click += new System.EventHandler(this.BroadcastBtn_Click);
            // 
            // availableListBox
            // 
            this.availableListBox.FormattingEnabled = true;
            this.availableListBox.ItemHeight = 15;
            this.availableListBox.Location = new System.Drawing.Point(33, 39);
            this.availableListBox.Name = "availableListBox";
            this.availableListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.availableListBox.Size = new System.Drawing.Size(306, 94);
            this.availableListBox.TabIndex = 10;
            // 
            // selectedListBox
            // 
            this.selectedListBox.FormattingEnabled = true;
            this.selectedListBox.ItemHeight = 15;
            this.selectedListBox.Location = new System.Drawing.Point(497, 39);
            this.selectedListBox.Name = "selectedListBox";
            this.selectedListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.selectedListBox.Size = new System.Drawing.Size(306, 94);
            this.selectedListBox.TabIndex = 11;
            // 
            // DokterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(821, 501);
            this.Controls.Add(this.selectedListBox);
            this.Controls.Add(this.availableListBox);
            this.Controls.Add(this.BroadcastBtn);
            this.Controls.Add(this.BroadcastTextBox);
            this.Controls.Add(this.PatientLabel);
            this.Controls.Add(this.LayoutPanelClient);
            this.Controls.Add(this.deselectBtn);
            this.Controls.Add(this.SelectBtn);
            this.Controls.Add(this.selectedListView);
            this.Controls.Add(this.SelectedLabel);
            this.Controls.Add(this.AvailableLabel);
            this.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DokterForm";
            this.Text = "Dokter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AvailableLabel;
        private System.Windows.Forms.Label SelectedLabel;
        private System.Windows.Forms.ListView selectedListView;
        private System.Windows.Forms.Button SelectBtn;
        private System.Windows.Forms.Button deselectBtn;
        private System.Windows.Forms.FlowLayoutPanel LayoutPanelClient;
        private System.Windows.Forms.Label PatientLabel;
        private System.Windows.Forms.TextBox BroadcastTextBox;
        private System.Windows.Forms.Button BroadcastBtn;
        private System.Windows.Forms.ListBox availableListBox;
        private System.Windows.Forms.ListBox selectedListBox;
    }
}

