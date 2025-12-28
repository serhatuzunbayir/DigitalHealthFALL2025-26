namespace DigitalHealthTracker.Desktop
{
    partial class WorkoutEditForm
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
            txtName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtDescription = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            label3 = new Label();
            panel1 = new Panel();
            label4 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(337, 144);
            txtName.Name = "txtName";
            txtName.Size = new Size(170, 27);
            txtName.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(110, 144);
            label1.Name = "label1";
            label1.Size = new Size(126, 20);
            label1.TabIndex = 1;
            label1.Text = "*Workout Name : ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(110, 240);
            label2.Name = "label2";
            label2.Size = new Size(158, 20);
            label2.TabIndex = 2;
            label2.Text = "*Workout Description :";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(337, 237);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(170, 27);
            txtDescription.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.Teal;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnSave.ForeColor = Color.WhiteSmoke;
            btnSave.Location = new Point(504, 328);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 50);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Teal;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnCancel.ForeColor = Color.WhiteSmoke;
            btnCancel.Location = new Point(639, 328);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 50);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Close";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(126, 361);
            label3.Name = "label3";
            label3.Size = new Size(155, 20);
            label3.TabIndex = 6;
            label3.Text = "'*' fields are required...";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Teal;
            panel1.Controls.Add(label4);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 85);
            panel1.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label4.ForeColor = Color.WhiteSmoke;
            label4.Location = new Point(25, 9);
            label4.Name = "label4";
            label4.Size = new Size(327, 41);
            label4.TabIndex = 0;
            label4.Text = "Digital Health Tracker";
            // 
            // WorkoutEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(label3);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtDescription);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtName);
            Name = "WorkoutEditForm";
            Text = "WorkoutEditForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtName;
        private Label label1;
        private Label label2;
        private TextBox txtDescription;
        private Button btnSave;
        private Button btnCancel;
        private Label label3;
        private Panel panel1;
        private Label label4;
    }
}