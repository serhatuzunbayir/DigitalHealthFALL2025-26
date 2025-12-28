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
            lblServerStatus = new Button();
            panel1 = new Panel();
            label4 = new Label();
            panel1.SuspendLayout();
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
            cmbRole.Location = new Point(266, 100);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(151, 28);
            cmbRole.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(156, 103);
            label3.Name = "label3";
            label3.Size = new Size(88, 20);
            label3.TabIndex = 5;
            label3.Text = "Login Type :";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.Teal;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnLogin.ForeColor = Color.WhiteSmoke;
            btnLogin.Location = new Point(292, 241);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(100, 42);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnRegisterUser
            // 
            btnRegisterUser.BackColor = Color.Teal;
            btnRegisterUser.FlatStyle = FlatStyle.Flat;
            btnRegisterUser.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnRegisterUser.ForeColor = Color.WhiteSmoke;
            btnRegisterUser.Location = new Point(180, 377);
            btnRegisterUser.Name = "btnRegisterUser";
            btnRegisterUser.Size = new Size(160, 60);
            btnRegisterUser.TabIndex = 7;
            btnRegisterUser.Text = "Register As User";
            btnRegisterUser.UseVisualStyleBackColor = false;
            btnRegisterUser.Click += btnRegisterUser_Click;
            // 
            // btnRegisterTrainer
            // 
            btnRegisterTrainer.BackColor = Color.Teal;
            btnRegisterTrainer.FlatStyle = FlatStyle.Flat;
            btnRegisterTrainer.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnRegisterTrainer.ForeColor = Color.WhiteSmoke;
            btnRegisterTrainer.Location = new Point(379, 377);
            btnRegisterTrainer.Name = "btnRegisterTrainer";
            btnRegisterTrainer.Size = new Size(160, 60);
            btnRegisterTrainer.TabIndex = 8;
            btnRegisterTrainer.Text = "Register As Trainer";
            btnRegisterTrainer.UseVisualStyleBackColor = false;
            btnRegisterTrainer.Click += btnRegisterTrainer_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.Teal;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnExit.ForeColor = Color.WhiteSmoke;
            btnExit.Location = new Point(667, 387);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(100, 40);
            btnExit.TabIndex = 9;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // lblServerStatus
            // 
            lblServerStatus.BackColor = Color.Teal;
            lblServerStatus.FlatStyle = FlatStyle.Flat;
            lblServerStatus.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblServerStatus.ForeColor = Color.WhiteSmoke;
            lblServerStatus.Location = new Point(255, 317);
            lblServerStatus.Name = "lblServerStatus";
            lblServerStatus.Size = new Size(180, 36);
            lblServerStatus.TabIndex = 10;
            lblServerStatus.Text = "Refresh";
            lblServerStatus.UseVisualStyleBackColor = false;
            lblServerStatus.Click += lblServerStatus_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Teal;
            panel1.Controls.Add(label4);
            panel1.Dock = DockStyle.Top;
            panel1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 162);
            panel1.ForeColor = Color.WhiteSmoke;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 85);
            panel1.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(25, 9);
            label4.Name = "label4";
            label4.Size = new Size(327, 41);
            label4.TabIndex = 0;
            label4.Text = "Digital Health Tracker";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(lblServerStatus);
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
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private Button lblServerStatus;
        private Panel panel1;
        private Label label4;
    }
}