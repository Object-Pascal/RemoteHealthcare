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
            this.BikeSpeed = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.HeartRate = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.Name = new System.Windows.Forms.Label();
            this.StopSesion = new System.Windows.Forms.Button();
            this.BirthDate = new System.Windows.Forms.Label();
            this.PantiëntKey = new System.Windows.Forms.Label();
            this.Gender = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BikeSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeartRate)).BeginInit();
            this.SuspendLayout();
            // 
            // BikeSpeed
            // 
            chartArea3.Name = "ChartArea1";
            this.BikeSpeed.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.BikeSpeed.Legends.Add(legend3);
            this.BikeSpeed.Location = new System.Drawing.Point(418, 80);
            this.BikeSpeed.Name = "BikeSpeed";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.BikeSpeed.Series.Add(series3);
            this.BikeSpeed.Size = new System.Drawing.Size(400, 300);
            this.BikeSpeed.TabIndex = 3;
            this.BikeSpeed.Text = "chart1";
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(12, 417);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(130, 25);
            this.Start.TabIndex = 4;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(282, 417);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(130, 25);
            this.Stop.TabIndex = 5;
            this.Stop.Text = "Noodstop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // HeartRate
            // 
            chartArea4.Name = "ChartArea1";
            this.HeartRate.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.HeartRate.Legends.Add(legend4);
            this.HeartRate.Location = new System.Drawing.Point(12, 80);
            this.HeartRate.Name = "HeartRate";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.HeartRate.Series.Add(series4);
            this.HeartRate.Size = new System.Drawing.Size(400, 300);
            this.HeartRate.TabIndex = 6;
            this.HeartRate.Text = "chart2";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(418, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 25);
            this.button1.TabIndex = 10;
            this.button1.Text = "send message";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(12, 386);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(400, 25);
            this.textBox6.TabIndex = 11;
            // 
            // Name
            // 
            this.Name.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Name.AutoSize = true;
            this.Name.Location = new System.Drawing.Point(12, 9);
            this.Name.Name = "Name";
            this.Name.Size = new System.Drawing.Size(45, 17);
            this.Name.TabIndex = 12;
            this.Name.Text = "Name";
            // 
            // StopSesion
            // 
            this.StopSesion.Location = new System.Drawing.Point(148, 417);
            this.StopSesion.Name = "StopSesion";
            this.StopSesion.Size = new System.Drawing.Size(130, 25);
            this.StopSesion.TabIndex = 13;
            this.StopSesion.Text = "StopSesion";
            this.StopSesion.UseVisualStyleBackColor = true;
            this.StopSesion.Click += new System.EventHandler(this.Button2_Click);
            // 
            // BirthDate
            // 
            this.BirthDate.AutoSize = true;
            this.BirthDate.Location = new System.Drawing.Point(12, 26);
            this.BirthDate.Name = "BirthDate";
            this.BirthDate.Size = new System.Drawing.Size(67, 17);
            this.BirthDate.TabIndex = 14;
            this.BirthDate.Text = "BirthDate";
            this.BirthDate.Click += new System.EventHandler(this.BirthDate_Click);
            // 
            // PantiëntKey
            // 
            this.PantiëntKey.AutoSize = true;
            this.PantiëntKey.Location = new System.Drawing.Point(12, 60);
            this.PantiëntKey.Name = "PantiëntKey";
            this.PantiëntKey.Size = new System.Drawing.Size(84, 17);
            this.PantiëntKey.TabIndex = 15;
            this.PantiëntKey.Text = "PantiëntKey";
            // 
            // Gender
            // 
            this.Gender.AutoSize = true;
            this.Gender.Location = new System.Drawing.Point(12, 43);
            this.Gender.Name = "Gender";
            this.Gender.Size = new System.Drawing.Size(56, 17);
            this.Gender.TabIndex = 16;
            this.Gender.Text = "Gender";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 453);
            this.Controls.Add(this.Gender);
            this.Controls.Add(this.PantiëntKey);
            this.Controls.Add(this.BirthDate);
            this.Controls.Add(this.StopSesion);
            this.Controls.Add(this.Name);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.HeartRate);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.BikeSpeed);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BikeSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeartRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart BikeSpeed;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.DataVisualization.Charting.Chart HeartRate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label Name;
        private System.Windows.Forms.Button StopSesion;
        private System.Windows.Forms.Label BirthDate;
        private System.Windows.Forms.Label PantiëntKey;
        private System.Windows.Forms.Label Gender;
    }
}