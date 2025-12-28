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
            btnClose = new Button();
            panel1 = new Panel();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvTrainers).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvTrainers
            // 
            dgvTrainers.AllowUserToAddRows = false;
            dgvTrainers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTrainers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTrainers.Location = new Point(0, 85);
            dgvTrainers.Name = "dgvTrainers";
            dgvTrainers.ReadOnly = true;
            dgvTrainers.RowHeadersVisible = false;
            dgvTrainers.RowHeadersWidth = 51;
            dgvTrainers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTrainers.Size = new Size(882, 288);
            dgvTrainers.TabIndex = 0;
            // 
            // btnEditTrainer
            // 
            btnEditTrainer.BackColor = Color.Teal;
            btnEditTrainer.FlatStyle = FlatStyle.Flat;
            btnEditTrainer.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnEditTrainer.ForeColor = Color.WhiteSmoke;
            btnEditTrainer.Location = new Point(184, 391);
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
            btnDeleteTrainer.Location = new Point(388, 391);
            btnDeleteTrainer.Name = "btnDeleteTrainer";
            btnDeleteTrainer.Size = new Size(135, 50);
            btnDeleteTrainer.TabIndex = 3;
            btnDeleteTrainer.Text = "Delete Trainer";
            btnDeleteTrainer.UseVisualStyleBackColor = false;
            btnDeleteTrainer.Click += btnDeleteTrainer_Click;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.Teal;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnClose.ForeColor = Color.WhiteSmoke;
            btnClose.Location = new Point(598, 391);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(135, 50);
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
            panel1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 162);
            panel1.ForeColor = Color.WhiteSmoke;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(882, 85);
            panel1.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 9);
            label1.Name = "label1";
            label1.Size = new Size(327, 41);
            label1.TabIndex = 0;
            label1.Text = "Digital Health Tracker";
            // 
            // TrainerListForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(882, 453);
            Controls.Add(panel1);
            Controls.Add(btnClose);
            Controls.Add(btnDeleteTrainer);
            Controls.Add(btnEditTrainer);
            Controls.Add(dgvTrainers);
            Name = "TrainerListForm";
            Text = "Trainer List Panel";
            ((System.ComponentModel.ISupportInitialize)dgvTrainers).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvTrainers;
        private Button btnEditTrainer;
        private Button btnDeleteTrainer;
        private Button btnClose;
        private Panel panel1;
        private Label label1;
    }
}