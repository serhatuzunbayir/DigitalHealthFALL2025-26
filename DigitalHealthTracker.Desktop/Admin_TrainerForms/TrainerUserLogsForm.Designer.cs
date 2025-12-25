namespace DigitalHealthTracker.Desktop.Admin_TrainerForms
{
    partial class TrainerUserLogsForm
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
            cmbUsers = new ComboBox();
            btnLoad = new Button();
            dgvLogs = new DataGridView();
            label5 = new Label();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            lblWeightDiffValue = new Label();
            lblTargetWeightValue = new Label();
            lblBmiCategoryValue = new Label();
            lblBmiValue = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            SuspendLayout();
            // 
            // cmbUsers
            // 
            cmbUsers.FormattingEnabled = true;
            cmbUsers.Location = new Point(110, 304);
            cmbUsers.Name = "cmbUsers";
            cmbUsers.Size = new Size(151, 28);
            cmbUsers.TabIndex = 0;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(125, 355);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(94, 29);
            btnLoad.TabIndex = 1;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // dgvLogs
            // 
            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogs.Dock = DockStyle.Top;
            dgvLogs.Location = new Point(0, 0);
            dgvLogs.Name = "dgvLogs";
            dgvLogs.RowHeadersWidth = 51;
            dgvLogs.Size = new Size(800, 250);
            dgvLogs.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(597, 295);
            label5.Name = "label5";
            label5.Size = new Size(92, 20);
            label5.TabIndex = 24;
            label5.Text = "Weight Diff: ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(597, 269);
            label4.Name = "label4";
            label4.Size = new Size(108, 20);
            label4.TabIndex = 23;
            label4.Text = "Target Weight: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(393, 295);
            label2.Name = "label2";
            label2.Size = new Size(106, 20);
            label2.TabIndex = 22;
            label2.Text = "BMI Category: ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(393, 273);
            label1.Name = "label1";
            label1.Size = new Size(42, 20);
            label1.TabIndex = 21;
            label1.Text = "BMI: ";
            // 
            // lblWeightDiffValue
            // 
            lblWeightDiffValue.AutoSize = true;
            lblWeightDiffValue.Location = new Point(695, 295);
            lblWeightDiffValue.Name = "lblWeightDiffValue";
            lblWeightDiffValue.Size = new Size(0, 20);
            lblWeightDiffValue.TabIndex = 20;
            // 
            // lblTargetWeightValue
            // 
            lblTargetWeightValue.AutoSize = true;
            lblTargetWeightValue.Location = new Point(711, 269);
            lblTargetWeightValue.Name = "lblTargetWeightValue";
            lblTargetWeightValue.Size = new Size(0, 20);
            lblTargetWeightValue.TabIndex = 19;
            // 
            // lblBmiCategoryValue
            // 
            lblBmiCategoryValue.AutoSize = true;
            lblBmiCategoryValue.Location = new Point(505, 295);
            lblBmiCategoryValue.Name = "lblBmiCategoryValue";
            lblBmiCategoryValue.Size = new Size(0, 20);
            lblBmiCategoryValue.TabIndex = 18;
            // 
            // lblBmiValue
            // 
            lblBmiValue.AutoSize = true;
            lblBmiValue.Location = new Point(441, 273);
            lblBmiValue.Name = "lblBmiValue";
            lblBmiValue.Size = new Size(0, 20);
            lblBmiValue.TabIndex = 17;
            // 
            // TrainerUserLogsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblWeightDiffValue);
            Controls.Add(lblTargetWeightValue);
            Controls.Add(lblBmiCategoryValue);
            Controls.Add(lblBmiValue);
            Controls.Add(dgvLogs);
            Controls.Add(btnLoad);
            Controls.Add(cmbUsers);
            Name = "TrainerUserLogsForm";
            Text = "TrainerUserLogsForm";
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbUsers;
        private Button btnLoad;
        private DataGridView dgvLogs;
        private Label label5;
        private Label label4;
        private Label label2;
        private Label label1;
        private Label lblWeightDiffValue;
        private Label lblTargetWeightValue;
        private Label lblBmiCategoryValue;
        private Label lblBmiValue;
    }
}