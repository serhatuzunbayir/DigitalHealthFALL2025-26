using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DigitalHealthTracker.Desktop.Services;

namespace DigitalHealthTracker.Desktop
{
	public partial class TrainerApprovalForm : Form
	{
		private readonly TrainerApiService _trainerApi = new TrainerApiService();

		public TrainerApprovalForm()
		{
			InitializeComponent();
		}

		protected override async void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			await LoadPendingTrainers();
		}

		private async Task LoadPendingTrainers()
		{
			try
			{
				var pendingTrainers = await _trainerApi.GetPendingAsync();

				// Senin eski Select anonimiyle aynı görünüm:
				var view = pendingTrainers
					.Where(t => t.IsApproved == false)
					.Select(t => new
					{
						t.Id,
						t.Name,
						t.Surname,
						t.Email
					})
					.ToList();

				dgvTrainers.DataSource = null;
				dgvTrainers.DataSource = view;
			}
			catch (Exception ex)
			{
				MessageBox.Show("API Pending Load Error: " + ex.Message);
			}
		}
		

		private async void btnApprove_Click(object sender, EventArgs e)
		{
			if (dgvTrainers.CurrentRow == null)
			{
				MessageBox.Show("Please select a trainer.");
				return;
			}

			int trainerId = (int)dgvTrainers.CurrentRow.Cells["Id"].Value;

			try
			{
				await _trainerApi.ApproveAsync(trainerId);
				MessageBox.Show("Trainer approved successfully.");
				await LoadPendingTrainers();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Approve Error: " + ex.Message);
			}
		}

		private async void btnRefresh_Click(object sender, EventArgs e)
		{
			await LoadPendingTrainers();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
