namespace Doctor
{
    partial class DetailDoctorForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chBikeSpeed = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnStartSesion = new System.Windows.Forms.Button();
            this.btnEmergencyBreak = new System.Windows.Forms.Button();
            this.chHeartRate = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnSendPrivateMessage = new System.Windows.Forms.Button();
            this.tbTextBoxSendMessage = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnStopSesion = new System.Windows.Forms.Button();
            this.lblBirthDate = new System.Windows.Forms.Label();
            this.lblPantiëntKey = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.btnHistory = new System.Windows.Forms.Button();
            this.tbMessageHistory = new System.Windows.Forms.TextBox();
            this.trackBarResistance = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.chBikeSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chHeartRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarResistance)).BeginInit();
            this.SuspendLayout();
            // 
            // chBikeSpeed
            // 
            chartArea1.Name = "ChartArea1";
            this.chBikeSpeed.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chBikeSpeed.Legends.Add(legend1);
            this.chBikeSpeed.Location = new System.Drawing.Point(418, 80);
            this.chBikeSpeed.Name = "chBikeSpeed";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chBikeSpeed.Series.Add(series1);
            this.chBikeSpeed.Size = new System.Drawing.Size(400, 300);
            this.chBikeSpeed.TabIndex = 3;
            this.chBikeSpeed.Text = "chart1";
            // 
            // btnStartSesion
            // 
            this.btnStartSesion.Location = new System.Drawing.Point(12, 532);
            this.btnStartSesion.Name = "btnStartSesion";
            this.btnStartSesion.Size = new System.Drawing.Size(130, 25);
            this.btnStartSesion.TabIndex = 4;
            this.btnStartSesion.Text = "Start Sessie";
            this.btnStartSesion.UseVisualStyleBackColor = true;
            this.btnStartSesion.Click += new System.EventHandler(this.StartSesion_Click);
            // 
            // btnEmergencyBreak
            // 
            this.btnEmergencyBreak.Location = new System.Drawing.Point(282, 532);
            this.btnEmergencyBreak.Name = "btnEmergencyBreak";
            this.btnEmergencyBreak.Size = new System.Drawing.Size(130, 25);
            this.btnEmergencyBreak.TabIndex = 5;
            this.btnEmergencyBreak.Text = "Noodstop";
            this.btnEmergencyBreak.UseVisualStyleBackColor = true;
            this.btnEmergencyBreak.Click += new System.EventHandler(this.EmergencyBreak_Click);
            // 
            // chHeartRate
            // 
            chartArea2.Name = "ChartArea1";
            this.chHeartRate.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chHeartRate.Legends.Add(legend2);
            this.chHeartRate.Location = new System.Drawing.Point(12, 80);
            this.chHeartRate.Name = "chHeartRate";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chHeartRate.Series.Add(series2);
            this.chHeartRate.Size = new System.Drawing.Size(400, 300);
            this.chHeartRate.TabIndex = 6;
            this.chHeartRate.Text = "chart2";
            // 
            // btnSendPrivateMessage
            // 
            this.btnSendPrivateMessage.Location = new System.Drawing.Point(418, 500);
            this.btnSendPrivateMessage.Name = "btnSendPrivateMessage";
            this.btnSendPrivateMessage.Size = new System.Drawing.Size(130, 25);
            this.btnSendPrivateMessage.TabIndex = 10;
            this.btnSendPrivateMessage.Text = "Stuur bericht";
            this.btnSendPrivateMessage.UseVisualStyleBackColor = true;
            this.btnSendPrivateMessage.Click += new System.EventHandler(this.SendPrivateMessage_Click);
            // 
            // tbTextBoxSendMessage
            // 
            this.tbTextBoxSendMessage.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.tbTextBoxSendMessage.Location = new System.Drawing.Point(12, 501);
            this.tbTextBoxSendMessage.Name = "tbTextBoxSendMessage";
            this.tbTextBoxSendMessage.Size = new System.Drawing.Size(400, 22);
            this.tbTextBoxSendMessage.TabIndex = 11;
            this.tbTextBoxSendMessage.Text = "Stuur bericht ...";
            this.tbTextBoxSendMessage.Enter += new System.EventHandler(this.TextBoxSendMessage_Enter);
            this.tbTextBoxSendMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxSendMessage_KeyDown);
            this.tbTextBoxSendMessage.Leave += new System.EventHandler(this.TextBoxSendMessage_Leave);
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(48, 17);
            this.lblName.TabIndex = 12;
            this.lblName.Text = "Form2";
            // 
            // btnStopSesion
            // 
            this.btnStopSesion.Location = new System.Drawing.Point(148, 532);
            this.btnStopSesion.Name = "btnStopSesion";
            this.btnStopSesion.Size = new System.Drawing.Size(130, 25);
            this.btnStopSesion.TabIndex = 13;
            this.btnStopSesion.Text = "Stop Sessie";
            this.btnStopSesion.UseVisualStyleBackColor = true;
            this.btnStopSesion.Click += new System.EventHandler(this.StopSession_Click);
            // 
            // lblBirthDate
            // 
            this.lblBirthDate.AutoSize = true;
            this.lblBirthDate.Location = new System.Drawing.Point(12, 26);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(67, 17);
            this.lblBirthDate.TabIndex = 14;
            this.lblBirthDate.Text = "BirthDate";
            // 
            // lblPantiëntKey
            // 
            this.lblPantiëntKey.AutoSize = true;
            this.lblPantiëntKey.Location = new System.Drawing.Point(12, 60);
            this.lblPantiëntKey.Name = "lblPantiëntKey";
            this.lblPantiëntKey.Size = new System.Drawing.Size(84, 17);
            this.lblPantiëntKey.TabIndex = 15;
            this.lblPantiëntKey.Text = "PantiëntKey";
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(12, 43);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(56, 17);
            this.lblGender.TabIndex = 16;
            this.lblGender.Text = "Gender";
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(688, 12);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(130, 25);
            this.btnHistory.TabIndex = 17;
            this.btnHistory.Text = "Geschiedenis";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.BtnHistory_Click);
            // 
            // tbMessageHistory
            // 
            this.tbMessageHistory.Location = new System.Drawing.Point(12, 387);
            this.tbMessageHistory.Multiline = true;
            this.tbMessageHistory.Name = "tbMessageHistory";
            this.tbMessageHistory.ReadOnly = true;
            this.tbMessageHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMessageHistory.Size = new System.Drawing.Size(400, 108);
            this.tbMessageHistory.TabIndex = 18;
            // 
            // trackBar1
            // 
            this.trackBarResistance.Location = new System.Drawing.Point(419, 387);
            this.trackBarResistance.Name = "trackBarResistance";
            this.trackBarResistance.Size = new System.Drawing.Size(399, 56);
            this.trackBarResistance.TabIndex = 100;
            // 
            // DetailDoctorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 563);
            this.Controls.Add(this.trackBarResistance);
            this.Controls.Add(this.tbMessageHistory);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.lblPantiëntKey);
            this.Controls.Add(this.lblBirthDate);
            this.Controls.Add(this.btnStopSesion);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.tbTextBoxSendMessage);
            this.Controls.Add(this.btnSendPrivateMessage);
            this.Controls.Add(this.chHeartRate);
            this.Controls.Add(this.btnEmergencyBreak);
            this.Controls.Add(this.btnStartSesion);
            this.Controls.Add(this.chBikeSpeed);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "DetailDoctorForm";
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chBikeSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chHeartRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarResistance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chBikeSpeed;
        private System.Windows.Forms.Button btnStartSesion;
        private System.Windows.Forms.Button btnEmergencyBreak;
        private System.Windows.Forms.DataVisualization.Charting.Chart chHeartRate;
        private System.Windows.Forms.Button btnSendPrivateMessage;
        private System.Windows.Forms.TextBox tbTextBoxSendMessage;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnStopSesion;
        private System.Windows.Forms.Label lblBirthDate;
        private System.Windows.Forms.Label lblPantiëntKey;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.TextBox tbMessageHistory;
        private System.Windows.Forms.TrackBar trackBarResistance;
    }
}