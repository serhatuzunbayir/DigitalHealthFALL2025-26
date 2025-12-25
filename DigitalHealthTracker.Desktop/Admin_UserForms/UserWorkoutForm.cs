using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DigitalHealthTracker.Desktop.Services;

namespace DigitalHealthTracker.Desktop
{
	public partial class UserWorkoutForm : Form
	{
		private readonly AssignedProgramApiService _assignedApi = new AssignedProgramApiService();
		private readonly WorkoutLogApiService _logApi = new WorkoutLogApiService();

		private ActiveAssignmentDto? _active;

		public UserWorkoutForm()
		{
			InitializeComponent();

			ConfigureGrid();
			StyleGrid();
		}

		protected override async void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			await RefreshAll();
		}

		private void ConfigureGrid()
		{
			dgvHistory.AutoGenerateColumns = true;
			dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvHistory.MultiSelect = false;
			dgvHistory.ReadOnly = true;
			dgvHistory.AllowUserToAddRows = false;
		}

		private void StyleGrid()
		{
			dgvHistory.EnableHeadersVisualStyles = false;
			dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 170, 170);
			dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
			dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

			dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 248);
			dgvHistory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 150, 150);
			dgvHistory.DefaultCellStyle.SelectionForeColor = Color.White;
		}

		private int? GetLoggedInUserId()
		{
			if (Session.Role != AppRole.User || Session.UserId == null)
				return null;

			return Session.UserId.Value;
		}

		private async Task RefreshAll()
		{
			await LoadActiveProgram();
			await LoadWorkoutHistory();
		}

		private async Task LoadActiveProgram()
		{
			var userId = GetLoggedInUserId();
			if (userId == null)
			{
				lblActiveProgram.Text = "Active Program: - (Please login as User)";
				_active = null;
				return;
			}

			try
			{
				_active = await _assignedApi.GetActiveForUserAsync(userId.Value);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "API ERROR (GetActive)", MessageBoxButtons.OK, MessageBoxIcon.Error);
				_active = null;
				lblActiveProgram.Text = "Active Program: - (API error)";
				return;
			}

			if (_active == null)
			{
				lblActiveProgram.Text = "Active Program: - (No active assignment)";
				return;
			}

			lblActiveProgram.Text = $"Active Program: {_active.ProgramTitle} (Trainer: {_active.TrainerName})";
		}

		private async Task LoadWorkoutHistory()
		{
			var userId = GetLoggedInUserId();
			if (userId == null)
			{
				dgvHistory.DataSource = null;
				return;
			}

			try
			{
				var logs = await _logApi.GetUserHistoryAsync(userId.Value);

				dgvHistory.DataSource = null;
				dgvHistory.DataSource = logs;

				if (dgvHistory.Columns["Id"] != null)
					dgvHistory.Columns["Id"].Visible = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "API ERROR (Logs)", MessageBoxButtons.OK, MessageBoxIcon.Error);
				dgvHistory.DataSource = null;
			}
		}

		// ✅ tek tıkla: assignment Completed + programın tüm itemları loglanır
		private async void btnCompleteDay_Click(object sender, EventArgs e)
		{
			var userId = GetLoggedInUserId();
			if (userId == null)
			{
				MessageBox.Show("Please login as User.");
				return;
			}

			if (_active == null)
			{
				MessageBox.Show("No active program found.");
				return;
			}

			try
			{
				await _assignedApi.CompleteWithLogsAsync(_active.Id, userId.Value);

				MessageBox.Show("Program COMPLETED. All workouts logged ✅");
				await RefreshAll();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Complete Program Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private async void btnRefreshHistory_Click(object sender, EventArgs e)
		{
			await RefreshAll();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
