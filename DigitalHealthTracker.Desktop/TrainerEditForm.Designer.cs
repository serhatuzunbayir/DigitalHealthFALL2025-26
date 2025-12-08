namespace DigitalHealthTracker.Desktop
{
    partial class TrainerEditForm
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
            txtPhone = new TextBox();
            txtEmail = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            chkIsApproved = new CheckBox();
            btnSave = new Button();
            btnCancel = new Button();
            txtBirthYear = new TextBox();
            label5 = new Label();
            label6 = new Label();
            panel1 = new Panel();
            lblTrainerEditTitle = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(250, 87);
            txtName.Name = "txtName";
            txtName.Size = new Size(125, 27);
            txtName.TabIndex = 0;
            // 
            // txtSurname
            // 
            txtSurname.Location = new Point(250, 130);
            txtSurname.Name = "txtSurname";
            txtSurname.Size = new Size(125, 27);
            txtSurname.TabIndex = 1;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(250, 170);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(125, 27);
            txtPhone.TabIndex = 2;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(250, 210);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(125, 27);
            txtEmail.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(120, 90);
            label1.Name = "label1";
            label1.Size = new Size(62, 20);
            label1.TabIndex = 4;
            label1.Text = "*Name :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(120, 130);
            label2.Name = "label2";
            label2.Size = new Size(80, 20);
            label2.TabIndex = 5;
            label2.Text = "*Surname :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(120, 170);
            label3.Name = "label3";
            label3.Size = new Size(63, 20);
            label3.TabIndex = 6;
            label3.Text = "*Phone :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(120, 210);
            label4.Name = "label4";
            label4.Size = new Size(45, 20);
            label4.TabIndex = 7;
            label4.Text = "Mail :";
            // 
            // chkIsApproved
            // 
            chkIsApproved.AutoSize = true;
            chkIsApproved.Location = new Point(120, 300);
            chkIsApproved.Name = "chkIsApproved";
            chkIsApproved.Size = new Size(248, 24);
            chkIsApproved.TabIndex = 8;
            chkIsApproved.Text = "Is Approved as a Trainer (Admin)";
            chkIsApproved.UseVisualStyleBackColor = true;
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
            btnSave.TabIndex = 9;
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
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtBirthYear
            // 
            txtBirthYear.Location = new Point(250, 250);
            txtBirthYear.Name = "txtBirthYear";
            txtBirthYear.Size = new Size(125, 27);
            txtBirthYear.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(120, 250);
            label5.Name = "label5";
            label5.Size = new Size(85, 20);
            label5.TabIndex = 12;
            label5.Text = "*Birth Year :\r\n";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(120, 372);
            label6.Name = "label6";
            label6.Size = new Size(171, 20);
            label6.TabIndex = 13;
            label6.Text = "'*' fields are mandatory...";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Teal;
            panel1.Controls.Add(lblTrainerEditTitle);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(882, 70);
            panel1.TabIndex = 14;
            // 
            // lblTrainerEditTitle
            // 
            lblTrainerEditTitle.AutoSize = true;
            lblTrainerEditTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblTrainerEditTitle.ForeColor = Color.WhiteSmoke;
            lblTrainerEditTitle.Location = new Point(25, 10);
            lblTrainerEditTitle.Name = "lblTrainerEditTitle";
            lblTrainerEditTitle.Size = new Size(327, 41);
            lblTrainerEditTitle.TabIndex = 0;
            lblTrainerEditTitle.Text = "Digital Health Tracker";
            // 
            // TrainerEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 453);
            Controls.Add(panel1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(txtBirthYear);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(chkIsApproved);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtEmail);
            Controls.Add(txtPhone);
            Controls.Add(txtSurname);
            Controls.Add(txtName);
            Name = "TrainerEditForm";
            Text = "Trainer Edit Panel";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtName;
        private TextBox txtSurname;
        private TextBox txtPhone;
        private TextBox txtEmail;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private CheckBox chkIsApproved;
        private Button btnSave;
        private Button btnCancel;
        private TextBox txtBirthYear;
        private Label label5;
        private Label label6;
        private Panel panel1;
        private Label lblTrainerEditTitle;
    }
}