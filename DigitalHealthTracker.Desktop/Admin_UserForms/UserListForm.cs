using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;



namespace DigitalHealthTracker.Desktop
{
    public partial class UserListForm : Form
    {
		public delegate void UserActionHandler(string message);
		public event UserActionHandler? UserChanged;

		public UserListForm()
		{
			InitializeComponent();
			ConfigureGrid();
			StyleGrid();
			LoadUsers();
		}

		private void ConfigureGrid()
		{
			dgvUsers.AutoGenerateColumns = true;
			dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvUsers.MultiSelect = false;
			dgvUsers.ReadOnly = true;
			dgvUsers.AllowUserToAddRows = false;
		}

		private void LoadUsers()
        {
            using (var context = new AppDbContext())
            {
				// kayıtlar (id) sıralı gelsin diye OrderBy ekledim
				var users = context.Users.OrderBy(u => u.Id).ToList();

				// Önce DataSource’u sıfırla, sonra yeniden ata
				dgvUsers.DataSource = null;
				dgvUsers.DataSource = users;
			}
        }

		private void StyleGrid()
		{
			dgvUsers.EnableHeadersVisualStyles = false;
			dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 170, 170);  // Turkuaz
			dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
			dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

			dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 248);
			dgvUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 150, 150);
			dgvUsers.DefaultCellStyle.SelectionForeColor = Color.White;
		}

		private User? GetSelectedUser()
		{
			if (dgvUsers.CurrentRow == null)
				return null;

			return dgvUsers.CurrentRow.DataBoundItem as User;
		}

		private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
			if (dgvUsers.CurrentRow == null)
			{
				MessageBox.Show("Please select a user to edit...");
				return;
			}

			var selectedUser = GetSelectedUser();

			if (selectedUser == null)
			{
				MessageBox.Show("Selected user is invalid...");
				return;
			}

			try
			{
				using (var frm = new UserEditForm(selectedUser))
				{
					var result = frm.ShowDialog();

					if (result == DialogResult.OK)
					{
						// Güncellemeden sonra listeyi yenile
						LoadUsers();
						//El Yapımı Event
						UserChanged?.Invoke($"User '{selectedUser.Name} {selectedUser.Surname}' was updated.");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Edit Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {

            if(dgvUsers.CurrentRow == null)
            {
                MessageBox.Show("Please select a user to delete...");
                return;
            }


			//kullanıcıyı seçtik
			var selectedUser = GetSelectedUser();

            if(selectedUser == null)
            {
                MessageBox.Show("Selected user is invalid...");
                return;
			}


            //Son Emin misin onayı
			var result = MessageBox.Show($"Are you sure to delete user named: '{selectedUser.Name} {selectedUser.Surname}' ?","Delete Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(result != DialogResult.Yes)
            {
                MessageBox.Show("User deletion cancelled...");
				return;
            }

			try
			{
				//DataBey'den kullanıcıyı varsa sil
				using (var context = new AppDbContext())
				{
					var user = context.Users.SingleOrDefault(u => u.Id == selectedUser.Id);

					if (user == null)
					{
						MessageBox.Show("Kullanıcı veritabanında bulunamadı.");
						return;
					}

					context.Users.Remove(user);
					context.SaveChanges();
				}

				LoadUsers(); //  Değşiklikten sonra DB'i tekrar yükle..
				//El Yapımı Event
				UserChanged?.Invoke($"User '{selectedUser.Name} {selectedUser.Surname}' deleted.");

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnAddUser_Click(object sender, EventArgs e)
        {
			try
			{
				using (var frm = new UserEditForm())
				{
					var result = frm.ShowDialog();

					if (result == DialogResult.OK)
					{
						LoadUsers();

						//El Yapımı Event
						UserChanged?.Invoke("A new user was added.");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Add Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
    }
}
