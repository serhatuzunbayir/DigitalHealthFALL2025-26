using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DigitalHealthTracker.Desktop
{
    public partial class WorkoutProgramListForm : Form
    {
        public WorkoutProgramListForm()
        {
            InitializeComponent();
            ConfigureGrid();
            StyleGrid();
            LoadTrainersToCombo();
        }

        private void ConfigureGrid()
        {
            dgvPrograms.AutoGenerateColumns = true;
            dgvPrograms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPrograms.MultiSelect = false;
            dgvPrograms.ReadOnly = true;
            dgvPrograms.AllowUserToAddRows = false;
        }

        private void StyleGrid()
        {
            dgvPrograms.EnableHeadersVisualStyles = false;
            dgvPrograms.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 170, 170);
            dgvPrograms.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPrograms.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvPrograms.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 248);
            dgvPrograms.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 150, 150);
            dgvPrograms.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void LoadTrainersToCombo()
        {
            using (var context = new AppDbContext())
            {
				var trainers = context.Trainers
	                                     .Where(t => t.IsApproved)
	                                     .OrderBy(t => t.Id)
	                                     .Select(t => new
	                                     {
		                                     t.Id,
		                                     FullName = t.Name + " " + t.Surname + " (" + t.Email + ")"
	                                     })
	                                     .ToList();

				cmbTrainers.DataSource = trainers;
				cmbTrainers.DisplayMember = "FullName";
				cmbTrainers.ValueMember = "Id";
			}
        }

        private int? GetSelectedTrainerId()
        {
            if (cmbTrainers.SelectedValue == null) return null;
            return (int)cmbTrainers.SelectedValue;
        }

        private WorkoutProgram? GetSelectedProgram()
        {
            if (dgvPrograms.CurrentRow == null) return null;
            return dgvPrograms.CurrentRow.DataBoundItem as WorkoutProgram;
        }

        private void LoadPrograms()
        {
            var trainerId = GetSelectedTrainerId();
            if (trainerId == null)
            {
                MessageBox.Show("Please select a trainer.");
                return;
            }

            using (var context = new AppDbContext())
            {
                var programs = context.WorkoutPrograms
                    .Include(p => p.Trainer)
                    .Where(p => p.TrainerId == trainerId.Value)
                    .OrderBy(p => p.Id)
                    .ToList();

                dgvPrograms.DataSource = null;
                dgvPrograms.DataSource = programs;
            }

			if (dgvPrograms.Columns["Trainer"] != null) dgvPrograms.Columns["Trainer"].Visible = false;
			if (dgvPrograms.Columns["Items"] != null) dgvPrograms.Columns["Items"].Visible = false;

		}

		private void btnLoadPrograms_Click(object sender, EventArgs e)
        {
            LoadPrograms();
        }

        private void btnAddProgram_Click(object sender, EventArgs e)
        {
            var trainerId = GetSelectedTrainerId();
            if (trainerId == null)
            {
                MessageBox.Show("Please select a trainer.");
                return;
            }

            try
            {
                using (var frm = new WorkoutProgramEditForm(trainerId.Value))
                {
                    var result = frm.ShowDialog();
                    if (result == DialogResult.OK)
                        LoadPrograms();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Add Program Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditProgram_Click(object sender, EventArgs e)
        {
            var trainerId = GetSelectedTrainerId();
            if (trainerId == null)
            {
                MessageBox.Show("Please select a trainer.");
                return;
            }

            var selectedProgram = GetSelectedProgram();
            if (selectedProgram == null)
            {
                MessageBox.Show("Please select a program to edit...");
                return;
            }

            try
            {
                using (var frm = new WorkoutProgramEditForm(trainerId.Value, selectedProgram))
                {
                    var result = frm.ShowDialog();
                    if (result == DialogResult.OK)
                        LoadPrograms();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Edit Program Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteProgram_Click(object sender, EventArgs e)
        {
			var selectedProgram = GetSelectedProgram();
			if (selectedProgram == null)
			{
				MessageBox.Show("Please select a program to delete...");
				return;
			}

			var result = MessageBox.Show(
				$"Are you sure to delete program: '{selectedProgram.Title}' ?",
				"Delete Confirmation",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (result != DialogResult.Yes)
				return;

			try
			{
				using (var context = new AppDbContext())
				{
					var program = context.WorkoutPrograms
						.Include(p => p.Items)
						.SingleOrDefault(p => p.Id == selectedProgram.Id);

					if (program == null)
					{
						MessageBox.Show("Program not found in database.");
						return;
					}

					context.WorkoutPrograms.Remove(program); // cascade ile item’lar da silinir
					context.SaveChanges();
				}

				LoadPrograms();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Delete Program Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
    }
}
