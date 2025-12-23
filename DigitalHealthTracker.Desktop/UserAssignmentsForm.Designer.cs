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
            ((System.ComponentModel.ISupportInitialize)dgvPending).BeginInit();
            SuspendLayout();
            // 
            // btnApproveSelected
            // 
            btnApproveSelected.Dock = DockStyle.Bottom;
            btnApproveSelected.Location = new Point(0, 401);
            btnApproveSelected.Name = "btnApproveSelected";
            btnApproveSelected.Size = new Size(800, 49);
            btnApproveSelected.TabIndex = 3;
            btnApproveSelected.Text = "Approve Selected Assignment";
            btnApproveSelected.UseVisualStyleBackColor = true;
            btnApproveSelected.Click += btnApproveSelected_Click;
            // 
            // dgvPending
            // 
            dgvPending.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPending.Location = new Point(0, 0);
            dgvPending.Name = "dgvPending";
            dgvPending.RowHeadersWidth = 51;
            dgvPending.Size = new Size(802, 262);
            dgvPending.TabIndex = 2;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(32, 289);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(137, 45);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(621, 292);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(130, 42);
            btnClose.TabIndex = 5;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // UserAssignmentsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnClose);
            Controls.Add(btnRefresh);
            Controls.Add(btnApproveSelected);
            Controls.Add(dgvPending);
            Name = "UserAssignmentsForm";
            Text = "UserAssignmentsForm";
            ((System.ComponentModel.ISupportInitialize)dgvPending).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnApproveSelected;
        private DataGridView dgvPending;
        private Button btnRefresh;
        private Button btnClose;
    }
}