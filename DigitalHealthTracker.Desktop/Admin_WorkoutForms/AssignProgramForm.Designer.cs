namespace DigitalHealthTracker.Desktop
{
    partial class AssignProgramForm
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
            dgvAssignments = new DataGridView();
            cmbUsers = new ComboBox();
            cmbPrograms = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            btnRefresh = new Button();
            btnAssign = new Button();
            btnCancel = new Button();
            btnDeleteAssignment = new Button();
            panel1 = new Panel();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvAssignments).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvAssignments
            // 
            dgvAssignments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAssignments.Location = new Point(0, 85);
            dgvAssignments.Name = "dgvAssignments";
            dgvAssignments.RowHeadersWidth = 51;
            dgvAssignments.Size = new Size(800, 200);
            dgvAssignments.TabIndex = 0;
            // 
            // cmbUsers
            // 
            cmbUsers.FormattingEnabled = true;
            cmbUsers.Location = new Point(165, 311);
            cmbUsers.Name = "cmbUsers";
            cmbUsers.Size = new Size(244, 28);
            cmbUsers.TabIndex = 2;
            // 
            // cmbPrograms
            // 
            cmbPrograms.FormattingEnabled = true;
            cmbPrograms.Location = new Point(165, 361);
            cmbPrograms.Name = "cmbPrograms";
            cmbPrograms.Size = new Size(244, 28);
            cmbPrograms.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(41, 311);
            label2.Name = "label2";
            label2.Size = new Size(45, 20);
            label2.TabIndex = 5;
            label2.Text = "User :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(41, 361);
            label3.Name = "label3";
            label3.Size = new Size(73, 20);
            label3.TabIndex = 6;
            label3.Text = "Program :";
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.Teal;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnRefresh.ForeColor = Color.WhiteSmoke;
            btnRefresh.Location = new Point(41, 398);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 40);
            btnRefresh.TabIndex = 7;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnAssign
            // 
            btnAssign.BackColor = Color.Teal;
            btnAssign.FlatStyle = FlatStyle.Flat;
            btnAssign.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnAssign.ForeColor = Color.WhiteSmoke;
            btnAssign.Location = new Point(471, 307);
            btnAssign.Name = "btnAssign";
            btnAssign.Size = new Size(140, 60);
            btnAssign.TabIndex = 8;
            btnAssign.Text = "Assign Program";
            btnAssign.UseVisualStyleBackColor = false;
            btnAssign.Click += btnAssign_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Teal;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnCancel.ForeColor = Color.WhiteSmoke;
            btnCancel.Location = new Point(646, 373);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(140, 60);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Close";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnDeleteAssignment
            // 
            btnDeleteAssignment.BackColor = Color.Teal;
            btnDeleteAssignment.FlatStyle = FlatStyle.Flat;
            btnDeleteAssignment.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnDeleteAssignment.ForeColor = Color.WhiteSmoke;
            btnDeleteAssignment.Location = new Point(471, 373);
            btnDeleteAssignment.Name = "btnDeleteAssignment";
            btnDeleteAssignment.Size = new Size(140, 60);
            btnDeleteAssignment.TabIndex = 11;
            btnDeleteAssignment.Text = "Delete Assignment";
            btnDeleteAssignment.UseVisualStyleBackColor = false;
            btnDeleteAssignment.Click += btnDeleteAssignment_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Teal;
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 85);
            panel1.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.ForeColor = Color.WhiteSmoke;
            label1.Location = new Point(25, 9);
            label1.Name = "label1";
            label1.Size = new Size(327, 41);
            label1.TabIndex = 0;
            label1.Text = "Digital Health Tracker";
            // 
            // AssignProgramForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(btnDeleteAssignment);
            Controls.Add(btnCancel);
            Controls.Add(btnAssign);
            Controls.Add(btnRefresh);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(cmbPrograms);
            Controls.Add(cmbUsers);
            Controls.Add(dgvAssignments);
            Name = "AssignProgramForm";
            Text = "AssignProgramForm";
            ((System.ComponentModel.ISupportInitialize)dgvAssignments).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvAssignments;
        private ComboBox cmbUsers;
        private ComboBox cmbPrograms;
        private Label label2;
        private Label label3;
        private Button btnRefresh;
        private Button btnAssign;
        private Button btnCancel;
        private Button btnDeleteAssignment;
        private Panel panel1;
        private Label label1;
    }
}