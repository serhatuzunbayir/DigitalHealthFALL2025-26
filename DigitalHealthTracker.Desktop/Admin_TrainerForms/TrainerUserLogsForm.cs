using DigitalHealthTracker.Desktop.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DigitalHealthTracker.Desktop.Admin_TrainerForms
{
    public partial class TrainerUserLogsForm : Form
    {
		private readonly UserLookupApiService _lookupApi = new UserLookupApiService();
		private readonly WorkoutLogApiService _logApi = new WorkoutLogApiService();

		public TrainerUserLogsForm()
		{
			InitializeComponent();
			ConfigureGrid();
			StyleGrid();
		}

		protected override async void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			await LoadUsersAsync();
		}

		private void ConfigureGrid()
		{
			dgvLogs.AutoGenerateColumns = true;
			dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvLogs.MultiSelect = false;
			dgvLogs.ReadOnly = true;
			dgvLogs.AllowUserToAddRows = false;
		}

		private void StyleGrid()
		{
			dgvLogs.EnableHeadersVisualStyles = false;
			dgvLogs.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 170, 170);
			dgvLogs.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
			dgvLogs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

			dgvLogs.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 248);
			dgvLogs.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 150, 150);
			dgvLogs.DefaultCellStyle.SelectionForeColor = Color.White;
		}

		private async Task LoadUsersAsync()
		{
			if (Session.TrainerId == null)
			{
				MessageBox.Show("Session error: TrainerId is null.");
				return;
			}

			var users = await _lookupApi.GetAssignedToTrainerAsync(Session.TrainerId.Value);

			cmbUsers.DataSource = null;
			cmbUsers.DisplayMember = "FullName";
			cmbUsers.ValueMember = "Id";
			cmbUsers.DataSource = users;
		}
		private async void btnLoad_Click(object sender, EventArgs e)
        {
			if (cmbUsers.SelectedItem is not UserLookupDto user)
			{
				MessageBox.Show("Please select a user.");
				return;
			}

			// 1) Logs
			var logs = await _logApi.GetByUserAsync(user.Id);
			dgvLogs.DataSource = logs;

			// 2) BMI Panel (canlı = her Load'ta güncel)
			UpdateMetrics(user);
		}

		private void UpdateMetrics(UserLookupDto user)
		{
			if (user.HeightCm <= 0 || user.WeightKg <= 0)
			{
				lblBmiValue.Text = "-";
				lblTargetWeightValue.Text = "-";
				lblWeightDiffValue.Text = "-";
				lblBmiCategoryValue.Text = "-";
				return;
			}

			var h = user.HeightCm / 100.0;
			var bmi = user.WeightKg / (h * h);
			var target = 22.0 * h * h;
			var diff = target - user.WeightKg;

			lblBmiValue.Text = bmi.ToString("0.00");
			lblTargetWeightValue.Text = target.ToString("0.0");
			lblWeightDiffValue.Text = diff.ToString("0.0");

			lblBmiCategoryValue.Text =
				bmi < 18.5 ? "Underweight" :
				bmi < 25 ? "Normal" :
				bmi < 30 ? "Overweight" : "Obese";
		}
	}
}
