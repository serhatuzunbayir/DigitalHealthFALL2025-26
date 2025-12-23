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
            ((System.ComponentModel.ISupportInitialize)dgvAssignments).BeginInit();
            SuspendLayout();
            // 
            // dgvAssignments
            // 
            dgvAssignments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAssignments.Dock = DockStyle.Top;
            dgvAssignments.Location = new Point(0, 0);
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
            btnRefresh.Location = new Point(174, 405);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(94, 29);
            btnRefresh.TabIndex = 7;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnAssign
            // 
            btnAssign.Location = new Point(606, 307);
            btnAssign.Name = "btnAssign";
            btnAssign.Size = new Size(142, 42);
            btnAssign.TabIndex = 8;
            btnAssign.Text = "Assign Program";
            btnAssign.UseVisualStyleBackColor = true;
            btnAssign.Click += btnAssign_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(606, 402);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(133, 32);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnDeleteAssignment
            // 
            btnDeleteAssignment.Location = new Point(606, 357);
            btnDeleteAssignment.Name = "btnDeleteAssignment";
            btnDeleteAssignment.Size = new Size(142, 39);
            btnDeleteAssignment.TabIndex = 11;
            btnDeleteAssignment.Text = "Delete Assignment";
            btnDeleteAssignment.UseVisualStyleBackColor = true;
            btnDeleteAssignment.Click += btnDeleteAssignment_Click;
            // 
            // AssignProgramForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}