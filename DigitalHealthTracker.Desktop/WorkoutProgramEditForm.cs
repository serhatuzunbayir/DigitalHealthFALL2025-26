using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DigitalHealthTracker.Desktop
{
    public partial class WorkoutProgramEditForm : Form
    {

        private int trainerId;
        private int? programIdEdit;

        private readonly List<WorkoutProgramItem> tempItems = new List<WorkoutProgramItem>();

        public WorkoutProgramEditForm(int trainerId)
        {
            InitializeComponent();
            this.trainerId = trainerId;

            ConfigureItemsGrid();
            LoadWorkoutsToCombo();
        }

        public WorkoutProgramEditForm(int trainerId, WorkoutProgram tempProgram) : this(trainerId)
        {
            programIdEdit = tempProgram.Id;

            using (var context = new AppDbContext())
            {
                var program = context.WorkoutPrograms
                    .Include(p => p.Items)
                    .ThenInclude(i => i.Workout)
                    .SingleOrDefault(p => p.Id == tempProgram.Id);

                if (program == null)
                {
                    MessageBox.Show("Program not found in DataBase.");
                    Close();
                    return;
                }

                txtTitle.Text = program.Title;

                // Edit modunda item’ları temp listeye al
                tempItems.Clear();
                foreach (var it in program.Items)
                {
                    tempItems.Add(new WorkoutProgramItem
                    {
                        Id = it.Id, // var olan item’ları tanımak için
                        WorkoutId = it.WorkoutId,
                        DayNo = it.DayNo,
                        Sets = it.Sets,
                        Reps = it.Reps
                    });
                }

                RefreshItemsGrid();
            }
        }

        private void ConfigureItemsGrid()
        {
            dgvItems.AutoGenerateColumns = true;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItems.MultiSelect = false;
            dgvItems.ReadOnly = true;
            dgvItems.AllowUserToAddRows = false;
		}

		private void LoadWorkoutsToCombo()
        {
            using (var context = new AppDbContext())
            {
                var workouts = context.Workouts
                    .OrderBy(w => w.Name)
                    .Select(w => new { w.Id, w.Name })
                    .ToList();

                cmbWorkouts.DataSource = workouts;
                cmbWorkouts.DisplayMember = "Name";
                cmbWorkouts.ValueMember = "Id";
            }
        }

        private void RefreshItemsGrid()
        {
            using (var context = new AppDbContext())
            {
				// Grid’de workout name görünsün diye join’li view
				var view = tempItems
	                                .Select(it => new
	                                {
		                                it.WorkoutId,
		                                WorkoutName = context.Workouts
			                                .Where(w => w.Id == it.WorkoutId)
			                                .Select(w => w.Name)
			                                .FirstOrDefault(),
		                                it.DayNo,
		                                it.Sets,
		                                it.Reps
	                                })
	                                .OrderBy(x => x.DayNo)
	                                .ThenBy(x => x.WorkoutName)
	                                .ToList();

				dgvItems.DataSource = null;
                dgvItems.DataSource = view;
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (cmbWorkouts.SelectedValue == null)
            {
                MessageBox.Show("Please select a workout.");
                return;
            }

            if (!int.TryParse(txtDayNo.Text, out int dayNo) || dayNo < 1 || dayNo > 7)
            {
                MessageBox.Show("DayNo must be between 1 and 7.");
                return;
            }

            if (!int.TryParse(txtSets.Text, out int sets) || sets <= 0)
            {
                MessageBox.Show("Sets must be a positive number.");
                return;
            }

            if (!int.TryParse(txtReps.Text, out int reps) || reps <= 0)
            {
                MessageBox.Show("Reps must be a positive number.");
                return;
            }

            int workoutId = (int)cmbWorkouts.SelectedValue;

            tempItems.Add(new WorkoutProgramItem
            {
                WorkoutId = workoutId,
                DayNo = dayNo,
                Sets = sets,
                Reps = reps
            });

            RefreshItemsGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var title = txtTitle.Text.Trim();
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Program title is required.");
                return;
            }

            if (tempItems.Count == 0)
            {
                MessageBox.Show("Please add at least one workout item.");
                return;
            }

            using (var context = new AppDbContext())
            {
                WorkoutProgram program;

                if (programIdEdit == null)
                {
                    // ADD
                    program = new WorkoutProgram();
                    context.WorkoutPrograms.Add(program);
                }
                else
                {
                    // EDIT
                    program = context.WorkoutPrograms
                        .Include(p => p.Items)
                        .SingleOrDefault(p => p.Id == programIdEdit.Value);

                    if (program == null)
                    {
                        MessageBox.Show("Program not found in database.");
                        return;
                    }

                    // Eski item’ları temizle (en basit yöntem)
                    context.WorkoutProgramItems.RemoveRange(program.Items);
                }

                program.Title = title;
                program.TrainerId = trainerId;

                // Yeni item’ları ekle
                program.Items = tempItems.Select(it => new WorkoutProgramItem
                {
                    WorkoutId = it.WorkoutId,
                    DayNo = it.DayNo,
                    Sets = it.Sets,
                    Reps = it.Reps
                }).ToList();

                context.SaveChanges();
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
			if (dgvItems.CurrentRow == null)
			{
				MessageBox.Show("Please select an item to remove.");
				return;
			}

			int workoutId = Convert.ToInt32(dgvItems.CurrentRow.Cells["WorkoutId"].Value);
			int dayNo = Convert.ToInt32(dgvItems.CurrentRow.Cells["DayNo"].Value);
			int sets = Convert.ToInt32(dgvItems.CurrentRow.Cells["Sets"].Value);
			int reps = Convert.ToInt32(dgvItems.CurrentRow.Cells["Reps"].Value);

			var item = tempItems.FirstOrDefault(x =>
				x.WorkoutId == workoutId &&
				x.DayNo == dayNo &&
				x.Sets == sets &&
				x.Reps == reps);

			if (item == null)
			{
				MessageBox.Show("Selected item not found.");
				return;
			}

			tempItems.Remove(item);
			RefreshItemsGrid();
		}
    }
}
