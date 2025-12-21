using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DigitalHealthTracker.Desktop
{
	public partial class WorkoutListForm : Form
	{
		public delegate void WorkoutActionHandler(string message);
		public event WorkoutActionHandler? WorkoutChanged;

		public WorkoutListForm()
		{
			InitializeComponent();
			ConfigureGrid();
			StyleGrid();
			LoadWorkouts();
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

		private void LoadWorkouts()
		{
			using (var context = new AppDbContext())
			{
				var workouts = context.Workouts
					.OrderBy(w => w.Id)
					.ToList();

				dgvWorkouts.DataSource = null;
				dgvWorkouts.DataSource = workouts;
			}
		}

		private Workout? GetSelectedWorkout()
		{
			if (dgvWorkouts.CurrentRow == null)
				return null;

			return dgvWorkouts.CurrentRow.DataBoundItem as Workout;
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				using (var frm = new WorkoutEditForm())
				{
					var result = frm.ShowDialog();

					if (result == DialogResult.OK)
					{
						LoadWorkouts();
						WorkoutChanged?.Invoke("A new workout was added.");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Add Workout Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
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
					var result = frm.ShowDialog();

					if (result == DialogResult.OK)
					{
						LoadWorkouts();
						WorkoutChanged?.Invoke($"Workout '{selectedWorkout.Name}' was updated.");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Edit Workout Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
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
				using (var context = new AppDbContext())
				{
					var workout = context.Workouts
						.SingleOrDefault(w => w.Id == selectedWorkout.Id);

					if (workout == null)
					{
						MessageBox.Show("Workout not found in database.");
						return;
					}

					context.Workouts.Remove(workout);
					context.SaveChanges();
				}

				LoadWorkouts();
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