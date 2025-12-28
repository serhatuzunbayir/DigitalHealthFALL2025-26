using DigitalHealthTracker.Data.Entities;
using System;
using System.Windows.Forms;

namespace DigitalHealthTracker.Desktop
{
	public partial class WorkoutEditForm : Form
	{
		private int? workoutIdEdit;
		private Workout _workout = new Workout();

		public Workout EditedWorkout => _workout;

		public WorkoutEditForm()
		{
			InitializeComponent();
		}

		public WorkoutEditForm(Workout tempWorkout) : this()
		{
			workoutIdEdit = tempWorkout.Id;
			txtName.Text = tempWorkout.Name;
			txtDescription.Text = tempWorkout.Description;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtName.Text))
			{
				MessageBox.Show("Workout name is required.");
				return;
			}
			if (string.IsNullOrWhiteSpace(txtDescription.Text))
			{
				MessageBox.Show("Workout description is required.");
				return;
			}

			_workout = new Workout
			{
				Id = workoutIdEdit ?? 0,
				Name = txtName.Text.Trim(),
				Description = txtDescription.Text.Trim()
			};

			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
