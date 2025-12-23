using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace DigitalHealthTracker.Desktop
{
    public partial class AssignProgramForm : Form
    {


		public AssignProgramForm()
		{
			InitializeComponent();

			ConfigureGrid();
			StyleGrid();

			// Trainer ekranında user approve işi yok (eğer buton varsa)
			// btnApprove.Visible = false;

			LoadUsers();
			LoadProgramsForLoggedInTrainer();
			LoadAssignmentsForLoggedInTrainer();
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

		// ✅ TrainerId artık Session'dan gelir
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

		private void LoadUsers()
		{
			using (var context = new AppDbContext())
			{
				var users = context.Users
					.OrderBy(u => u.Id)
					.Select(u => new
					{
						u.Id,
						FullName = u.Name + " " + u.Surname + " (" + u.Phone + ")"
					})
					.ToList();

				cmbUsers.DataSource = users;
				cmbUsers.DisplayMember = "FullName";
				cmbUsers.ValueMember = "Id";
			}
		}

		private void LoadProgramsForLoggedInTrainer()
		{
			var trainerId = GetLoggedInTrainerId();
			if (trainerId == null)
			{
				cmbPrograms.DataSource = null;
				MessageBox.Show("Trainer session not found. Please login as Trainer.");
				return;
			}

			using (var context = new AppDbContext())
			{
				var programs = context.WorkoutPrograms
					.Where(p => p.TrainerId == trainerId.Value)
					.OrderBy(p => p.Id)
					.Select(p => new
					{
						p.Id,
						p.Title
					})
					.ToList();

				cmbPrograms.DataSource = programs;
				cmbPrograms.DisplayMember = "Title";
				cmbPrograms.ValueMember = "Id";
			}
		}

		private void LoadAssignmentsForLoggedInTrainer()
		{
			var trainerId = GetLoggedInTrainerId();
			if (trainerId == null)
			{
				dgvAssignments.DataSource = null;
				return;
			}

			using (var context = new AppDbContext())
			{
				var list = context.AssignedPrograms
					.Include(a => a.User)
					.Include(a => a.WorkoutProgram)
					.Where(a => a.TrainerId == trainerId.Value)
					.OrderByDescending(a => a.AssignedAt)
					.Select(a => new
					{
						a.Id,
						User = a.User.Name + " " + a.User.Surname,
						Program = a.WorkoutProgram.Title,
						Status = a.Status.ToString(),
						AssignedAt = a.AssignedAt.ToString("yyyy-MM-dd HH:mm"),
						ApprovedAt = a.ApprovedAt.HasValue ? a.ApprovedAt.Value.ToString("yyyy-MM-dd HH:mm") : ""
					})
					.ToList();

				dgvAssignments.DataSource = null;
				dgvAssignments.DataSource = list;

				if (dgvAssignments.Columns["Id"] != null)
					dgvAssignments.Columns["Id"].Visible = false;
			}
		}


		// Buttons
		private void btnAssign_Click(object sender, EventArgs e)
		{
			var trainerId = GetLoggedInTrainerId();
			var userId = GetSelectedUserId();
			var programId = GetSelectedProgramId();

			if (trainerId == null)
			{
				MessageBox.Show("Please login as Trainer.");
				return;
			}

			if (userId == null)
			{
				MessageBox.Show("Please select a user.");
				return;
			}

			if (programId == null)
			{
				MessageBox.Show("Please select a program.");
				return;
			}

			try
			{
				using (var context = new AppDbContext())
				{
					// ✅ ekstra güvenlik: seçilen program gerçekten bu trainer'a ait mi?
					bool ownsProgram = context.WorkoutPrograms.Any(p => p.Id == programId.Value && p.TrainerId == trainerId.Value);
					if (!ownsProgram)
					{
						MessageBox.Show("You can only assign your own programs.");
						return;
					}

					// Aynı user'a aynı program tamamlanmadan atanmasın (opsiyonel)
					bool exists = context.AssignedPrograms.Any(a =>
						a.UserId == userId.Value &&
						a.WorkoutProgramId == programId.Value &&
						a.Status != AssignmentStatus.Completed);

					if (exists)
					{
						MessageBox.Show("This program is already assigned to the user (not completed yet).");
						return;
					}

					var assignment = new AssignedProgram
					{
						TrainerId = trainerId.Value,
						UserId = userId.Value,
						WorkoutProgramId = programId.Value,
						Status = AssignmentStatus.Pending,
						AssignedAt = DateTime.Now
					};

					context.AssignedPrograms.Add(assignment);
					context.SaveChanges();
				}

				MessageBox.Show("Program assigned successfully. (Status: Pending)");
				LoadAssignmentsForLoggedInTrainer();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Assign Program Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnRefresh_Click(object sender, EventArgs e)
        {
			LoadProgramsForLoggedInTrainer();
			LoadAssignmentsForLoggedInTrainer();
		}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

		private void btnDeleteAssignment_Click(object sender, EventArgs e)
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

			try
			{
				using (var context = new AppDbContext())
				{
					var trainerId = GetLoggedInTrainerId();
					if (trainerId == null)
					{
						MessageBox.Show("Please login as Trainer.");
						return;
					}

					var assignment = context.AssignedPrograms.SingleOrDefault(a => a.Id == assignmentId);
					if (assignment == null)
					{
						MessageBox.Show("Assignment not found.");
						return;
					}

					// ✅ Trainer sadece kendi assignment'ını silebilsin
					if (assignment.TrainerId != trainerId.Value)
					{
						MessageBox.Show("You can only delete your own assignments.");
						return;
					}

					context.AssignedPrograms.Remove(assignment);
					context.SaveChanges();
				}

				MessageBox.Show("Assignment deleted.");
				LoadAssignmentsForLoggedInTrainer();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Delete Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
    }
}
