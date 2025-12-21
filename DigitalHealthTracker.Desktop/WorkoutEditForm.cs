using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
namespace DigitalHealthTracker.Desktop
{
	public partial class WorkoutEditForm : Form
	{
		private int? workoutIdEdit;
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
			if(string.IsNullOrEmpty(txtDescription.Text))
			{
				MessageBox.Show("Workout description is required.");
				return;
			}

			using (var context = new AppDbContext())
			{
				Workout workout;

				if (workoutIdEdit == null)
				{
					// ADD
					workout = new Workout();
					context.Workouts.Add(workout);
				}
				else
				{
					// EDIT
					workout = context.Workouts.Find(workoutIdEdit.Value);

					if (workout == null)
					{
						MessageBox.Show("Workout not found in DataBase.");
						return;
					}
				}

				workout.Name = txtName.Text.Trim();
				workout.Description = txtDescription.Text.Trim();

				context.SaveChanges();
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}