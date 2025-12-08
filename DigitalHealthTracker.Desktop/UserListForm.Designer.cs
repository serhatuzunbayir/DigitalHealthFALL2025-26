namespace DigitalHealthTracker.Desktop
{
    partial class UserListForm
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
            dgvUsers = new DataGridView();
            btnAddUser = new Button();
            btnEditUser = new Button();
            btnDeleteUser = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            SuspendLayout();
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Dock = DockStyle.Top;
            dgvUsers.Location = new Point(0, 0);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(882, 350);
            dgvUsers.TabIndex = 0;
            dgvUsers.CellContentClick += dgvUsers_CellContentClick;
            // 
            // btnAddUser
            // 
            btnAddUser.BackColor = Color.Teal;
            btnAddUser.FlatStyle = FlatStyle.Flat;
            btnAddUser.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnAddUser.ForeColor = Color.WhiteSmoke;
            btnAddUser.Location = new Point(220, 380);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(135, 50);
            btnAddUser.TabIndex = 1;
            btnAddUser.Text = "Add User";
            btnAddUser.UseVisualStyleBackColor = false;
            btnAddUser.Click += btnAddUser_Click;
            // 
            // btnEditUser
            // 
            btnEditUser.BackColor = Color.Teal;
            btnEditUser.FlatStyle = FlatStyle.Flat;
            btnEditUser.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnEditUser.ForeColor = Color.WhiteSmoke;
            btnEditUser.Location = new Point(380, 380);
            btnEditUser.Name = "btnEditUser";
            btnEditUser.Size = new Size(135, 50);
            btnEditUser.TabIndex = 2;
            btnEditUser.Text = "Edit User";
            btnEditUser.UseVisualStyleBackColor = false;
            btnEditUser.Click += btnEditUser_Click;
            // 
            // btnDeleteUser
            // 
            btnDeleteUser.BackColor = Color.Teal;
            btnDeleteUser.FlatStyle = FlatStyle.Flat;
            btnDeleteUser.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnDeleteUser.ForeColor = Color.WhiteSmoke;
            btnDeleteUser.Location = new Point(540, 380);
            btnDeleteUser.Name = "btnDeleteUser";
            btnDeleteUser.Size = new Size(135, 50);
            btnDeleteUser.TabIndex = 3;
            btnDeleteUser.Text = "Delete User";
            btnDeleteUser.UseVisualStyleBackColor = false;
            btnDeleteUser.Click += btnDeleteUser_Click;
            // 
            // UserListForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(882, 453);
            Controls.Add(btnDeleteUser);
            Controls.Add(btnEditUser);
            Controls.Add(btnAddUser);
            Controls.Add(dgvUsers);
            Name = "UserListForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "User List Panel";
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvUsers;
        private Button btnAddUser;
        private Button btnEditUser;
        private Button btnDeleteUser;
    }
}