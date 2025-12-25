namespace DigitalHealthTracker.Desktop
{
    partial class UserEditForm
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
            txtPhone = new TextBox();
            txtMedicalRecord = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            txtBirthYear = new TextBox();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            txtHeightCm = new TextBox();
            txtWeightKg = new TextBox();
            label9 = new Label();
            panel1 = new Panel();
            lblUserEditTitle = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(250, 90);
            txtName.Name = "txtName";
            txtName.Size = new Size(144, 27);
            txtName.TabIndex = 1;
            // 
            // txtSurname
            // 
            txtSurname.Location = new Point(250, 130);
            txtSurname.Name = "txtSurname";
            txtSurname.Size = new Size(144, 27);
            txtSurname.TabIndex = 2;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(250, 170);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(245, 27);
            txtEmail.TabIndex = 3;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(250, 210);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(144, 27);
            txtPhone.TabIndex = 4;
            // 
            // txtMedicalRecord
            // 
            txtMedicalRecord.Location = new Point(250, 250);
            txtMedicalRecord.Multiline = true;
            txtMedicalRecord.Name = "txtMedicalRecord";
            txtMedicalRecord.Size = new Size(144, 34);
            txtMedicalRecord.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(120, 90);
            label1.Name = "label1";
            label1.Size = new Size(62, 20);
            label1.TabIndex = 6;
            label1.Text = "*Name :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(120, 130);
            label2.Name = "label2";
            label2.Size = new Size(80, 20);
            label2.TabIndex = 7;
            label2.Text = "*Surname :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(120, 170);
            label3.Name = "label3";
            label3.Size = new Size(53, 20);
            label3.TabIndex = 8;
            label3.Text = "Email :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(120, 210);
            label4.Name = "label4";
            label4.Size = new Size(63, 20);
            label4.TabIndex = 9;
            label4.Text = "*Phone :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(120, 250);
            label5.Name = "label5";
            label5.Size = new Size(120, 20);
            label5.TabIndex = 10;
            label5.Text = "Medical Record :";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.Teal;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnSave.ForeColor = Color.WhiteSmoke;
            btnSave.Location = new Point(640, 330);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 40);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Teal;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnCancel.ForeColor = Color.WhiteSmoke;
            btnCancel.Location = new Point(640, 380);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 40);
            btnCancel.TabIndex = 12;
            btnCancel.Text = " Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtBirthYear
            // 
            txtBirthYear.Location = new Point(250, 290);
            txtBirthYear.Name = "txtBirthYear";
            txtBirthYear.Size = new Size(125, 27);
            txtBirthYear.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(120, 290);
            label6.Name = "label6";
            label6.Size = new Size(85, 20);
            label6.TabIndex = 14;
            label6.Text = "*Birth Year :";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(120, 330);
            label7.Name = "label7";
            label7.Size = new Size(101, 20);
            label7.TabIndex = 15;
            label7.Text = "*Height (cm) :";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(120, 370);
            label8.Name = "label8";
            label8.Size = new Size(99, 20);
            label8.TabIndex = 16;
            label8.Text = "*Weight (kg) :";
            // 
            // txtHeightCm
            // 
            txtHeightCm.Location = new Point(250, 330);
            txtHeightCm.Name = "txtHeightCm";
            txtHeightCm.Size = new Size(125, 27);
            txtHeightCm.TabIndex = 17;
            // 
            // txtWeightKg
            // 
            txtWeightKg.Location = new Point(250, 370);
            txtWeightKg.Name = "txtWeightKg";
            txtWeightKg.Size = new Size(125, 27);
            txtWeightKg.TabIndex = 18;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(110, 417);
            label9.Name = "label9";
            label9.Size = new Size(155, 20);
            label9.TabIndex = 19;
            label9.Text = "'*' fields are required...";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Teal;
            panel1.Controls.Add(lblUserEditTitle);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(882, 70);
            panel1.TabIndex = 20;
            // 
            // lblUserEditTitle
            // 
            lblUserEditTitle.AutoSize = true;
            lblUserEditTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblUserEditTitle.ForeColor = Color.WhiteSmoke;
            lblUserEditTitle.Location = new Point(25, 10);
            lblUserEditTitle.Name = "lblUserEditTitle";
            lblUserEditTitle.Size = new Size(327, 41);
            lblUserEditTitle.TabIndex = 0;
            lblUserEditTitle.Text = "Digital Health Tracker";
            // 
            // UserEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 453);
            Controls.Add(panel1);
            Controls.Add(label9);
            Controls.Add(txtWeightKg);
            Controls.Add(txtHeightCm);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(txtBirthYear);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtMedicalRecord);
            Controls.Add(txtPhone);
            Controls.Add(txtEmail);
            Controls.Add(txtSurname);
            Controls.Add(txtName);
            Name = "UserEditForm";
            Text = "User Edit Panel";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtName;
        private TextBox txtSurname;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private TextBox txtMedicalRecord;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button btnSave;
        private Button btnCancel;
        private TextBox txtBirthYear;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox txtHeightCm;
        private TextBox txtWeightKg;
        private Label label9;
        private Panel panel1;
        private Label lblUserEditTitle;
    }
}