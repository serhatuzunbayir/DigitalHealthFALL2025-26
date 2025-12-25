using DigitalHealthTracker.Desktop.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalHealthTracker.Desktop
{
	public partial class AssignProgramForm : Form
	{
		private readonly UserLookupApiService _userLookupApi = new UserLookupApiService();
		private readonly WorkoutProgramApiService _programApi = new WorkoutProgramApiService();
		private readonly AssignedProgramApiService _assignedApi = new AssignedProgramApiService();

		public AssignProgramForm()
		{
			InitializeComponent();

			ConfigureGrid();
			StyleGrid();
		}

		// Designer Load event’i buna bağlı olabilir diye ekliyoruz
		private async void AssignProgramForm_Load(object sender, EventArgs e)
		{
			await LoadAll();
		}

		protected override async void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			await LoadAll();
		}

		private void ConfigureGrid()
		{
			dgvAssignments.AutoGenerateColumns = true;
			dgvAssignments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvAssignments.MultiSelect = false;
			dgvAssignments.ReadOnly = true;
			dgvAssignments.AllowUserToAddRows = false;
		}

		private void StyleGrid()
		{
			dgvAssignments.EnableHeadersVisualStyles = false;
			dgvAssignments.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 170, 170);
			dgvAssignments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
			dgvAssignments.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

			dgvAssignments.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 248);
			dgvAssignments.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 150, 150);
			dgvAssignments.DefaultCellStyle.SelectionForeColor = Color.White;
		}

		private int? GetLoggedInTrainerId()
		{
			if (Session.Role != AppRole.Trainer || Session.TrainerId == null)
				return null;

			return Session.TrainerId.Value;
		}

		private int? GetSelectedUserId()
		{
			if (cmbUsers.SelectedValue == null) return null;
			return (int)cmbUsers.SelectedValue;
		}

		private int? GetSelectedProgramId()
		{
			if (cmbPrograms.SelectedValue == null) return null;
			return (int)cmbPrograms.SelectedValue;
		}

		private async Task LoadAll()
		{
			await LoadUsers();
			await LoadProgramsForLoggedInTrainer();
			await LoadAssignmentsForLoggedInTrainer();
		}

		private async Task LoadUsers()
		{
			var users = await _userLookupApi.GetLookupAsync();
			cmbUsers.DataSource = users;
			cmbUsers.DisplayMember = "FullName";
			cmbUsers.ValueMember = "Id";
		}

		private async Task LoadProgramsForLoggedInTrainer()
		{
			var trainerId = GetLoggedInTrainerId();
			if (trainerId == null)
			{
				cmbPrograms.DataSource = null;
				MessageBox.Show("Trainer session not found. Please login as Trainer.");
				return;
			}

			var programs = await _programApi.GetByTrainerAsync(trainerId.Value);

			cmbPrograms.DataSource = programs;
			cmbPrograms.DisplayMember = "Title";
			cmbPrograms.ValueMember = "Id";
		}

		private async Task LoadAssignmentsForLoggedInTrainer()
		{
			var trainerId = GetLoggedInTrainerId();
			if (trainerId == null)
			{
				dgvAssignments.DataSource = null;
				return;
			}

			var list = await _assignedApi.GetForTrainerAsync(trainerId.Value);

			dgvAssignments.DataSource = null;
			dgvAssignments.DataSource = list;

			if (dgvAssignments.Columns["Id"] != null)
				dgvAssignments.Columns["Id"].Visible = false;
		}

		private async void btnAssign_Click(object sender, EventArgs e)
		{
			var trainerId = GetLoggedInTrainerId();
			var userId = GetSelectedUserId();
			var programId = GetSelectedProgramId();

			if (trainerId == null) { MessageBox.Show("Please login as Trainer."); return; }
			if (userId == null) { MessageBox.Show("Please select a user."); return; }
			if (programId == null) { MessageBox.Show("Please select a program."); return; }

			try
			{
				await _assignedApi.AssignAsync(trainerId.Value, userId.Value, programId.Value);

				MessageBox.Show("Program assigned successfully. (Status: Pending)");
				await LoadAssignmentsForLoggedInTrainer();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Assign Program Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private async void btnRefresh_Click(object sender, EventArgs e)
		{
			await LoadProgramsForLoggedInTrainer();
			await LoadAssignmentsForLoggedInTrainer();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private async void btnDeleteAssignment_Click(object sender, EventArgs e)
		{
			if (dgvAssignments.CurrentRow == null)
			{
				MessageBox.Show("Please select an assignment.");
				return;
			}

			int assignmentId = Convert.ToInt32(dgvAssignments.CurrentRow.Cells["Id"].Value);

			var confirm = MessageBox.Show("Delete selected assignment?", "Confirm",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (confirm != DialogResult.Yes)
				return;

			var trainerId = GetLoggedInTrainerId();
			if (trainerId == null)
			{
				MessageBox.Show("Please login as Trainer.");
				return;
			}

			try
			{
				await _assignedApi.DeleteAsync(assignmentId, trainerId.Value);

				MessageBox.Show("Assignment deleted.");
				await LoadAssignmentsForLoggedInTrainer();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Delete Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
