namespace DigitalHealthTracker.Desktop
{
    partial class UserAssignmentsForm
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
            btnApproveSelected = new Button();
            dgvPending = new DataGridView();
            btnRefresh = new Button();
            btnClose = new Button();
            panel1 = new Panel();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPending).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnApproveSelected
            // 
            btnApproveSelected.BackColor = Color.Teal;
            btnApproveSelected.FlatStyle = FlatStyle.Flat;
            btnApproveSelected.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnApproveSelected.ForeColor = Color.WhiteSmoke;
            btnApproveSelected.Location = new Point(279, 365);
            btnApproveSelected.Name = "btnApproveSelected";
            btnApproveSelected.Size = new Size(191, 73);
            btnApproveSelected.TabIndex = 3;
            btnApproveSelected.Text = "Approve Selected Assignment";
            btnApproveSelected.UseVisualStyleBackColor = false;
            btnApproveSelected.Click += btnApproveSelected_Click;
            // 
            // dgvPending
            // 
            dgvPending.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPending.Location = new Point(-2, 80);
            dgvPending.Name = "dgvPending";
            dgvPending.RowHeadersWidth = 51;
            dgvPending.Size = new Size(802, 235);
            dgvPending.TabIndex = 2;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.Teal;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnRefresh.ForeColor = Color.WhiteSmoke;
            btnRefresh.Location = new Point(40, 365);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(191, 73);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.Teal;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnClose.ForeColor = Color.WhiteSmoke;
            btnClose.Location = new Point(534, 365);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(191, 73);
            btnClose.TabIndex = 5;
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
            panel1.TabIndex = 6;
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
            // UserAssignmentsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(btnClose);
            Controls.Add(btnRefresh);
            Controls.Add(btnApproveSelected);
            Controls.Add(dgvPending);
            Name = "UserAssignmentsForm";
            Text = "UserAssignmentsForm";
            ((System.ComponentModel.ISupportInitialize)dgvPending).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnApproveSelected;
        private DataGridView dgvPending;
        private Button btnRefresh;
        private Button btnClose;
        private Panel panel1;
        private Label label1;
    }
}