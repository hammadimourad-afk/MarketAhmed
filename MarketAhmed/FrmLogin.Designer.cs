using System.Windows.Forms;

namespace MarketAhmed.UI
{
    partial class FrmLogin
    {
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnLogin;

        private void InitializeComponent()
        {
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnLogin = new Button();
            SuspendLayout();
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new System.Drawing.Point(30, 30);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new System.Drawing.Size(148, 25);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Nom d'utilisateur";
            // 
            // txtUsername
            // 
            txtUsername.Location = new System.Drawing.Point(181, 27);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new System.Drawing.Size(200, 31);
            txtUsername.TabIndex = 1;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new System.Drawing.Point(30, 70);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new System.Drawing.Size(120, 25);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Mot de passe";
            // 
            // txtPassword
            // 
            txtPassword.Location = new System.Drawing.Point(180, 67);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new System.Drawing.Size(200, 31);
            txtPassword.TabIndex = 3;
            // 
            // btnLogin
            // 
            btnLogin.Location = new System.Drawing.Point(186, 115);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new System.Drawing.Size(135, 42);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Connexion";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // FrmLogin
            // 
            ClientSize = new System.Drawing.Size(400, 180);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Connexion";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
