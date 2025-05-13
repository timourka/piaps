namespace DesctopSheduleManager
{
    partial class LoginForm
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
        private TextBox txtLogin;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblStatus;
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtLogin = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.lblStatus = new Label();

            this.SuspendLayout();

            // txtLogin
            this.txtLogin.Location = new Point(30, 30);
            this.txtLogin.Size = new Size(200, 23);
            this.txtLogin.PlaceholderText = "Login";

            // txtPassword
            this.txtPassword.Location = new Point(30, 70);
            this.txtPassword.Size = new Size(200, 23);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.PlaceholderText = "Password";

            // btnLogin
            this.btnLogin.Location = new Point(30, 110);
            this.btnLogin.Size = new Size(200, 30);
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new EventHandler(this.BtnLogin_Click);

            // lblStatus
            this.lblStatus.Location = new Point(30, 150);
            this.lblStatus.Size = new Size(200, 40);
            this.lblStatus.ForeColor = Color.Red;

            // LoginForm
            this.ClientSize = new Size(280, 220);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblStatus);
            this.Text = "Login";

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}