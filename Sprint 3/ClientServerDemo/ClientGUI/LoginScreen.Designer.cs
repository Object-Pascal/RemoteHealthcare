namespace ClientGUI
{
    partial class LoginScreen
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
            this.login = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.TextBox();
            this.patientNumber = new System.Windows.Forms.TextBox();
            this.unknownNumber = new System.Windows.Forms.Label();
            this.selectBike = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // login
            // 
            this.login.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.login.Location = new System.Drawing.Point(299, 260);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(161, 46);
            this.login.TabIndex = 0;
            this.login.Text = "Log-in";
            this.login.UseVisualStyleBackColor = true;
            this.login.Click += new System.EventHandler(this.Login_Click);
            // 
            // name
            // 
            this.name.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.name.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.name.Location = new System.Drawing.Point(299, 164);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(161, 26);
            this.name.TabIndex = 1;
            this.name.Text = "Naam";
            this.name.Enter += new System.EventHandler(this.Name_Enter);
            this.name.Leave += new System.EventHandler(this.Name_Leave);
            // 
            // patientNumber
            // 
            this.patientNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.patientNumber.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.patientNumber.Location = new System.Drawing.Point(299, 196);
            this.patientNumber.Name = "patientNumber";
            this.patientNumber.Size = new System.Drawing.Size(161, 26);
            this.patientNumber.TabIndex = 2;
            this.patientNumber.Text = "Patiëntnummer";
            this.patientNumber.Enter += new System.EventHandler(this.PatientNumber_Enter);
            this.patientNumber.Leave += new System.EventHandler(this.PatientNumber_Leave);
            // 
            // unknownNumber
            // 
            this.unknownNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.unknownNumber.AutoSize = true;
            this.unknownNumber.ForeColor = System.Drawing.Color.Red;
            this.unknownNumber.Location = new System.Drawing.Point(271, 309);
            this.unknownNumber.Name = "unknownNumber";
            this.unknownNumber.Size = new System.Drawing.Size(209, 20);
            this.unknownNumber.TabIndex = 3;
            this.unknownNumber.Text = "Patiëntnummer bestaat niet!";
            this.unknownNumber.Visible = false;
            // 
            // selectBike
            // 
            this.selectBike.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectBike.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectBike.FormattingEnabled = true;
            this.selectBike.Location = new System.Drawing.Point(299, 228);
            this.selectBike.Name = "selectBike";
            this.selectBike.Size = new System.Drawing.Size(161, 25);
            this.selectBike.TabIndex = 6;
            this.selectBike.Text = "Selecteer een fiets";
            // 
            // LoginScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.selectBike);
            this.Controls.Add(this.unknownNumber);
            this.Controls.Add(this.patientNumber);
            this.Controls.Add(this.name);
            this.Controls.Add(this.login);
            this.Name = "LoginScreen";
            this.Text = "LoginScreen";
            this.Load += new System.EventHandler(this.LoginScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button login;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox patientNumber;
        private System.Windows.Forms.Label unknownNumber;
        private System.Windows.Forms.ComboBox selectBike;
    }
}