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
            btnLogout = new Button();
            lblSubTitle = new Label();
            lblTitle = new Label();
            pnlContent = new Panel();
            btnApproveAssignment = new Button();
            btnUserWorkout = new Button();
            btnAssignProgram = new Button();
            btnManagePrograms = new Button();
            btnManageWorkouts = new Button();
            btnApproveTrainers = new Button();
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
            btnManageUsers.Location = new Point(25, 102);
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
            btnManageTrainers.Location = new Point(650, 102);
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
            pnlHeader.Controls.Add(btnLogout);
            pnlHeader.Controls.Add(lblSubTitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(882, 80);
            pnlHeader.TabIndex = 2;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(803, 21);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(67, 38);
            btnLogout.TabIndex = 2;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // lblSubTitle
            // 
            lblSubTitle.AutoSize = true;
            lblSubTitle.Font = new Font("Segoe UI", 10F);
            lblSubTitle.ForeColor = Color.WhiteSmoke;
            lblSubTitle.Location = new Point(25, 50);
            lblSubTitle.Name = "lblSubTitle";
            lblSubTitle.Size = new Size(148, 23);
            lblSubTitle.TabIndex = 1;
            lblSubTitle.Text = "Admin Dashboard";
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
            pnlContent.Controls.Add(btnApproveAssignment);
            pnlContent.Controls.Add(btnUserWorkout);
            pnlContent.Controls.Add(btnAssignProgram);
            pnlContent.Controls.Add(btnManagePrograms);
            pnlContent.Controls.Add(btnManageWorkouts);
            pnlContent.Controls.Add(btnApproveTrainers);
            pnlContent.Controls.Add(label3);
            pnlContent.Controls.Add(btnManageUsers);
            pnlContent.Controls.Add(btnManageTrainers);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(882, 453);
            pnlContent.TabIndex = 4;
            // 
            // btnApproveAssignment
            // 
            btnApproveAssignment.Location = new Point(472, 228);
            btnApproveAssignment.Name = "btnApproveAssignment";
            btnApproveAssignment.Size = new Size(170, 81);
            btnApproveAssignment.TabIndex = 8;
            btnApproveAssignment.Text = "Approve Assignment";
            btnApproveAssignment.UseVisualStyleBackColor = true;
            btnApproveAssignment.Click += btnApproveAssignment_Click;
            // 
            // btnUserWorkout
            // 
            btnUserWorkout.Location = new Point(196, 228);
            btnUserWorkout.Name = "btnUserWorkout";
            btnUserWorkout.Size = new Size(176, 81);
            btnUserWorkout.TabIndex = 7;
            btnUserWorkout.Text = "My Workout";
            btnUserWorkout.UseVisualStyleBackColor = true;
            btnUserWorkout.Click += btnUserWorkout_Click;
            // 
            // btnAssignProgram
            // 
            btnAssignProgram.Location = new Point(634, 323);
            btnAssignProgram.Name = "btnAssignProgram";
            btnAssignProgram.Size = new Size(220, 103);
            btnAssignProgram.TabIndex = 6;
            btnAssignProgram.Text = "Assign Program";
            btnAssignProgram.UseVisualStyleBackColor = true;
            btnAssignProgram.Click += btnAssignProgram_Click;
            // 
            // btnManagePrograms
            // 
            btnManagePrograms.Location = new Point(25, 333);
            btnManagePrograms.Name = "btnManagePrograms";
            btnManagePrograms.Size = new Size(220, 93);
            btnManagePrograms.TabIndex = 5;
            btnManagePrograms.Text = "Manage Programs";
            btnManagePrograms.UseVisualStyleBackColor = true;
            btnManagePrograms.Click += btnManagePrograms_Click;
            // 
            // btnManageWorkouts
            // 
            btnManageWorkouts.Location = new Point(336, 102);
            btnManageWorkouts.Name = "btnManageWorkouts";
            btnManageWorkouts.Size = new Size(228, 120);
            btnManageWorkouts.TabIndex = 4;
            btnManageWorkouts.Text = "Manage Workouts";
            btnManageWorkouts.UseVisualStyleBackColor = true;
            btnManageWorkouts.Click += btnManageWorkouts_Click;
            // 
            // btnApproveTrainers
            // 
            btnApproveTrainers.Location = new Point(336, 323);
            btnApproveTrainers.Name = "btnApproveTrainers";
            btnApproveTrainers.Size = new Size(228, 108);
            btnApproveTrainers.TabIndex = 3;
            btnApproveTrainers.Text = "Approve Trainers";
            btnApproveTrainers.UseVisualStyleBackColor = true;
            btnApproveTrainers.Click += btnApproveTrainers_Click;
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
            Load += MainForm_Load;
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
        private Label lblSubTitle;
        private Panel pnlContent;
        private Label label3;
        private Button btnApproveTrainers;
        private Button btnManageWorkouts;
        private Button btnManagePrograms;
        private Button btnAssignProgram;
        private Button btnUserWorkout;
        private Button btnApproveAssignment;
        private Button btnLogout;
    }
}