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
            ((System.ComponentModel.ISupportInitialize)dgvPrograms).BeginInit();
            SuspendLayout();
            // 
            // btnLoadPrograms
            // 
            btnLoadPrograms.Location = new Point(81, 337);
            btnLoadPrograms.Name = "btnLoadPrograms";
            btnLoadPrograms.Size = new Size(94, 29);
            btnLoadPrograms.TabIndex = 1;
            btnLoadPrograms.Text = "Load";
            btnLoadPrograms.UseVisualStyleBackColor = true;
            btnLoadPrograms.Click += btnLoadPrograms_Click;
            // 
            // btnAddProgram
            // 
            btnAddProgram.Location = new Point(296, 333);
            btnAddProgram.Name = "btnAddProgram";
            btnAddProgram.Size = new Size(135, 47);
            btnAddProgram.TabIndex = 2;
            btnAddProgram.Text = "Add Program";
            btnAddProgram.UseVisualStyleBackColor = true;
            btnAddProgram.Click += btnAddProgram_Click;
            // 
            // btnEditProgram
            // 
            btnEditProgram.Location = new Point(458, 333);
            btnEditProgram.Name = "btnEditProgram";
            btnEditProgram.Size = new Size(119, 49);
            btnEditProgram.TabIndex = 3;
            btnEditProgram.Text = "Edit Program";
            btnEditProgram.UseVisualStyleBackColor = true;
            btnEditProgram.Click += btnEditProgram_Click;
            // 
            // btnDeleteProgram
            // 
            btnDeleteProgram.Location = new Point(605, 337);
            btnDeleteProgram.Name = "btnDeleteProgram";
            btnDeleteProgram.Size = new Size(129, 40);
            btnDeleteProgram.TabIndex = 4;
            btnDeleteProgram.Text = "Delete Program";
            btnDeleteProgram.UseVisualStyleBackColor = true;
            btnDeleteProgram.Click += btnDeleteProgram_Click;
            // 
            // dgvPrograms
            // 
            dgvPrograms.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPrograms.Dock = DockStyle.Top;
            dgvPrograms.Location = new Point(0, 0);
            dgvPrograms.Name = "dgvPrograms";
            dgvPrograms.RowHeadersWidth = 51;
            dgvPrograms.Size = new Size(800, 250);
            dgvPrograms.TabIndex = 5;
            // 
            // WorkoutProgramListForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvPrograms);
            Controls.Add(btnDeleteProgram);
            Controls.Add(btnEditProgram);
            Controls.Add(btnAddProgram);
            Controls.Add(btnLoadPrograms);
            Name = "WorkoutProgramListForm";
            Text = "WorkoutProgramListForm";
            ((System.ComponentModel.ISupportInitialize)dgvPrograms).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button btnLoadPrograms;
        private Button btnAddProgram;
        private Button btnEditProgram;
        private Button btnDeleteProgram;
        private DataGridView dgvPrograms;
    }
}