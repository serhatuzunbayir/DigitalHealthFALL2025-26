namespace DigitalHealthTracker.Desktop
{
    partial class TrainerListForm
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
            dgvTrainers = new DataGridView();
            btnEditTrainer = new Button();
            btnDeleteTrainer = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvTrainers).BeginInit();
            SuspendLayout();
            // 
            // dgvTrainers
            // 
            dgvTrainers.AllowUserToAddRows = false;
            dgvTrainers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTrainers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTrainers.Dock = DockStyle.Top;
            dgvTrainers.Location = new Point(0, 0);
            dgvTrainers.Name = "dgvTrainers";
            dgvTrainers.ReadOnly = true;
            dgvTrainers.RowHeadersVisible = false;
            dgvTrainers.RowHeadersWidth = 51;
            dgvTrainers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTrainers.Size = new Size(882, 350);
            dgvTrainers.TabIndex = 0;
            // 
            // btnEditTrainer
            // 
            btnEditTrainer.BackColor = Color.Teal;
            btnEditTrainer.FlatStyle = FlatStyle.Flat;
            btnEditTrainer.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnEditTrainer.ForeColor = Color.WhiteSmoke;
            btnEditTrainer.Location = new Point(380, 380);
            btnEditTrainer.Name = "btnEditTrainer";
            btnEditTrainer.Size = new Size(135, 50);
            btnEditTrainer.TabIndex = 2;
            btnEditTrainer.Text = "Edit Trainer";
            btnEditTrainer.UseVisualStyleBackColor = false;
            btnEditTrainer.Click += btnEditTrainer_Click;
            // 
            // btnDeleteTrainer
            // 
            btnDeleteTrainer.BackColor = Color.Teal;
            btnDeleteTrainer.FlatStyle = FlatStyle.Flat;
            btnDeleteTrainer.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnDeleteTrainer.ForeColor = Color.WhiteSmoke;
            btnDeleteTrainer.Location = new Point(540, 380);
            btnDeleteTrainer.Name = "btnDeleteTrainer";
            btnDeleteTrainer.Size = new Size(135, 50);
            btnDeleteTrainer.TabIndex = 3;
            btnDeleteTrainer.Text = "Delete Trainer";
            btnDeleteTrainer.UseVisualStyleBackColor = false;
            btnDeleteTrainer.Click += btnDeleteTrainer_Click;
            // 
            // TrainerListForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(882, 453);
            Controls.Add(btnDeleteTrainer);
            Controls.Add(btnEditTrainer);
            Controls.Add(dgvTrainers);
            Name = "TrainerListForm";
            Text = "Trainer List Panel";
            ((System.ComponentModel.ISupportInitialize)dgvTrainers).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvTrainers;
        private Button btnEditTrainer;
        private Button btnDeleteTrainer;
    }
}