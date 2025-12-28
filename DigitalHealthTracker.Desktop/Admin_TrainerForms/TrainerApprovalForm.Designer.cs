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
            btnClose = new Button();
            btnRefresh = new Button();
            panel1 = new Panel();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvTrainers).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvTrainers
            // 
            dgvTrainers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTrainers.Location = new Point(0, 85);
            dgvTrainers.Name = "dgvTrainers";
            dgvTrainers.RowHeadersWidth = 51;
            dgvTrainers.Size = new Size(800, 250);
            dgvTrainers.TabIndex = 0;
            // 
            // btnApprove
            // 
            btnApprove.BackColor = Color.Teal;
            btnApprove.FlatStyle = FlatStyle.Flat;
            btnApprove.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnApprove.ForeColor = Color.WhiteSmoke;
            btnApprove.Location = new Point(308, 365);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new Size(160, 60);
            btnApprove.TabIndex = 1;
            btnApprove.Text = "Approve Selected Trainer";
            btnApprove.UseVisualStyleBackColor = false;
            btnApprove.Click += btnApprove_Click;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.Teal;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnClose.ForeColor = Color.WhiteSmoke;
            btnClose.Location = new Point(558, 365);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(160, 60);
            btnClose.TabIndex = 7;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.Teal;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnRefresh.ForeColor = Color.WhiteSmoke;
            btnRefresh.Location = new Point(72, 365);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(160, 60);
            btnRefresh.TabIndex = 6;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
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
            panel1.Size = new Size(800, 85);
            panel1.TabIndex = 8;
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
            // TrainerApprovalForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(btnClose);
            Controls.Add(btnRefresh);
            Controls.Add(btnApprove);
            Controls.Add(dgvTrainers);
            Name = "TrainerApprovalForm";
            Text = "Trainer Approval";
            ((System.ComponentModel.ISupportInitialize)dgvTrainers).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvTrainers;
        private Button btnApprove;
        private Button btnClose;
        private Button btnRefresh;
        private Panel panel1;
        private Label label1;
    }
}