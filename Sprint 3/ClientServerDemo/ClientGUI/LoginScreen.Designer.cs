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
            this.roomName = new System.Windows.Forms.TextBox();
            this.noRoom = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // login
            // 
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
            this.name.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.name.Location = new System.Drawing.Point(299, 151);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(161, 26);
            this.name.TabIndex = 1;
            this.name.Text = "Name";
            // 
            // roomName
            // 
            this.roomName.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.roomName.Location = new System.Drawing.Point(299, 203);
            this.roomName.Name = "roomName";
            this.roomName.Size = new System.Drawing.Size(161, 26);
            this.roomName.TabIndex = 2;
            this.roomName.Text = "Room ID";
            // 
            // noRoom
            // 
            this.noRoom.AutoSize = true;
            this.noRoom.ForeColor = System.Drawing.Color.Red;
            this.noRoom.Location = new System.Drawing.Point(282, 324);
            this.noRoom.Name = "noRoom";
            this.noRoom.Size = new System.Drawing.Size(202, 20);
            this.noRoom.TabIndex = 3;
            this.noRoom.Text = "Room name does not exist!";
            this.Visible= false;
            // 
            // LoginScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.noRoom);
            this.Controls.Add(this.roomName);
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
        private System.Windows.Forms.TextBox roomName;
        private System.Windows.Forms.Label noRoom;
    }
}