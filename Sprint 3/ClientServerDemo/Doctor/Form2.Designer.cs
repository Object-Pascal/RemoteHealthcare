namespace Doctor
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            ((System.ComponentModel.ISupportInitialize)(this.chBikeSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chHeartRate)).BeginInit();
            this.SuspendLayout();
            // 
            // chBikeSpeed
            // 
            chartArea3.Name = "ChartArea1";
            this.chBikeSpeed.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chBikeSpeed.Legends.Add(legend3);
            this.chBikeSpeed.Location = new System.Drawing.Point(418, 80);
            this.chBikeSpeed.Name = "chBikeSpeed";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chBikeSpeed.Series.Add(series3);
            this.chBikeSpeed.Size = new System.Drawing.Size(400, 300);
            this.chBikeSpeed.TabIndex = 3;
            this.chBikeSpeed.Text = "chart1";
            this.chBikeSpeed.Click += new System.EventHandler(this.BikeSpeed_Click);
            // 
            // btnStartSesion
            // 
            this.btnStartSesion.Location = new System.Drawing.Point(12, 417);
            this.btnStartSesion.Name = "btnStartSesion";
            this.btnStartSesion.Size = new System.Drawing.Size(130, 25);
            this.btnStartSesion.TabIndex = 4;
            this.btnStartSesion.Text = "Start Sessie";
            this.btnStartSesion.UseVisualStyleBackColor = true;
            this.btnStartSesion.Click += new System.EventHandler(this.StartSesion_Click);
            // 
            // btnEmergencyBreak
            // 
            this.btnEmergencyBreak.Location = new System.Drawing.Point(282, 417);
            this.btnEmergencyBreak.Name = "btnEmergencyBreak";
            this.btnEmergencyBreak.Size = new System.Drawing.Size(130, 25);
            this.btnEmergencyBreak.TabIndex = 5;
            this.btnEmergencyBreak.Text = "Noodstop";
            this.btnEmergencyBreak.UseVisualStyleBackColor = true;
            this.btnEmergencyBreak.Click += new System.EventHandler(this.EmergencyBreak_Click);
            // 
            // chHeartRate
            // 
            chartArea4.Name = "ChartArea1";
            this.chHeartRate.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chHeartRate.Legends.Add(legend4);
            this.chHeartRate.Location = new System.Drawing.Point(12, 80);
            this.chHeartRate.Name = "chHeartRate";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chHeartRate.Series.Add(series4);
            this.chHeartRate.Size = new System.Drawing.Size(400, 300);
            this.chHeartRate.TabIndex = 6;
            this.chHeartRate.Text = "chart2";
            this.chHeartRate.Click += new System.EventHandler(this.HeartRate_Click);
            // 
            // btnSendPrivateMessage
            // 
            this.btnSendPrivateMessage.Location = new System.Drawing.Point(418, 386);
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
            this.tbTextBoxSendMessage.Location = new System.Drawing.Point(12, 386);
            this.tbTextBoxSendMessage.Multiline = true;
            this.tbTextBoxSendMessage.Name = "tbTextBoxSendMessage";
            this.tbTextBoxSendMessage.Size = new System.Drawing.Size(400, 25);
            this.tbTextBoxSendMessage.TabIndex = 11;
            this.tbTextBoxSendMessage.Text = "Stuur bericht ...";
            this.tbTextBoxSendMessage.Enter += new System.EventHandler(this.TextBoxSendMessage_Enter);
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
            this.lblName.Click += new System.EventHandler(this.Name_Click);
            // 
            // btnStopSesion
            // 
            this.btnStopSesion.Location = new System.Drawing.Point(148, 417);
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
            this.lblBirthDate.Click += new System.EventHandler(this.BirthDate_Click);
            // 
            // lblPantiëntKey
            // 
            this.lblPantiëntKey.AutoSize = true;
            this.lblPantiëntKey.Location = new System.Drawing.Point(12, 60);
            this.lblPantiëntKey.Name = "lblPantiëntKey";
            this.lblPantiëntKey.Size = new System.Drawing.Size(84, 17);
            this.lblPantiëntKey.TabIndex = 15;
            this.lblPantiëntKey.Text = "PantiëntKey";
            this.lblPantiëntKey.Click += new System.EventHandler(this.PantiëntKey_Click);
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Location = new System.Drawing.Point(12, 43);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(56, 17);
            this.lblGender.TabIndex = 16;
            this.lblGender.Text = "Gender";
            this.lblGender.Click += new System.EventHandler(this.Gender_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 453);
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
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chBikeSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chHeartRate)).EndInit();
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
    }
}