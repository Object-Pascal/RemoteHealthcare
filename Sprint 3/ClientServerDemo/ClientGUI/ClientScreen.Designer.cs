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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tbMessageHistory = new System.Windows.Forms.TextBox();
            this.txtSendMessage = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.time = new System.Windows.Forms.Timer(this.components);
            this.timePassed = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
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
            this.tbMessageHistory.Size = new System.Drawing.Size(336, 88);
            this.tbMessageHistory.TabIndex = 20;
            // 
            // txtSendMessage
            // 
            this.txtSendMessage.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtSendMessage.Location = new System.Drawing.Point(11, 411);
            this.txtSendMessage.Margin = new System.Windows.Forms.Padding(2);
            this.txtSendMessage.Name = "txtSendMessage";
            this.txtSendMessage.Size = new System.Drawing.Size(336, 20);
            this.txtSendMessage.TabIndex = 19;
            this.txtSendMessage.Text = "Stuur bericht ...";
            this.txtSendMessage.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtSendMessage_KeyUp);
            this.txtSendMessage.MouseEnter += new System.EventHandler(this.TxtSendMessage_Enter);
            this.txtSendMessage.MouseLeave += new System.EventHandler(this.TxtSendMessage_Leave);
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(352, 12);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "BikeSpeed";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "HeartRate";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(340, 394);
            this.chart1.TabIndex = 21;
            this.chart1.Text = "chart1";
            // 
            // time
            // 
            this.time.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // timePassed
            // 
            this.timePassed.Location = new System.Drawing.Point(352, 411);
            this.timePassed.Name = "timePassed";
            this.timePassed.Size = new System.Drawing.Size(100, 20);
            this.timePassed.TabIndex = 24;
            // 
            // ClientScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 441);
            this.Controls.Add(this.timePassed);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.tbMessageHistory);
            this.Controls.Add(this.txtSendMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ClientScreen";
            this.Text = "ClientForm";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbMessageHistory;
        private System.Windows.Forms.TextBox txtSendMessage;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Timer time;
        private System.Windows.Forms.TextBox timePassed;
    }
}