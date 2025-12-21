namespace DigitalHealthTracker.Desktop
{
    partial class WorkoutListForm
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
            dgvWorkouts = new DataGridView();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvWorkouts).BeginInit();
            SuspendLayout();
            // 
            // dgvWorkouts
            // 
            dgvWorkouts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvWorkouts.Dock = DockStyle.Top;
            dgvWorkouts.Location = new Point(0, 0);
            dgvWorkouts.Name = "dgvWorkouts";
            dgvWorkouts.RowHeadersWidth = 51;
            dgvWorkouts.Size = new Size(800, 250);
            dgvWorkouts.TabIndex = 0;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(116, 327);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(160, 99);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add Workout";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(344, 339);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(158, 75);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Edit Workout";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(561, 333);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(139, 75);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete Workout";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // WorkoutListForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(dgvWorkouts);
            Name = "WorkoutListForm";
            Text = "WorkoutListForm";
            ((System.ComponentModel.ISupportInitialize)dgvWorkouts).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvWorkouts;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
    }
}