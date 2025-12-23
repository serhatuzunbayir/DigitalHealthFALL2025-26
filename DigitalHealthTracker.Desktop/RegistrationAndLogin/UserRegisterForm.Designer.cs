namespace DigitalHealthTracker.Desktop.RegistrationAndLogin
{
    partial class UserRegisterForm
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
            txtName = new TextBox();
            txtSurname = new TextBox();
            txtEmail = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtPassword = new TextBox();
            txtConfirmPassword = new TextBox();
            btnRegister = new Button();
            btnCancel = new Button();
            txtPhone = new TextBox();
            label6 = new Label();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(281, 56);
            txtName.Name = "txtName";
            txtName.Size = new Size(125, 27);
            txtName.TabIndex = 0;
            // 
            // txtSurname
            // 
            txtSurname.Location = new Point(281, 108);
            txtSurname.Name = "txtSurname";
            txtSurname.Size = new Size(125, 27);
            txtSurname.TabIndex = 1;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(284, 217);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(125, 27);
            txtEmail.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(159, 60);
            label1.Name = "label1";
            label1.Size = new Size(56, 20);
            label1.TabIndex = 3;
            label1.Text = "Name :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(157, 119);
            label2.Name = "label2";
            label2.Size = new Size(74, 20);
            label2.TabIndex = 4;
            label2.Text = "Surname :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(162, 224);
            label3.Name = "label3";
            label3.Size = new Size(52, 20);
            label3.TabIndex = 5;
            label3.Text = "E-Mail";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(141, 315);
            label4.Name = "label4";
            label4.Size = new Size(77, 20);
            label4.TabIndex = 6;
            label4.Text = "Password :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(113, 358);
            label5.Name = "label5";
            label5.Size = new Size(134, 20);
            label5.TabIndex = 7;
            label5.Text = "Confirm Password :";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(263, 312);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(132, 27);
            txtPassword.TabIndex = 8;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(284, 367);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(132, 27);
            txtConfirmPassword.TabIndex = 9;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(575, 220);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(94, 29);
            btnRegister.TabIndex = 10;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(575, 313);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(284, 163);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(125, 27);
            txtPhone.TabIndex = 12;
            txtPhone.Text = " ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(168, 166);
            label6.Name = "label6";
            label6.Size = new Size(91, 20);
            label6.TabIndex = 13;
            label6.Text = "Phone : +90 ";
            // 
            // UserRegisterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label6);
            Controls.Add(txtPhone);
            Controls.Add(btnCancel);
            Controls.Add(btnRegister);
            Controls.Add(txtConfirmPassword);
            Controls.Add(txtPassword);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtEmail);
            Controls.Add(txtSurname);
            Controls.Add(txtName);
            Name = "UserRegisterForm";
            Text = "User Registration";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtName;
        private TextBox txtSurname;
        private TextBox txtEmail;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private Button btnRegister;
        private Button btnCancel;
        private TextBox txtPhone;
        private Label label6;
    }
}