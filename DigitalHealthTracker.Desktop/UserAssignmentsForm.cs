using DigitalHealthTracker.Desktop.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalHealthTracker.Desktop
{
	public partial class UserAssignmentsForm : Form
	{
		private readonly UserAssignmentsApiService _api = new UserAssignmentsApiService();

		public UserAssignmentsForm()
		{
			InitializeComponent();
			ConfigureGrid();
		}

		protected override async void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			await LoadPendingAssignments();
		}

		private void ConfigureGrid()
		{
			dgvPending.AutoGenerateColumns = true;
			dgvPending.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvPending.MultiSelect = false;
			dgvPending.ReadOnly = true;
			dgvPending.AllowUserToAddRows = false;
		}

		private async Task LoadPendingAssignments()
		{
			if (Session.Role != AppRole.User || Session.UserId == null)
			{
				MessageBox.Show("Please login as User.");
				dgvPending.DataSource = null;
				return;
			}

			try
			{
				int userId = Session.UserId.Value;

				var list = await _api.GetPendingAsync(userId);

				dgvPending.DataSource = null;
				dgvPending.DataSource = list;

				if (dgvPending.Columns["Id"] != null)
					dgvPending.Columns["Id"].Visible = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Load Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private async void btnApproveSelected_Click(object sender, EventArgs e)
		{
			if (Session.Role != AppRole.User || Session.UserId == null)
			{
				MessageBox.Show("Please login as User.");
				return;
			}

			if (dgvPending.CurrentRow == null)
			{
				MessageBox.Show("Please select an assignment.");
				return;
			}

			int assignmentId = Convert.ToInt32(dgvPending.CurrentRow.Cells["Id"].Value);

			try
			{
				await _api.ApproveAsync(assignmentId, Session.UserId.Value);

				MessageBox.Show("Approved! Status is now ACTIVE.");
				await LoadPendingAssignments(); 
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Approve Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private async void btnRefresh_Click(object sender, EventArgs e)
		{
			await LoadPendingAssignments();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
