namespace Doctor
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.PatientLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.LayoutPanelClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // AvailableLabel
            // 
            this.AvailableLabel.AutoSize = true;
            this.AvailableLabel.Location = new System.Drawing.Point(41, 20);
            this.AvailableLabel.Name = "AvailableLabel";
            this.AvailableLabel.Size = new System.Drawing.Size(126, 15);
            this.AvailableLabel.TabIndex = 0;
            this.AvailableLabel.Text = "Beschikbare Patienten:";
            // 
            // SelectedLabel
            // 
            this.SelectedLabel.AutoSize = true;
            this.SelectedLabel.Location = new System.Drawing.Point(471, 17);
            this.SelectedLabel.Name = "SelectedLabel";
            this.SelectedLabel.Size = new System.Drawing.Size(136, 15);
            this.SelectedLabel.TabIndex = 1;
            this.SelectedLabel.Text = "Geselecteerde Patienten:";
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(44, 38);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(240, 101);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // listView2
            // 
            this.listView2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(464, 38);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(240, 101);
            this.listView2.TabIndex = 3;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // SelectBtn
            // 
            this.SelectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectBtn.Location = new System.Drawing.Point(330, 58);
            this.SelectBtn.Name = "SelectBtn";
            this.SelectBtn.Size = new System.Drawing.Size(87, 27);
            this.SelectBtn.TabIndex = 4;
            this.SelectBtn.Text = "Selecteer";
            this.SelectBtn.UseVisualStyleBackColor = true;
            // 
            // deselectBtn
            // 
            this.deselectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deselectBtn.Location = new System.Drawing.Point(330, 91);
            this.deselectBtn.Name = "deselectBtn";
            this.deselectBtn.Size = new System.Drawing.Size(87, 27);
            this.deselectBtn.TabIndex = 5;
            this.deselectBtn.Text = "Deselecteer";
            this.deselectBtn.UseVisualStyleBackColor = true;
            // 
            // LayoutPanelClient
            // 
            this.LayoutPanelClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanelClient.Controls.Add(this.button1);
            this.LayoutPanelClient.Controls.Add(this.button2);
            this.LayoutPanelClient.Controls.Add(this.button3);
            this.LayoutPanelClient.Controls.Add(this.button4);
            this.LayoutPanelClient.Controls.Add(this.button5);
            this.LayoutPanelClient.Controls.Add(this.button6);
            this.LayoutPanelClient.Controls.Add(this.button7);
            this.LayoutPanelClient.Controls.Add(this.button8);
            this.LayoutPanelClient.Controls.Add(this.button9);
            this.LayoutPanelClient.Controls.Add(this.button10);
            this.LayoutPanelClient.Location = new System.Drawing.Point(33, 183);
            this.LayoutPanelClient.Name = "LayoutPanelClient";
            this.LayoutPanelClient.Padding = new System.Windows.Forms.Padding(2);
            this.LayoutPanelClient.Size = new System.Drawing.Size(775, 187);
            this.LayoutPanelClient.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Window;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(5, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 82);
            this.button1.TabIndex = 0;
            this.button1.Text = "name: Kirsten Cox\r\nleeftijd: 20 \r\ngeslacht: vrouw\r\nBPM : -";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // PatientLabel
            // 
            this.PatientLabel.AutoSize = true;
            this.PatientLabel.Location = new System.Drawing.Point(47, 165);
            this.PatientLabel.Name = "PatientLabel";
            this.PatientLabel.Size = new System.Drawing.Size(60, 15);
            this.PatientLabel.TabIndex = 7;
            this.PatientLabel.Text = "Patienten:";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Window;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(158, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 82);
            this.button2.TabIndex = 1;
            this.button2.Text = "name: Kirsten Cox\r\nleeftijd: 20 \r\ngeslacht: vrouw\r\nBPM : -";
            this.button2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Window;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(311, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(147, 82);
            this.button3.TabIndex = 2;
            this.button3.Text = "name: Kirsten Cox\r\nleeftijd: 20 \r\ngeslacht: vrouw\r\nBPM : -";
            this.button3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Window;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Location = new System.Drawing.Point(464, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(147, 82);
            this.button4.TabIndex = 3;
            this.button4.Text = "name: Kirsten Cox\r\nleeftijd: 20 \r\ngeslacht: vrouw\r\nBPM : -";
            this.button4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.Window;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Location = new System.Drawing.Point(617, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(147, 82);
            this.button5.TabIndex = 4;
            this.button5.Text = "name: Kirsten Cox\r\nleeftijd: 20 \r\ngeslacht: vrouw\r\nBPM : -";
            this.button5.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.Window;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button6.Location = new System.Drawing.Point(5, 93);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(147, 82);
            this.button6.TabIndex = 5;
            this.button6.Text = "name: Kirsten Cox\r\nleeftijd: 20 \r\ngeslacht: vrouw\r\nBPM : -";
            this.button6.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.Window;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button7.Location = new System.Drawing.Point(158, 93);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(147, 82);
            this.button7.TabIndex = 6;
            this.button7.Text = "name: Kirsten Cox\r\nleeftijd: 20 \r\ngeslacht: vrouw\r\nBPM : -";
            this.button7.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button7.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.SystemColors.Window;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button8.Location = new System.Drawing.Point(311, 93);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(147, 82);
            this.button8.TabIndex = 7;
            this.button8.Text = "name: Kirsten Cox\r\nleeftijd: 20 \r\ngeslacht: vrouw\r\nBPM : -";
            this.button8.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button8.UseVisualStyleBackColor = false;
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.SystemColors.Window;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button9.Location = new System.Drawing.Point(464, 93);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(147, 82);
            this.button9.TabIndex = 8;
            this.button9.Text = "name: Kirsten Cox\r\nleeftijd: 20 \r\ngeslacht: vrouw\r\nBPM : -";
            this.button9.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button9.UseVisualStyleBackColor = false;
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.SystemColors.Window;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button10.Location = new System.Drawing.Point(617, 93);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(147, 82);
            this.button10.TabIndex = 9;
            this.button10.Text = "name: Kirsten Cox\r\nleeftijd: 20 \r\ngeslacht: vrouw\r\nBPM : -";
            this.button10.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button10.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(821, 509);
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
            this.Name = "Form1";
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label PatientLabel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
    }
}

