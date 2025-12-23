namespace DigitalHealthTracker.Desktop
{
    partial class WorkoutProgramEditForm
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
            txtTitle = new TextBox();
            cmbWorkouts = new ComboBox();
            txtSets = new TextBox();
            txtReps = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnAddItem = new Button();
            dgvItems = new DataGridView();
            btnCancel = new Button();
            btnSave = new Button();
            btnRemoveItem = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            SuspendLayout();
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(227, 255);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(125, 27);
            txtTitle.TabIndex = 0;
            // 
            // cmbWorkouts
            // 
            cmbWorkouts.FormattingEnabled = true;
            cmbWorkouts.Location = new Point(221, 297);
            cmbWorkouts.Name = "cmbWorkouts";
            cmbWorkouts.Size = new Size(245, 28);
            cmbWorkouts.TabIndex = 1;
            // 
            // txtSets
            // 
            txtSets.Location = new Point(221, 348);
            txtSets.Name = "txtSets";
            txtSets.Size = new Size(125, 27);
            txtSets.TabIndex = 3;
            // 
            // txtReps
            // 
            txtReps.Location = new Point(221, 392);
            txtReps.Name = "txtReps";
            txtReps.Size = new Size(125, 27);
            txtReps.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(66, 258);
            label1.Name = "label1";
            label1.Size = new Size(45, 20);
            label1.TabIndex = 5;
            label1.Text = "Title :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(72, 305);
            label2.Name = "label2";
            label2.Size = new Size(72, 20);
            label2.TabIndex = 6;
            label2.Text = "Workout :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(72, 348);
            label4.Name = "label4";
            label4.Size = new Size(43, 20);
            label4.TabIndex = 8;
            label4.Text = "Sets :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(75, 390);
            label5.Name = "label5";
            label5.Size = new Size(48, 20);
            label5.TabIndex = 9;
            label5.Text = "Reps :";
            // 
            // btnAddItem
            // 
            btnAddItem.Location = new Point(383, 357);
            btnAddItem.Name = "btnAddItem";
            btnAddItem.Size = new Size(104, 62);
            btnAddItem.TabIndex = 10;
            btnAddItem.Text = "Add Item";
            btnAddItem.UseVisualStyleBackColor = true;
            btnAddItem.Click += btnAddItem_Click;
            // 
            // dgvItems
            // 
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Dock = DockStyle.Top;
            dgvItems.Location = new Point(0, 0);
            dgvItems.Name = "dgvItems";
            dgvItems.RowHeadersWidth = 51;
            dgvItems.Size = new Size(800, 200);
            dgvItems.TabIndex = 11;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(676, 268);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(676, 219);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 14;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnRemoveItem
            // 
            btnRemoveItem.Location = new Point(511, 357);
            btnRemoveItem.Name = "btnRemoveItem";
            btnRemoveItem.Size = new Size(109, 61);
            btnRemoveItem.TabIndex = 15;
            btnRemoveItem.Text = "Remove Selected Item";
            btnRemoveItem.UseVisualStyleBackColor = true;
            btnRemoveItem.Click += btnRemoveItem_Click;
            // 
            // WorkoutProgramEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRemoveItem);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(dgvItems);
            Controls.Add(btnAddItem);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtReps);
            Controls.Add(txtSets);
            Controls.Add(cmbWorkouts);
            Controls.Add(txtTitle);
            Name = "WorkoutProgramEditForm";
            Text = "WorkoutProgramEditForm";
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTitle;
        private ComboBox cmbWorkouts;
        private TextBox txtSets;
        private TextBox txtReps;
        private Label label1;
        private Label label2;
        private Label label4;
        private Label label5;
        private Button btnAddItem;
        private DataGridView dgvItems;
        private Button button1;
        private Button btnCancel;
        private Button btnSave;
        private Button btnRemoveItem;
    }
}