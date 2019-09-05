namespace FietsDemoUI
{
    partial class MainForm
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
            this.lstBikes = new System.Windows.Forms.ListBox();
            this.lstHearts = new System.Windows.Forms.ListBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.btnSimulator = new System.Windows.Forms.Button();
            this.lblSimPercentage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstBikes
            // 
            this.lstBikes.FormattingEnabled = true;
            this.lstBikes.Location = new System.Drawing.Point(12, 35);
            this.lstBikes.Name = "lstBikes";
            this.lstBikes.Size = new System.Drawing.Size(120, 186);
            this.lstBikes.TabIndex = 0;
            this.lstBikes.SelectedIndexChanged += new System.EventHandler(this.LstBikes_SelectedIndexChanged);
            // 
            // lstHearts
            // 
            this.lstHearts.FormattingEnabled = true;
            this.lstHearts.Location = new System.Drawing.Point(12, 230);
            this.lstHearts.Name = "lstHearts";
            this.lstHearts.Size = new System.Drawing.Size(120, 199);
            this.lstHearts.TabIndex = 1;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(138, 35);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(102, 25);
            this.lbl1.TabIndex = 2;
            this.lbl1.Text = "Distance:";
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistance.Location = new System.Drawing.Point(246, 35);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(41, 25);
            this.lblDistance.TabIndex = 3;
            this.lblDistance.Text = "0m";
            // 
            // btnSimulator
            // 
            this.btnSimulator.Location = new System.Drawing.Point(12, 9);
            this.btnSimulator.Name = "btnSimulator";
            this.btnSimulator.Size = new System.Drawing.Size(120, 23);
            this.btnSimulator.TabIndex = 4;
            this.btnSimulator.Text = "Run Simulator";
            this.btnSimulator.UseVisualStyleBackColor = true;
            this.btnSimulator.Click += new System.EventHandler(this.BtnSimulator_Click);
            // 
            // lblSimPercentage
            // 
            this.lblSimPercentage.AutoSize = true;
            this.lblSimPercentage.Location = new System.Drawing.Point(140, 14);
            this.lblSimPercentage.Name = "lblSimPercentage";
            this.lblSimPercentage.Size = new System.Drawing.Size(21, 13);
            this.lblSimPercentage.TabIndex = 5;
            this.lblSimPercentage.Text = "0%";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 441);
            this.Controls.Add(this.lblSimPercentage);
            this.Controls.Add(this.btnSimulator);
            this.Controls.Add(this.lblDistance);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.lstHearts);
            this.Controls.Add(this.lstBikes);
            this.Name = "MainForm";
            this.Text = "Fiets Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstBikes;
        private System.Windows.Forms.ListBox lstHearts;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Button btnSimulator;
        private System.Windows.Forms.Label lblSimPercentage;
    }
}

