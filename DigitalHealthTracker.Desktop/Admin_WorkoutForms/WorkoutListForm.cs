using DigitalHealthTracker.Data.Entities;
using DigitalHealthTracker.Desktop.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalHealthTracker.Desktop
{
	public partial class WorkoutListForm : Form
	{
		public delegate void WorkoutActionHandler(string message);
		public event WorkoutActionHandler? WorkoutChanged;

		private readonly WorkoutApiService _workoutApi = new WorkoutApiService();

		public WorkoutListForm()
		{
			InitializeComponent();
			ConfigureGrid();
			StyleGrid();
		}

		protected override async void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			await LoadWorkouts();
		}

		private void ConfigureGrid()
		{
			dgvWorkouts.AutoGenerateColumns = true;
			dgvWorkouts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvWorkouts.MultiSelect = false;
			dgvWorkouts.ReadOnly = true;
			dgvWorkouts.AllowUserToAddRows = false;
		}

		private void StyleGrid()
		{
			dgvWorkouts.EnableHeadersVisualStyles = false;
			dgvWorkouts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 170, 170);
			dgvWorkouts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
			dgvWorkouts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

			dgvWorkouts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 248);
			dgvWorkouts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 150, 150);
			dgvWorkouts.DefaultCellStyle.SelectionForeColor = Color.White;
		}

		private async Task LoadWorkouts()
		{
			try
			{
				var workouts = await _workoutApi.GetAllAsync();

				dgvWorkouts.DataSource = null;
				dgvWorkouts.DataSource = workouts.OrderBy(w => w.Id).ToList();
			}
			catch (Exception ex)
			{
				MessageBox.Show("API Workout Load Error: " + ex.Message);
			}
		}

		private Workout? GetSelectedWorkout()
		{
			return dgvWorkouts.CurrentRow?.DataBoundItem as Workout;
		}

		private async void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				using (var frm = new WorkoutEditForm())
				{
					if (frm.ShowDialog() != DialogResult.OK) return;

					await _workoutApi.CreateAsync(frm.EditedWorkout);
					await LoadWorkouts();
					WorkoutChanged?.Invoke("A new workout was added.");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Add Workout Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private async void btnEdit_Click(object sender, EventArgs e)
		{
			var selectedWorkout = GetSelectedWorkout();

			if (selectedWorkout == null)
			{
				MessageBox.Show("Please select a workout to edit...");
				return;
			}

			try
			{
				using (var frm = new WorkoutEditForm(selectedWorkout))
				{
					if (frm.ShowDialog() != DialogResult.OK) return;

					await _workoutApi.UpdateAsync(selectedWorkout.Id, frm.EditedWorkout);
					await LoadWorkouts();
					WorkoutChanged?.Invoke($"Workout '{selectedWorkout.Name}' was updated.");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Edit Workout Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private async void btnDelete_Click(object sender, EventArgs e)
		{
			var selectedWorkout = GetSelectedWorkout();

			if (selectedWorkout == null)
			{
				MessageBox.Show("Please select a workout to delete...");
				return;
			}

			var result = MessageBox.Show(
				$"Are you sure to delete workout: '{selectedWorkout.Name}' ?",
				"Delete Confirmation",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (result != DialogResult.Yes)
				return;

			try
			{
				await _workoutApi.DeleteAsync(selectedWorkout.Id);
				await LoadWorkouts();
				WorkoutChanged?.Invoke($"Workout '{selectedWorkout.Name}' was deleted.");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Delete Workout Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
