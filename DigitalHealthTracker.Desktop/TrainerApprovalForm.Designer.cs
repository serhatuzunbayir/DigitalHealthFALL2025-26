namespace DigitalHealthTracker.Desktop
{
    partial class TrainerApprovalForm
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
            btnApprove = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvTrainers).BeginInit();
            SuspendLayout();
            // 
            // dgvTrainers
            // 
            dgvTrainers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTrainers.Dock = DockStyle.Top;
            dgvTrainers.Location = new Point(0, 0);
            dgvTrainers.Name = "dgvTrainers";
            dgvTrainers.RowHeadersWidth = 51;
            dgvTrainers.Size = new Size(800, 250);
            dgvTrainers.TabIndex = 0;
            // 
            // btnApprove
            // 
            btnApprove.Dock = DockStyle.Bottom;
            btnApprove.Location = new Point(0, 421);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new Size(800, 29);
            btnApprove.TabIndex = 1;
            btnApprove.Text = "Approve Selected Trainer";
            btnApprove.UseVisualStyleBackColor = true;
            btnApprove.Click += btnApprove_Click;
            // 
            // TrainerApprovalForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnApprove);
            Controls.Add(dgvTrainers);
            Name = "TrainerApprovalForm";
            Text = "Trainer Approval";
            Load += TrainerApprovalForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTrainers).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvTrainers;
        private Button btnApprove;
    }
}