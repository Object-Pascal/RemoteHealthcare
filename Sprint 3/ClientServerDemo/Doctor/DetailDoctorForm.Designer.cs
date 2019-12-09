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
            this.buttonResistance = new System.Windows.Forms.Button();
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
            this.chBikeSpeed.Location = new System.Drawing.Point(314, 65);
            this.chBikeSpeed.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chBikeSpeed.Name = "chBikeSpeed";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chBikeSpeed.Series.Add(series1);
            this.chBikeSpeed.Size = new System.Drawing.Size(300, 244);
            this.chBikeSpeed.TabIndex = 3;
            this.chBikeSpeed.Text = "chart1";
            // 
            // btnStartSesion
            // 
            this.btnStartSesion.Location = new System.Drawing.Point(9, 432);
            this.btnStartSesion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStartSesion.Name = "btnStartSesion";
            this.btnStartSesion.Size = new System.Drawing.Size(98, 20);
            this.btnStartSesion.TabIndex = 4;
            this.btnStartSesion.Text = "Start Sessie";
            this.btnStartSesion.UseVisualStyleBackColor = true;
            this.btnStartSesion.Click += new System.EventHandler(this.StartSesion_Click);
            // 
            // btnEmergencyBreak
            // 
            this.btnEmergencyBreak.Location = new System.Drawing.Point(212, 432);
            this.btnEmergencyBreak.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEmergencyBreak.Name = "btnEmergencyBreak";
            this.btnEmergencyBreak.Size = new System.Drawing.Size(98, 20);
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
            this.chHeartRate.Location = new System.Drawing.Point(9, 65);
            this.chHeartRate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chHeartRate.Name = "chHeartRate";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chHeartRate.Series.Add(series2);
            this.chHeartRate.Size = new System.Drawing.Size(300, 244);
            this.chHeartRate.TabIndex = 6;
            this.chHeartRate.Text = "chart2";
            // 
            // btnSendPrivateMessage
            // 
            this.btnSendPrivateMessage.Location = new System.Drawing.Point(314, 406);
            this.btnSendPrivateMessage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSendPrivateMessage.Name = "btnSendPrivateMessage";
            this.btnSendPrivateMessage.Size = new System.Drawing.Size(98, 20);
            this.btnSendPrivateMessage.TabIndex = 10;
            this.btnSendPrivateMessage.Text = "Stuur bericht";
            this.btnSendPrivateMessage.UseVisualStyleBackColor = true;
            this.btnSendPrivateMessage.Click += new System.EventHandler(this.SendPrivateMessage_Click);
            // 
            // tbTextBoxSendMessage
            // 
            this.tbTextBoxSendMessage.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.tbTextBoxSendMessage.Location = new System.Drawing.Point(9, 407);
            this.tbTextBoxSendMessage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbTextBoxSendMessage.Name = "tbTextBoxSendMessage";
            this.tbTextBoxSendMessage.Size = new System.Drawing.Size(301, 20);
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
            this.lblName.Location = new System.Drawing.Point(9, 7);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(36, 13);
            this.lblName.TabIndex = 12;
            this.lblName.Text = "Form2";
            // 
            // btnStopSesion
            // 
            this.btnStopSesion.Location = new System.Drawing.Point(111, 432);
            this.btnStopSesion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStopSesion.Name = "btnStopSesion";
            this.btnStopSesion.Size = new System.Drawing.Size(98, 20);
            this.btnStopSesion.TabIndex = 13;
            this.btnStopSesion.Text = "Stop Sessie";
            this.btnStopSesion.UseVisualStyleBackColor = true;
            this.btnStopSesion.Click += new System.EventHandler(this.StopSession_Click);
            // 
            // lblBirthDate
            // 
            this.lblBirthDate.AutoSize = true;
            this.lblBirthDate.Location = new System.Drawing.Point(9, 21);
            this.lblBirthDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(51, 13);
            this.lblBirthDate.TabIndex = 14;
            this.lblBirthDate.Text = "BirthDate";
            // 
            // lblPantiëntKey
            // 
            this.lblPantiëntKey.AutoSize = true;
            this.lblPantiëntKey.Location = new System.Drawing.Point(9, 49);
            this.lblPantiëntKey.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPantiëntKey.Name = "lblPantiëntKey";
            this.lblPantiëntKey.Size = new System.Drawing.Size(64, 13);
            this.lblPantiëntKey.TabIndex = 15;
            this.lblPantiëntKey.Text = "PantiëntKey";
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(9, 35);
            this.lblGender.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(42, 13);
            this.lblGender.TabIndex = 16;
            this.lblGender.Text = "Gender";
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(516, 10);
            this.btnHistory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(98, 20);
            this.btnHistory.TabIndex = 17;
            this.btnHistory.Text = "Geschiedenis";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.BtnHistory_Click);
            // 
            // tbMessageHistory
            // 
            this.tbMessageHistory.Location = new System.Drawing.Point(9, 314);
            this.tbMessageHistory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbMessageHistory.Multiline = true;
            this.tbMessageHistory.Name = "tbMessageHistory";
            this.tbMessageHistory.ReadOnly = true;
            this.tbMessageHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMessageHistory.Size = new System.Drawing.Size(301, 88);
            this.tbMessageHistory.TabIndex = 18;
            // 
            // trackBarResistance
            // 
            this.trackBarResistance.Location = new System.Drawing.Point(314, 314);
            this.trackBarResistance.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.trackBarResistance.Name = "trackBarResistance";
            this.trackBarResistance.Size = new System.Drawing.Size(299, 45);
            this.trackBarResistance.TabIndex = 100;
            // 
            // buttonResistance
            // 
            this.buttonResistance.Location = new System.Drawing.Point(314, 340);
            this.buttonResistance.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonResistance.Name = "buttonResistance";
            this.buttonResistance.Size = new System.Drawing.Size(120, 20);
            this.buttonResistance.TabIndex = 101;
            this.buttonResistance.Text = "Change Resistance";
            this.buttonResistance.UseVisualStyleBackColor = true;
            this.buttonResistance.Click += new System.EventHandler(this.buttonResistance_Click);
            // 
            // DetailDoctorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 457);
            this.Controls.Add(this.buttonResistance);
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
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        private System.Windows.Forms.Button buttonResistance;
    }
}