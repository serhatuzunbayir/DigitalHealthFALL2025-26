namespace DigitalHealthTracker.Desktop
{
    partial class MainForm
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
            btnManageUsers = new Button();
            btnManageTrainers = new Button();
            pnlHeader = new Panel();
            label2 = new Label();
            lblTitle = new Label();
            pnlContent = new Panel();
            label3 = new Label();
            pnlHeader.SuspendLayout();
            pnlContent.SuspendLayout();
            SuspendLayout();
            // 
            // btnManageUsers
            // 
            btnManageUsers.BackColor = Color.Teal;
            btnManageUsers.FlatStyle = FlatStyle.Flat;
            btnManageUsers.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnManageUsers.ForeColor = Color.White;
            btnManageUsers.Location = new Point(170, 140);
            btnManageUsers.Name = "btnManageUsers";
            btnManageUsers.Size = new Size(220, 120);
            btnManageUsers.TabIndex = 0;
            btnManageUsers.Text = "Manage Users";
            btnManageUsers.UseVisualStyleBackColor = false;
            btnManageUsers.Click += btnManageUsers_Click;
            // 
            // btnManageTrainers
            // 
            btnManageTrainers.BackColor = Color.Teal;
            btnManageTrainers.FlatStyle = FlatStyle.Flat;
            btnManageTrainers.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnManageTrainers.ForeColor = Color.White;
            btnManageTrainers.Location = new Point(510, 140);
            btnManageTrainers.Name = "btnManageTrainers";
            btnManageTrainers.Size = new Size(220, 120);
            btnManageTrainers.TabIndex = 1;
            btnManageTrainers.Text = "Manage Trainers";
            btnManageTrainers.UseVisualStyleBackColor = false;
            btnManageTrainers.Click += btnManageTrainers_Click;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.Teal;
            pnlHeader.Controls.Add(label2);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(882, 80);
            pnlHeader.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.ForeColor = Color.WhiteSmoke;
            label2.Location = new Point(25, 50);
            label2.Name = "label2";
            label2.Size = new Size(148, 23);
            label2.TabIndex = 1;
            label2.Text = "Admin Dashboard";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(25, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(327, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Digital Health Tracker";
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.WhiteSmoke;
            pnlContent.Controls.Add(label3);
            pnlContent.Controls.Add(btnManageUsers);
            pnlContent.Controls.Add(btnManageTrainers);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(882, 453);
            pnlContent.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Bottom;
            label3.ForeColor = Color.Gray;
            label3.Location = new Point(0, 433);
            label3.Name = "label3";
            label3.Size = new Size(292, 20);
            label3.TabIndex = 2;
            label3.Text = "SE410 – Digital Health & Fitness Tracker – v1";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(882, 453);
            Controls.Add(pnlHeader);
            Controls.Add(pnlContent);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainForm";
            Text = "Digital Health Tracker – Admin Panel";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnManageUsers;
        private Button btnManageTrainers;
        private Panel pnlHeader;
        private Label lblTitle;
        private Label label1;
        private Label label2;
        private Panel pnlContent;
        private Label label3;
    }
}