namespace DigitalHealthTracker.Desktop.RegistrationAndLogin
{
    partial class TrainerRegisterForm
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
            label6 = new Label();
            txtPhone = new TextBox();
            btnCancel = new Button();
            btnRegister = new Button();
            txtConfirmPassword = new TextBox();
            txtPassword = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtEmail = new TextBox();
            txtSurname = new TextBox();
            txtName = new TextBox();
            SuspendLayout();
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(167, 184);
            label6.Name = "label6";
            label6.Size = new Size(91, 20);
            label6.TabIndex = 27;
            label6.Text = "Phone : +90 ";
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(283, 181);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(125, 27);
            txtPhone.TabIndex = 26;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(574, 331);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 25;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(574, 238);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(94, 29);
            btnRegister.TabIndex = 24;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(304, 349);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(125, 27);
            txtConfirmPassword.TabIndex = 23;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(283, 294);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(125, 27);
            txtPassword.TabIndex = 22;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(133, 340);
            label5.Name = "label5";
            label5.Size = new Size(134, 20);
            label5.TabIndex = 21;
            label5.Text = "Confirm Password :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(161, 297);
            label4.Name = "label4";
            label4.Size = new Size(77, 20);
            label4.TabIndex = 20;
            label4.Text = "Password :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(161, 242);
            label3.Name = "label3";
            label3.Size = new Size(52, 20);
            label3.TabIndex = 19;
            label3.Text = "E-Mail";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(156, 137);
            label2.Name = "label2";
            label2.Size = new Size(74, 20);
            label2.TabIndex = 18;
            label2.Text = "Surname :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(158, 78);
            label1.Name = "label1";
            label1.Size = new Size(56, 20);
            label1.TabIndex = 17;
            label1.Text = "Name :";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(283, 235);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(125, 27);
            txtEmail.TabIndex = 16;
            // 
            // txtSurname
            // 
            txtSurname.Location = new Point(280, 126);
            txtSurname.Name = "txtSurname";
            txtSurname.Size = new Size(125, 27);
            txtSurname.TabIndex = 15;
            // 
            // txtName
            // 
            txtName.Location = new Point(280, 74);
            txtName.Name = "txtName";
            txtName.Size = new Size(125, 27);
            txtName.TabIndex = 14;
            // 
            // TrainerRegisterForm
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
            Name = "TrainerRegisterForm";
            Text = "TrainerRegisterForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label6;
        private TextBox txtPhone;
        private Button btnCancel;
        private Button btnRegister;
        private TextBox txtConfirmPassword;
        private TextBox txtPassword;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txtEmail;
        private TextBox txtSurname;
        private TextBox txtName;
    }
}