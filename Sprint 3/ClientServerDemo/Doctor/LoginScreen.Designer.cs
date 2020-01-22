namespace Doctor
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
            this.txtDoctorName = new System.Windows.Forms.TextBox();
            this.txtDoctorPassword = new System.Windows.Forms.TextBox();
            this.lblUnknownLogin = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // login
            // 
            this.login.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.login.Location = new System.Drawing.Point(199, 151);
            this.login.Margin = new System.Windows.Forms.Padding(2);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(109, 30);
            this.login.TabIndex = 2;
            this.login.Text = "Log-in";
            this.login.UseVisualStyleBackColor = true;
            this.login.Click += new System.EventHandler(this.Login_Click);
            // 
            // txtDoctorName
            // 
            this.txtDoctorName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDoctorName.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtDoctorName.HideSelection = false;
            this.txtDoctorName.Location = new System.Drawing.Point(199, 107);
            this.txtDoctorName.Margin = new System.Windows.Forms.Padding(2);
            this.txtDoctorName.Name = "txtDoctorName";
            this.txtDoctorName.Size = new System.Drawing.Size(109, 20);
            this.txtDoctorName.TabIndex = 0;
            this.txtDoctorName.Text = "Naam";
            this.txtDoctorName.Enter += new System.EventHandler(this.Name_Enter);
            this.txtDoctorName.Leave += new System.EventHandler(this.Name_Leave);
            // 
            // txtDoctorPassword
            // 
            this.txtDoctorPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDoctorPassword.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtDoctorPassword.Location = new System.Drawing.Point(199, 127);
            this.txtDoctorPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtDoctorPassword.Name = "txtDoctorPassword";
            this.txtDoctorPassword.PasswordChar = '*';
            this.txtDoctorPassword.Size = new System.Drawing.Size(109, 20);
            this.txtDoctorPassword.TabIndex = 1;
            this.txtDoctorPassword.Text = "Wachtwoord";
            this.txtDoctorPassword.Enter += new System.EventHandler(this.PatientNumber_Enter);
            this.txtDoctorPassword.Leave += new System.EventHandler(this.PatientNumber_Leave);
            // 
            // lblUnknownLogin
            // 
            this.lblUnknownLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUnknownLogin.AutoSize = true;
            this.lblUnknownLogin.ForeColor = System.Drawing.Color.Red;
            this.lblUnknownLogin.Location = new System.Drawing.Point(159, 202);
            this.lblUnknownLogin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUnknownLogin.Name = "lblUnknownLogin";
            this.lblUnknownLogin.Size = new System.Drawing.Size(188, 13);
            this.lblUnknownLogin.TabIndex = 3;
            this.lblUnknownLogin.Text = "Kan niet inloggen met deze gegevens!";
            this.lblUnknownLogin.Visible = false;
            // 
            // LoginScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 292);
            this.Controls.Add(this.lblUnknownLogin);
            this.Controls.Add(this.txtDoctorPassword);
            this.Controls.Add(this.txtDoctorName);
            this.Controls.Add(this.login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "LoginScreen";
            this.Text = "LoginScreen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button login;
        private System.Windows.Forms.TextBox txtDoctorName;
        private System.Windows.Forms.TextBox txtDoctorPassword;
        private System.Windows.Forms.Label lblUnknownLogin;
    }
}