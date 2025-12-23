namespace DigitalHealthTracker.Desktop.RegistrationAndLogin
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtPhone = new TextBox();
            txtPassword = new TextBox();
            label1 = new Label();
            label2 = new Label();
            cmbRole = new ComboBox();
            label3 = new Label();
            btnLogin = new Button();
            btnRegisterUser = new Button();
            btnRegisterTrainer = new Button();
            btnExit = new Button();
            SuspendLayout();
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(280, 146);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(125, 27);
            txtPhone.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(280, 191);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(125, 27);
            txtPassword.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(167, 146);
            label1.Name = "label1";
            label1.Size = new Size(87, 20);
            label1.TabIndex = 2;
            label1.Text = "Phone : +90";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(180, 188);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 3;
            label2.Text = "Password :";
            // 
            // cmbRole
            // 
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(267, 82);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(151, 28);
            cmbRole.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(157, 85);
            label3.Name = "label3";
            label3.Size = new Size(88, 20);
            label3.TabIndex = 5;
            label3.Text = "Login Type :";
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(293, 241);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(94, 29);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnRegisterUser
            // 
            btnRegisterUser.Location = new Point(180, 377);
            btnRegisterUser.Name = "btnRegisterUser";
            btnRegisterUser.Size = new Size(146, 50);
            btnRegisterUser.TabIndex = 7;
            btnRegisterUser.Text = "Register As User";
            btnRegisterUser.UseVisualStyleBackColor = true;
            btnRegisterUser.Click += btnRegisterUser_Click;
            // 
            // btnRegisterTrainer
            // 
            btnRegisterTrainer.Location = new Point(379, 377);
            btnRegisterTrainer.Name = "btnRegisterTrainer";
            btnRegisterTrainer.Size = new Size(164, 50);
            btnRegisterTrainer.TabIndex = 8;
            btnRegisterTrainer.Text = "Register As Trainer";
            btnRegisterTrainer.UseVisualStyleBackColor = true;
            btnRegisterTrainer.Click += btnRegisterTrainer_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(673, 388);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(94, 29);
            btnExit.TabIndex = 9;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnExit);
            Controls.Add(btnRegisterTrainer);
            Controls.Add(btnRegisterUser);
            Controls.Add(btnLogin);
            Controls.Add(label3);
            Controls.Add(cmbRole);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtPassword);
            Controls.Add(txtPhone);
            Name = "LoginForm";
            Text = "Login";
            Load += LoginForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPhone;
        private TextBox txtPassword;
        private Label label1;
        private Label label2;
        private ComboBox cmbRole;
        private Label label3;
        private Button btnLogin;
        private Button btnRegisterUser;
        private Button btnRegisterTrainer;
        private Button btnExit;
    }
}