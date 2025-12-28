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
            btnClose = new Button();
            panel1 = new Panel();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // cmbUsers
            // 
            cmbUsers.FormattingEnabled = true;
            cmbUsers.Location = new Point(379, 346);
            cmbUsers.Name = "cmbUsers";
            cmbUsers.Size = new Size(279, 28);
            cmbUsers.TabIndex = 0;
            // 
            // btnLoad
            // 
            btnLoad.BackColor = Color.Teal;
            btnLoad.FlatStyle = FlatStyle.Flat;
            btnLoad.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnLoad.ForeColor = Color.WhiteSmoke;
            btnLoad.Location = new Point(664, 341);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(125, 35);
            btnLoad.TabIndex = 1;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = false;
            btnLoad.Click += btnLoad_Click;
            // 
            // dgvLogs
            // 
            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogs.Location = new Point(0, 85);
            dgvLogs.Name = "dgvLogs";
            dgvLogs.RowHeadersWidth = 51;
            dgvLogs.Size = new Size(800, 250);
            dgvLogs.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(226, 391);
            label5.Name = "label5";
            label5.Size = new Size(92, 20);
            label5.TabIndex = 24;
            label5.Text = "Weight Diff: ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(226, 369);
            label4.Name = "label4";
            label4.Size = new Size(108, 20);
            label4.TabIndex = 23;
            label4.Text = "Target Weight: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 391);
            label2.Name = "label2";
            label2.Size = new Size(106, 20);
            label2.TabIndex = 22;
            label2.Text = "BMI Category: ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 369);
            label1.Name = "label1";
            label1.Size = new Size(42, 20);
            label1.TabIndex = 21;
            label1.Text = "BMI: ";
            // 
            // lblWeightDiffValue
            // 
            lblWeightDiffValue.AutoSize = true;
            lblWeightDiffValue.Location = new Point(324, 391);
            lblWeightDiffValue.Name = "lblWeightDiffValue";
            lblWeightDiffValue.Size = new Size(0, 20);
            lblWeightDiffValue.TabIndex = 20;
            // 
            // lblTargetWeightValue
            // 
            lblTargetWeightValue.AutoSize = true;
            lblTargetWeightValue.Location = new Point(340, 365);
            lblTargetWeightValue.Name = "lblTargetWeightValue";
            lblTargetWeightValue.Size = new Size(0, 20);
            lblTargetWeightValue.TabIndex = 19;
            // 
            // lblBmiCategoryValue
            // 
            lblBmiCategoryValue.AutoSize = true;
            lblBmiCategoryValue.Location = new Point(134, 391);
            lblBmiCategoryValue.Name = "lblBmiCategoryValue";
            lblBmiCategoryValue.Size = new Size(0, 20);
            lblBmiCategoryValue.TabIndex = 18;
            // 
            // lblBmiValue
            // 
            lblBmiValue.AutoSize = true;
            lblBmiValue.Location = new Point(70, 369);
            lblBmiValue.Name = "lblBmiValue";
            lblBmiValue.Size = new Size(0, 20);
            lblBmiValue.TabIndex = 17;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.Teal;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnClose.ForeColor = Color.WhiteSmoke;
            btnClose.Location = new Point(664, 391);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(125, 35);
            btnClose.TabIndex = 25;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Teal;
            panel1.Controls.Add(label3);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 85);
            panel1.TabIndex = 26;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label3.ForeColor = Color.WhiteSmoke;
            label3.Location = new Point(25, 9);
            label3.Name = "label3";
            label3.Size = new Size(327, 41);
            label3.TabIndex = 0;
            label3.Text = "Digital Health Tracker";
            // 
            // TrainerUserLogsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(btnClose);
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
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private Button btnClose;
        private Panel panel1;
        private Label label3;
    }
}