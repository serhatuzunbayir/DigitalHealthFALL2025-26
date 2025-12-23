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

			// Form açılır açılmaz kendi programlarını yükle
			LoadProgramsForLoggedInTrainer();
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

		private int GetLoggedInTrainerId()
		{
			if (Session.Role != AppRole.Trainer || Session.TrainerId == null)
				throw new InvalidOperationException("Please login as Trainer.");

			return Session.TrainerId.Value;
		}

		private WorkoutProgram? GetSelectedProgram()
		{
			if (dgvPrograms.CurrentRow == null) return null;
			return dgvPrograms.CurrentRow.DataBoundItem as WorkoutProgram;
		}

		private void LoadProgramsForLoggedInTrainer()
		{
			int trainerId;
			try
			{
				trainerId = GetLoggedInTrainerId();
			}
			catch
			{
				MessageBox.Show("Please login as Trainer.");
				dgvPrograms.DataSource = null;
				return;
			}

			using (var context = new AppDbContext())
			{
				var programs = context.WorkoutPrograms
					.Include(p => p.Trainer)
					.Where(p => p.TrainerId == trainerId)
					.OrderBy(p => p.Id)
					.ToList();

				dgvPrograms.DataSource = null;
				dgvPrograms.DataSource = programs;
			}

			if (dgvPrograms.Columns["Trainer"] != null) dgvPrograms.Columns["Trainer"].Visible = false;
			if (dgvPrograms.Columns["Items"] != null) dgvPrograms.Columns["Items"].Visible = false;
		}

		// Eğer butonun varsa refresh için kullan
		private void btnLoadPrograms_Click(object sender, EventArgs e)
		{
			LoadProgramsForLoggedInTrainer();
		}

		private void btnAddProgram_Click(object sender, EventArgs e)
		{
			int trainerId;
			try
			{
				trainerId = GetLoggedInTrainerId();
			}
			catch
			{
				MessageBox.Show("Please login as Trainer.");
				return;
			}

			try
			{
				using (var frm = new WorkoutProgramEditForm(trainerId))
				{
					var result = frm.ShowDialog();
					if (result == DialogResult.OK)
						LoadProgramsForLoggedInTrainer();
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
			int trainerId;
			try
			{
				trainerId = GetLoggedInTrainerId();
			}
			catch
			{
				MessageBox.Show("Please login as Trainer.");
				return;
			}

			var selectedProgram = GetSelectedProgram();
			if (selectedProgram == null)
			{
				MessageBox.Show("Please select a program to edit...");
				return;
			}

			// ✅ ekstra güvenlik: başka trainer programı edit edilemesin
			if (selectedProgram.TrainerId != trainerId)
			{
				MessageBox.Show("You can only edit your own programs.");
				return;
			}

			try
			{
				using (var frm = new WorkoutProgramEditForm(trainerId, selectedProgram))
				{
					var result = frm.ShowDialog();
					if (result == DialogResult.OK)
						LoadProgramsForLoggedInTrainer();
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
			int trainerId;
			try
			{
				trainerId = GetLoggedInTrainerId();
			}
			catch
			{
				MessageBox.Show("Please login as Trainer.");
				return;
			}

			var selectedProgram = GetSelectedProgram();
			if (selectedProgram == null)
			{
				MessageBox.Show("Please select a program to delete...");
				return;
			}

			// ✅ ekstra güvenlik: başka trainer programı silinmesin
			if (selectedProgram.TrainerId != trainerId)
			{
				MessageBox.Show("You can only delete your own programs.");
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

					// 1) Bu programa bağlı assignment'ları sil
					var assignments = context.AssignedPrograms
						.Where(a => a.WorkoutProgramId == program.Id)
						.ToList();
					if (assignments.Count > 0)
						context.AssignedPrograms.RemoveRange(assignments);

					// 2) Bu programa bağlı workout log'larını sil
					var logs = context.WorkoutLogs
						.Where(l => l.WorkoutProgramId == program.Id)
						.ToList();
					if (logs.Count > 0)
						context.WorkoutLogs.RemoveRange(logs);

					// 3) Program items (zaten include ettik)
					if (program.Items != null && program.Items.Count > 0)
						context.WorkoutProgramItems.RemoveRange(program.Items);

					// 4) Programı sil
					context.WorkoutPrograms.Remove(program);

					context.SaveChanges();
				}


				LoadProgramsForLoggedInTrainer();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Delete Program Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
