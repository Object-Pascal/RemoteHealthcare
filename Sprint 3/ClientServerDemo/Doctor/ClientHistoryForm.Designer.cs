namespace Doctor
{
    partial class ClientHistoryForm
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
            this.lblGender = new System.Windows.Forms.Label();
            this.lblPantiëntKey = new System.Windows.Forms.Label();
            this.lblBirthDate = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Speed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TraveldDistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(12, 43);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(56, 17);
            this.lblGender.TabIndex = 21;
            this.lblGender.Text = "Gender";
            this.lblGender.Click += new System.EventHandler(this.LblGender_Click);
            // 
            // lblPantiëntKey
            // 
            this.lblPantiëntKey.AutoSize = true;
            this.lblPantiëntKey.Location = new System.Drawing.Point(12, 60);
            this.lblPantiëntKey.Name = "lblPantiëntKey";
            this.lblPantiëntKey.Size = new System.Drawing.Size(84, 17);
            this.lblPantiëntKey.TabIndex = 20;
            this.lblPantiëntKey.Text = "PantiëntKey";
            this.lblPantiëntKey.Click += new System.EventHandler(this.LblPantiëntKey_Click);
            // 
            // lblBirthDate
            // 
            this.lblBirthDate.AutoSize = true;
            this.lblBirthDate.Location = new System.Drawing.Point(12, 26);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(67, 17);
            this.lblBirthDate.TabIndex = 19;
            this.lblBirthDate.Text = "BirthDate";
            this.lblBirthDate.Click += new System.EventHandler(this.LblBirthDate_Click);
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(45, 17);
            this.lblName.TabIndex = 18;
            this.lblName.Text = "Name";
            this.lblName.Click += new System.EventHandler(this.LblName_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(15, 80);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(240, 22);
            this.dateTimePicker1.TabIndex = 23;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Speed,
            this.TraveldDistance});
            this.dataGridView2.Location = new System.Drawing.Point(15, 108);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(500, 400);
            this.dataGridView2.TabIndex = 24;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView2_CellContentClick);
            // 
            // Time
            // 
            this.Time.HeaderText = "Tijd";
            this.Time.MinimumWidth = 6;
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // Speed
            // 
            this.Speed.HeaderText = "Snelheid";
            this.Speed.MinimumWidth = 6;
            this.Speed.Name = "Speed";
            this.Speed.ReadOnly = true;
            // 
            // TraveldDistance
            // 
            this.TraveldDistance.HeaderText = "Afgelegde Weg";
            this.TraveldDistance.MinimumWidth = 6;
            this.TraveldDistance.Name = "TraveldDistance";
            this.TraveldDistance.ReadOnly = true;
            // 
            // ClientHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 518);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.lblPantiëntKey);
            this.Controls.Add(this.lblBirthDate);
            this.Controls.Add(this.lblName);
            this.Name = "ClientHistoryForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblPantiëntKey;
        private System.Windows.Forms.Label lblBirthDate;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Speed;
        private System.Windows.Forms.DataGridViewTextBoxColumn TraveldDistance;
    }
}