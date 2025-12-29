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
            btnEditMyProfile = new Button();
            btnLogout = new Button();
            lblSubTitle = new Label();
            lblTitle = new Label();
            pnlContent = new Panel();
            btnStudentLogs = new Button();
            lblWeightDiffValueTxt = new Label();
            lblTargetWeightValueTxt = new Label();
            lblBmiCategoryValueTxt = new Label();
            lblBmiValueTxt = new Label();
            lblWeightDiffValue = new Label();
            lblTargetWeightValue = new Label();
            lblBmiCategoryValue = new Label();
            lblBmiValue = new Label();
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
            btnManageUsers.Location = new Point(50, 99);
            btnManageUsers.Name = "btnManageUsers";
            btnManageUsers.Size = new Size(230, 110);
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
            btnManageTrainers.Location = new Point(603, 99);
            btnManageTrainers.Name = "btnManageTrainers";
            btnManageTrainers.Size = new Size(230, 110);
            btnManageTrainers.TabIndex = 1;
            btnManageTrainers.Text = "Manage Trainers";
            btnManageTrainers.UseVisualStyleBackColor = false;
            btnManageTrainers.Click += btnManageTrainers_Click;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.Teal;
            pnlHeader.Controls.Add(btnEditMyProfile);
            pnlHeader.Controls.Add(btnLogout);
            pnlHeader.Controls.Add(lblSubTitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(882, 84);
            pnlHeader.TabIndex = 2;
            // 
            // btnEditMyProfile
            // 
            btnEditMyProfile.BackColor = Color.WhiteSmoke;
            btnEditMyProfile.FlatStyle = FlatStyle.Flat;
            btnEditMyProfile.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnEditMyProfile.ForeColor = Color.Teal;
            btnEditMyProfile.Location = new Point(664, 15);
            btnEditMyProfile.Name = "btnEditMyProfile";
            btnEditMyProfile.Size = new Size(95, 50);
            btnEditMyProfile.TabIndex = 3;
            btnEditMyProfile.Text = "Edit My Profile";
            btnEditMyProfile.UseVisualStyleBackColor = false;
            btnEditMyProfile.Click += btnEditMyProfile_Click;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.WhiteSmoke;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnLogout.ForeColor = Color.Teal;
            btnLogout.Location = new Point(776, 15);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(95, 50);
            btnLogout.TabIndex = 2;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = false;
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
            pnlContent.Controls.Add(btnStudentLogs);
            pnlContent.Controls.Add(lblWeightDiffValueTxt);
            pnlContent.Controls.Add(lblTargetWeightValueTxt);
            pnlContent.Controls.Add(lblBmiCategoryValueTxt);
            pnlContent.Controls.Add(lblBmiValueTxt);
            pnlContent.Controls.Add(lblWeightDiffValue);
            pnlContent.Controls.Add(lblTargetWeightValue);
            pnlContent.Controls.Add(lblBmiCategoryValue);
            pnlContent.Controls.Add(lblBmiValue);
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
            pnlContent.Paint += pnlContent_Paint;
            // 
            // btnStudentLogs
            // 
            btnStudentLogs.BackColor = Color.Teal;
            btnStudentLogs.FlatStyle = FlatStyle.Flat;
            btnStudentLogs.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnStudentLogs.ForeColor = Color.WhiteSmoke;
            btnStudentLogs.Location = new Point(603, 201);
            btnStudentLogs.Name = "btnStudentLogs";
            btnStudentLogs.Size = new Size(230, 110);
            btnStudentLogs.TabIndex = 17;
            btnStudentLogs.Text = "Student Logs";
            btnStudentLogs.UseVisualStyleBackColor = false;
            btnStudentLogs.Click += btnStudentLogs_Click;
            // 
            // lblWeightDiffValueTxt
            // 
            lblWeightDiffValueTxt.AutoSize = true;
            lblWeightDiffValueTxt.Location = new Point(454, 113);
            lblWeightDiffValueTxt.Name = "lblWeightDiffValueTxt";
            lblWeightDiffValueTxt.Size = new Size(92, 20);
            lblWeightDiffValueTxt.TabIndex = 16;
            lblWeightDiffValueTxt.Text = "Weight Diff: ";
            // 
            // lblTargetWeightValueTxt
            // 
            lblTargetWeightValueTxt.AutoSize = true;
            lblTargetWeightValueTxt.Location = new Point(454, 87);
            lblTargetWeightValueTxt.Name = "lblTargetWeightValueTxt";
            lblTargetWeightValueTxt.Size = new Size(108, 20);
            lblTargetWeightValueTxt.TabIndex = 15;
            lblTargetWeightValueTxt.Text = "Target Weight: ";
            // 
            // lblBmiCategoryValueTxt
            // 
            lblBmiCategoryValueTxt.AutoSize = true;
            lblBmiCategoryValueTxt.Location = new Point(250, 113);
            lblBmiCategoryValueTxt.Name = "lblBmiCategoryValueTxt";
            lblBmiCategoryValueTxt.Size = new Size(106, 20);
            lblBmiCategoryValueTxt.TabIndex = 14;
            lblBmiCategoryValueTxt.Text = "BMI Category: ";
            // 
            // lblBmiValueTxt
            // 
            lblBmiValueTxt.AutoSize = true;
            lblBmiValueTxt.Location = new Point(250, 91);
            lblBmiValueTxt.Name = "lblBmiValueTxt";
            lblBmiValueTxt.Size = new Size(42, 20);
            lblBmiValueTxt.TabIndex = 13;
            lblBmiValueTxt.Text = "BMI: ";
            // 
            // lblWeightDiffValue
            // 
            lblWeightDiffValue.AutoSize = true;
            lblWeightDiffValue.Location = new Point(552, 113);
            lblWeightDiffValue.Name = "lblWeightDiffValue";
            lblWeightDiffValue.Size = new Size(34, 20);
            lblWeightDiffValue.TabIndex = 12;
            lblWeightDiffValue.Text = "Diff";
            // 
            // lblTargetWeightValue
            // 
            lblTargetWeightValue.AutoSize = true;
            lblTargetWeightValue.Location = new Point(568, 87);
            lblTargetWeightValue.Name = "lblTargetWeightValue";
            lblTargetWeightValue.Size = new Size(56, 20);
            lblTargetWeightValue.TabIndex = 11;
            lblTargetWeightValue.Text = "Weight";
            lblTargetWeightValue.Click += lblTargetWeightValue_Click;
            // 
            // lblBmiCategoryValue
            // 
            lblBmiCategoryValue.AutoSize = true;
            lblBmiCategoryValue.Location = new Point(362, 113);
            lblBmiCategoryValue.Name = "lblBmiCategoryValue";
            lblBmiCategoryValue.Size = new Size(35, 20);
            lblBmiCategoryValue.TabIndex = 10;
            lblBmiCategoryValue.Text = "ctgr";
            // 
            // lblBmiValue
            // 
            lblBmiValue.AutoSize = true;
            lblBmiValue.Location = new Point(298, 91);
            lblBmiValue.Name = "lblBmiValue";
            lblBmiValue.Size = new Size(42, 20);
            lblBmiValue.TabIndex = 9;
            lblBmiValue.Text = "BMI: ";
            // 
            // btnApproveAssignment
            // 
            btnApproveAssignment.BackColor = Color.Teal;
            btnApproveAssignment.FlatStyle = FlatStyle.Flat;
            btnApproveAssignment.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnApproveAssignment.ForeColor = Color.WhiteSmoke;
            btnApproveAssignment.Location = new Point(316, 201);
            btnApproveAssignment.Name = "btnApproveAssignment";
            btnApproveAssignment.Size = new Size(230, 110);
            btnApproveAssignment.TabIndex = 8;
            btnApproveAssignment.Text = "Approve Assignment";
            btnApproveAssignment.UseVisualStyleBackColor = false;
            btnApproveAssignment.Click += btnApproveAssignment_Click;
            // 
            // btnUserWorkout
            // 
            btnUserWorkout.BackColor = Color.Teal;
            btnUserWorkout.FlatStyle = FlatStyle.Flat;
            btnUserWorkout.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnUserWorkout.ForeColor = Color.WhiteSmoke;
            btnUserWorkout.Location = new Point(316, 315);
            btnUserWorkout.Name = "btnUserWorkout";
            btnUserWorkout.Size = new Size(230, 110);
            btnUserWorkout.TabIndex = 7;
            btnUserWorkout.Text = "My Workout";
            btnUserWorkout.UseVisualStyleBackColor = false;
            btnUserWorkout.Click += btnUserWorkout_Click;
            // 
            // btnAssignProgram
            // 
            btnAssignProgram.BackColor = Color.Teal;
            btnAssignProgram.FlatStyle = FlatStyle.Flat;
            btnAssignProgram.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnAssignProgram.ForeColor = Color.WhiteSmoke;
            btnAssignProgram.Location = new Point(603, 315);
            btnAssignProgram.Name = "btnAssignProgram";
            btnAssignProgram.Size = new Size(230, 110);
            btnAssignProgram.TabIndex = 6;
            btnAssignProgram.Text = "Assign Program";
            btnAssignProgram.UseVisualStyleBackColor = false;
            btnAssignProgram.Click += btnAssignProgram_Click;
            // 
            // btnManagePrograms
            // 
            btnManagePrograms.BackColor = Color.Teal;
            btnManagePrograms.FlatStyle = FlatStyle.Flat;
            btnManagePrograms.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnManagePrograms.ForeColor = Color.WhiteSmoke;
            btnManagePrograms.Location = new Point(50, 315);
            btnManagePrograms.Name = "btnManagePrograms";
            btnManagePrograms.Size = new Size(230, 110);
            btnManagePrograms.TabIndex = 5;
            btnManagePrograms.Text = "Manage Programs";
            btnManagePrograms.UseVisualStyleBackColor = false;
            btnManagePrograms.Click += btnManagePrograms_Click;
            // 
            // btnManageWorkouts
            // 
            btnManageWorkouts.BackColor = Color.Teal;
            btnManageWorkouts.FlatStyle = FlatStyle.Flat;
            btnManageWorkouts.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnManageWorkouts.ForeColor = Color.WhiteSmoke;
            btnManageWorkouts.Location = new Point(50, 201);
            btnManageWorkouts.Name = "btnManageWorkouts";
            btnManageWorkouts.Size = new Size(230, 110);
            btnManageWorkouts.TabIndex = 4;
            btnManageWorkouts.Text = "Manage Workouts";
            btnManageWorkouts.UseVisualStyleBackColor = false;
            btnManageWorkouts.Click += btnManageWorkouts_Click;
            // 
            // btnApproveTrainers
            // 
            btnApproveTrainers.BackColor = Color.Teal;
            btnApproveTrainers.FlatStyle = FlatStyle.Flat;
            btnApproveTrainers.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnApproveTrainers.ForeColor = Color.WhiteSmoke;
            btnApproveTrainers.Location = new Point(316, 99);
            btnApproveTrainers.Name = "btnApproveTrainers";
            btnApproveTrainers.Size = new Size(230, 110);
            btnApproveTrainers.TabIndex = 3;
            btnApproveTrainers.Text = "Approve Trainers";
            btnApproveTrainers.UseVisualStyleBackColor = false;
            btnApproveTrainers.Click += btnApproveTrainers_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Bottom;
            label3.ForeColor = Color.Gray;
            label3.Location = new Point(0, 433);
            label3.Name = "label3";
            label3.Size = new Size(236, 20);
            label3.TabIndex = 2;
            label3.Text = "SE410 –Digital Health Tracker – v2";
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
            Text = "Digital Health Tracker – Main Panel";
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
        private Label lblTargetWeightValue;
        private Button btnApproveTrainers;
        private Button btnManageWorkouts;
        private Button btnManagePrograms;
        private Button btnAssignProgram;
        private Button btnUserWorkout;
        private Button btnApproveAssignment;
        private Button btnLogout;
        private Button btnEditMyProfile;
        private Label lblBmiValue;
        private Label lblBmiCategoryValue;
        private Label lblWeightDiffValue;
        private Label label5;
        private Label lblTargetWeightValueTxt;
        private Label lblBmiCategoryValueTxt;
        private Label lblBmiValueTxt;
        private Button btnStudentLogs;
        private Label lblWeightDiffValueTxt;
    }
}