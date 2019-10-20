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
            this.refreshBttn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AvailableLabel
            // 
            this.AvailableLabel.AutoSize = true;
            this.AvailableLabel.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AvailableLabel.Location = new System.Drawing.Point(25, 10);
            this.AvailableLabel.Name = "AvailableLabel";
            this.AvailableLabel.Size = new System.Drawing.Size(176, 21);
            this.AvailableLabel.TabIndex = 0;
            this.AvailableLabel.Text = "Beschikbare Patienten:";
            // 
            // SelectedLabel
            // 
            this.SelectedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectedLabel.AutoSize = true;
            this.SelectedLabel.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedLabel.Location = new System.Drawing.Point(489, 10);
            this.SelectedLabel.Name = "SelectedLabel";
            this.SelectedLabel.Size = new System.Drawing.Size(195, 21);
            this.SelectedLabel.TabIndex = 1;
            this.SelectedLabel.Text = "Geselecteerde Patienten:";
            // 
            // selectedListView
            // 
            this.selectedListView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedListView.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedListView.HideSelection = false;
            this.selectedListView.Location = new System.Drawing.Point(562, 40);
            this.selectedListView.Name = "selectedListView";
            this.selectedListView.Size = new System.Drawing.Size(0, 0);
            this.selectedListView.TabIndex = 3;
            this.selectedListView.UseCompatibleStateImageBehavior = false;
            // 
            // SelectBtn
            // 
            this.SelectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectBtn.BackColor = System.Drawing.Color.LightBlue;
            this.SelectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SelectBtn.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectBtn.Location = new System.Drawing.Point(358, 56);
            this.SelectBtn.Name = "SelectBtn";
            this.SelectBtn.Size = new System.Drawing.Size(105, 27);
            this.SelectBtn.TabIndex = 4;
            this.SelectBtn.Text = "Selecteer";
            this.SelectBtn.UseVisualStyleBackColor = false;
            this.SelectBtn.Click += new System.EventHandler(this.SelectBtn_Click);
            // 
            // deselectBtn
            // 
            this.deselectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.deselectBtn.BackColor = System.Drawing.Color.LightBlue;
            this.deselectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.deselectBtn.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deselectBtn.Location = new System.Drawing.Point(358, 89);
            this.deselectBtn.Name = "deselectBtn";
            this.deselectBtn.Size = new System.Drawing.Size(105, 27);
            this.deselectBtn.TabIndex = 5;
            this.deselectBtn.Text = "Deselecteer";
            this.deselectBtn.UseVisualStyleBackColor = false;
            this.deselectBtn.Click += new System.EventHandler(this.DeselectBtn_Click);
            // 
            // LayoutPanelClient
            // 
            this.LayoutPanelClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanelClient.AutoScroll = true;
            this.LayoutPanelClient.AutoScrollMinSize = new System.Drawing.Size(30, 100);
            this.LayoutPanelClient.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LayoutPanelClient.Location = new System.Drawing.Point(28, 220);
            this.LayoutPanelClient.Name = "LayoutPanelClient";
            this.LayoutPanelClient.Padding = new System.Windows.Forms.Padding(2);
            this.LayoutPanelClient.Size = new System.Drawing.Size(849, 269);
            this.LayoutPanelClient.TabIndex = 6;
            // 
            // PatientLabel
            // 
            this.PatientLabel.AutoSize = true;
            this.PatientLabel.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PatientLabel.Location = new System.Drawing.Point(26, 210);
            this.PatientLabel.Name = "PatientLabel";
            this.PatientLabel.Size = new System.Drawing.Size(60, 15);
            this.PatientLabel.TabIndex = 7;
            this.PatientLabel.Text = "Patienten:";
            // 
            // BroadcastTextBox
            // 
            this.BroadcastTextBox.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BroadcastTextBox.ForeColor = System.Drawing.Color.Gray;
            this.BroadcastTextBox.Location = new System.Drawing.Point(28, 161);
            this.BroadcastTextBox.Name = "BroadcastTextBox";
            this.BroadcastTextBox.Size = new System.Drawing.Size(388, 23);
            this.BroadcastTextBox.TabIndex = 8;
            this.BroadcastTextBox.Text = "Typ het uitzendbericht:";
            this.BroadcastTextBox.Enter += new System.EventHandler(this.BroadcastTextBox_Enter);
            this.BroadcastTextBox.Leave += new System.EventHandler(this.BroadcastTextBox_Leave);
            // 
            // BroadcastBtn
            // 
            this.BroadcastBtn.BackColor = System.Drawing.Color.LightBlue;
            this.BroadcastBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BroadcastBtn.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BroadcastBtn.Location = new System.Drawing.Point(437, 160);
            this.BroadcastBtn.Name = "BroadcastBtn";
            this.BroadcastBtn.Size = new System.Drawing.Size(87, 23);
            this.BroadcastBtn.TabIndex = 9;
            this.BroadcastBtn.Text = "Uitzenden";
            this.BroadcastBtn.UseVisualStyleBackColor = false;
            this.BroadcastBtn.Click += new System.EventHandler(this.BroadcastBtn_Click);
            // 
            // availableListBox
            // 
            this.availableListBox.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.availableListBox.FormattingEnabled = true;
            this.availableListBox.ItemHeight = 15;
            this.availableListBox.Location = new System.Drawing.Point(29, 41);
            this.availableListBox.Name = "availableListBox";
            this.availableListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.availableListBox.Size = new System.Drawing.Size(306, 94);
            this.availableListBox.TabIndex = 10;
            // 
            // selectedListBox
            // 
            this.selectedListBox.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedListBox.FormattingEnabled = true;
            this.selectedListBox.ItemHeight = 15;
            this.selectedListBox.Location = new System.Drawing.Point(493, 41);
            this.selectedListBox.Name = "selectedListBox";
            this.selectedListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.selectedListBox.Size = new System.Drawing.Size(306, 94);
            this.selectedListBox.TabIndex = 11;
            // 
            // refreshBttn
            // 
            this.refreshBttn.AccessibleName = "RefreshBttn";
            this.refreshBttn.BackColor = System.Drawing.Color.LightCyan;
            this.refreshBttn.BackgroundImage = global::Doctor.Properties.Resources.refreshBttn;
            this.refreshBttn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.refreshBttn.Location = new System.Drawing.Point(305, 10);
            this.refreshBttn.Name = "refreshBttn";
            this.refreshBttn.Size = new System.Drawing.Size(30, 30);
            this.refreshBttn.TabIndex = 12;
            this.refreshBttn.UseVisualStyleBackColor = false;
            this.refreshBttn.Click += new System.EventHandler(this.RefreshBttn_Click);
            // 
            // DokterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(895, 501);
            this.Controls.Add(this.refreshBttn);
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
        private System.Windows.Forms.Button refreshBttn;
    }
}

