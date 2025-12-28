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
            btnClose = new Button();
            panel1 = new Panel();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvWorkouts).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvWorkouts
            // 
            dgvWorkouts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvWorkouts.Location = new Point(0, 85);
            dgvWorkouts.Name = "dgvWorkouts";
            dgvWorkouts.RowHeadersWidth = 51;
            dgvWorkouts.Size = new Size(800, 250);
            dgvWorkouts.TabIndex = 0;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.Teal;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnAdd.ForeColor = Color.WhiteSmoke;
            btnAdd.Location = new Point(46, 358);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(160, 60);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add Workout";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.Teal;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnEdit.ForeColor = Color.WhiteSmoke;
            btnEdit.Location = new Point(237, 358);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(160, 60);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Edit Workout";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Teal;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnDelete.ForeColor = Color.WhiteSmoke;
            btnDelete.Location = new Point(425, 358);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(160, 60);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete Workout";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.Teal;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnClose.ForeColor = Color.WhiteSmoke;
            btnClose.Location = new Point(615, 358);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(160, 60);
            btnClose.TabIndex = 4;
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
            panel1.TabIndex = 5;
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
            // WorkoutListForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(btnClose);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(dgvWorkouts);
            Name = "WorkoutListForm";
            Text = "WorkoutListForm";
            ((System.ComponentModel.ISupportInitialize)dgvWorkouts).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvWorkouts;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnClose;
        private Panel panel1;
        private Label label1;
    }
}