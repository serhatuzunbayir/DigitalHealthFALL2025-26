namespace DigitalHealthTracker.Desktop
{
    partial class UserWorkoutForm
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
            lblActiveProgram = new Label();
            btnCompleteDay = new Button();
            dgvHistory = new DataGridView();
            btnRefreshHistory = new Button();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            SuspendLayout();
            // 
            // lblActiveProgram
            // 
            lblActiveProgram.AutoSize = true;
            lblActiveProgram.Location = new Point(47, 314);
            lblActiveProgram.Name = "lblActiveProgram";
            lblActiveProgram.Size = new Size(118, 20);
            lblActiveProgram.TabIndex = 1;
            lblActiveProgram.Text = "Active Program :";
            // 
            // btnCompleteDay
            // 
            btnCompleteDay.Location = new Point(603, 299);
            btnCompleteDay.Name = "btnCompleteDay";
            btnCompleteDay.Size = new Size(116, 51);
            btnCompleteDay.TabIndex = 6;
            btnCompleteDay.Text = "Complete Program";
            btnCompleteDay.UseVisualStyleBackColor = true;
            btnCompleteDay.Click += btnCompleteDay_Click;
            // 
            // dgvHistory
            // 
            dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistory.Dock = DockStyle.Top;
            dgvHistory.Location = new Point(0, 0);
            dgvHistory.Name = "dgvHistory";
            dgvHistory.RowHeadersWidth = 51;
            dgvHistory.Size = new Size(800, 200);
            dgvHistory.TabIndex = 7;
            // 
            // btnRefreshHistory
            // 
            btnRefreshHistory.Location = new Point(47, 376);
            btnRefreshHistory.Name = "btnRefreshHistory";
            btnRefreshHistory.Size = new Size(118, 48);
            btnRefreshHistory.TabIndex = 8;
            btnRefreshHistory.Text = "Refresh History";
            btnRefreshHistory.UseVisualStyleBackColor = true;
            btnRefreshHistory.Click += btnRefreshHistory_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(603, 376);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(116, 42);
            btnClose.TabIndex = 9;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // UserWorkoutForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnClose);
            Controls.Add(btnRefreshHistory);
            Controls.Add(dgvHistory);
            Controls.Add(btnCompleteDay);
            Controls.Add(lblActiveProgram);
            Name = "UserWorkoutForm";
            Text = "UserWorkoutForm";
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblActiveProgram;
        private TextBox txtDayNo;
        private Button btnCompleteDay;
        private DataGridView dgvHistory;
        private Button btnRefreshHistory;
        private Button btnClose;
    }
}