using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DigitalHealthTracker.Data.Entities;
using DigitalHealthTracker.Desktop.Services;

namespace DigitalHealthTracker.Desktop
{
	public partial class UserListForm : Form
	{
		public delegate void UserActionHandler(string message);
		public event UserActionHandler? UserChanged;

		private readonly UserApiService _userApi = new UserApiService();

		public UserListForm()
		{
			InitializeComponent();
			ConfigureGrid();
			StyleGrid();
		}

		protected override async void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			await LoadUsers();
		}

		private void ConfigureGrid()
		{
			dgvUsers.AutoGenerateColumns = true;
			dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvUsers.MultiSelect = false;
			dgvUsers.ReadOnly = true;
			dgvUsers.AllowUserToAddRows = false;
		}

		private async Task LoadUsers()
		{
			try
			{
				var users = await _userApi.GetAllAsync();

				dgvUsers.DataSource = null;
				dgvUsers.DataSource = users.OrderBy(u => u.Id).ToList();
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					"API bağlantı hatası:\n" + ex.Message,
					"API Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
			}
		}

		private void StyleGrid()
		{
			dgvUsers.EnableHeadersVisualStyles = false;
			dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 170, 170);
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
			// Bilerek boş — Designer bağlıysa patlamasın diye duruyor
		}

		private async void btnEditUser_Click(object sender, EventArgs e)
		{
			var selectedUser = GetSelectedUser();

			if (selectedUser == null)
			{
				MessageBox.Show("Please select a user to edit...");
				return;
			}

			try
			{
				using (var frm = new UserEditForm(selectedUser))
				{
					var result = frm.ShowDialog();

					if (result != DialogResult.OK)
						return;

					// 🔴 ASIL OLAY BURASI
					await _userApi.UpdateAsync(selectedUser.Id, frm.EditedUser);

					// listeyi yenile
					await LoadUsers();

					UserChanged?.Invoke(
						$"User '{selectedUser.Name} {selectedUser.Surname}' was updated."
					);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					ex.Message,
					"Edit Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
			}
		}

		private async void btnDeleteUser_Click(object sender, EventArgs e)
		{
			var selectedUser = GetSelectedUser();

			if (selectedUser == null)
			{
				MessageBox.Show("Please select a user to edit...");
				return;
			}

			try
			{
				using (var frm = new UserEditForm(selectedUser))
				{
					var result = frm.ShowDialog();

					if (result == DialogResult.OK)
					{
						await _userApi.UpdateAsync(selectedUser.Id, frm.EditedUser);
						await LoadUsers();
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

		private async void btnAddUser_Click(object sender, EventArgs e)
		{
			try
			{
				using (var frm = new UserEditForm())
				{
					var result = frm.ShowDialog();

					if (result == DialogResult.OK)
					{
						await _userApi.CreateAsync(frm.EditedUser);
						await LoadUsers();
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
