namespace DigitalHealthTracker.Desktop
{
    partial class WorkoutProgramListForm
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
            btnLoadPrograms = new Button();
            btnAddProgram = new Button();
            btnEditProgram = new Button();
            btnDeleteProgram = new Button();
            dgvPrograms = new DataGridView();
            btnClose = new Button();
            panel1 = new Panel();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPrograms).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnLoadPrograms
            // 
            btnLoadPrograms.BackColor = Color.Teal;
            btnLoadPrograms.FlatStyle = FlatStyle.Flat;
            btnLoadPrograms.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnLoadPrograms.ForeColor = Color.WhiteSmoke;
            btnLoadPrograms.Location = new Point(80, 372);
            btnLoadPrograms.Name = "btnLoadPrograms";
            btnLoadPrograms.Size = new Size(150, 50);
            btnLoadPrograms.TabIndex = 1;
            btnLoadPrograms.Text = "Load";
            btnLoadPrograms.UseVisualStyleBackColor = false;
            btnLoadPrograms.Click += btnLoadPrograms_Click;
            // 
            // btnAddProgram
            // 
            btnAddProgram.BackColor = Color.Teal;
            btnAddProgram.FlatStyle = FlatStyle.Flat;
            btnAddProgram.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnAddProgram.ForeColor = Color.WhiteSmoke;
            btnAddProgram.Location = new Point(322, 342);
            btnAddProgram.Name = "btnAddProgram";
            btnAddProgram.Size = new Size(150, 50);
            btnAddProgram.TabIndex = 2;
            btnAddProgram.Text = "Add Program";
            btnAddProgram.UseVisualStyleBackColor = false;
            btnAddProgram.Click += btnAddProgram_Click;
            // 
            // btnEditProgram
            // 
            btnEditProgram.BackColor = Color.Teal;
            btnEditProgram.FlatStyle = FlatStyle.Flat;
            btnEditProgram.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnEditProgram.ForeColor = Color.WhiteSmoke;
            btnEditProgram.Location = new Point(322, 398);
            btnEditProgram.Name = "btnEditProgram";
            btnEditProgram.Size = new Size(150, 50);
            btnEditProgram.TabIndex = 3;
            btnEditProgram.Text = "Edit Program";
            btnEditProgram.UseVisualStyleBackColor = false;
            btnEditProgram.Click += btnEditProgram_Click;
            // 
            // btnDeleteProgram
            // 
            btnDeleteProgram.BackColor = Color.Teal;
            btnDeleteProgram.FlatStyle = FlatStyle.Flat;
            btnDeleteProgram.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnDeleteProgram.ForeColor = Color.WhiteSmoke;
            btnDeleteProgram.Location = new Point(572, 342);
            btnDeleteProgram.Name = "btnDeleteProgram";
            btnDeleteProgram.Size = new Size(150, 50);
            btnDeleteProgram.TabIndex = 4;
            btnDeleteProgram.Text = "Delete Program";
            btnDeleteProgram.UseVisualStyleBackColor = false;
            btnDeleteProgram.Click += btnDeleteProgram_Click;
            // 
            // dgvPrograms
            // 
            dgvPrograms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPrograms.Location = new Point(1, 85);
            dgvPrograms.Name = "dgvPrograms";
            dgvPrograms.RowHeadersWidth = 51;
            dgvPrograms.Size = new Size(800, 250);
            dgvPrograms.TabIndex = 5;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.Teal;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnClose.ForeColor = Color.WhiteSmoke;
            btnClose.Location = new Point(572, 398);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(150, 50);
            btnClose.TabIndex = 6;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Teal;
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 85);
            panel1.TabIndex = 7;
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
            // WorkoutProgramListForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(btnClose);
            Controls.Add(dgvPrograms);
            Controls.Add(btnDeleteProgram);
            Controls.Add(btnEditProgram);
            Controls.Add(btnAddProgram);
            Controls.Add(btnLoadPrograms);
            Name = "WorkoutProgramListForm";
            Text = "WorkoutProgramListForm";
            ((System.ComponentModel.ISupportInitialize)dgvPrograms).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button btnLoadPrograms;
        private Button btnAddProgram;
        private Button btnEditProgram;
        private Button btnDeleteProgram;
        private DataGridView dgvPrograms;
        private Button btnClose;
        private Panel panel1;
        private Label label1;
    }
}