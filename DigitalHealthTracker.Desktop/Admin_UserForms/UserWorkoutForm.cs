using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigitalHealthTracker.Desktop
{
	public partial class UserWorkoutForm : Form
	{
		private AssignedProgram? activeAssignmentCache;

		public UserWorkoutForm()
		{
			InitializeComponent();

			ConfigureGrid();
			StyleGrid();

			LoadActiveProgram();
			LoadWorkoutLogs();
		}

		private void ConfigureGrid()
		{
			dgvHistory.AutoGenerateColumns = true;
			dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvHistory.MultiSelect = false;
			dgvHistory.ReadOnly = true;
			dgvHistory.AllowUserToAddRows = false;
		}

		private void StyleGrid()
		{
			dgvHistory.EnableHeadersVisualStyles = false;
			dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 170, 170);
			dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
			dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

			dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 248);
			dgvHistory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 150, 150);
			dgvHistory.DefaultCellStyle.SelectionForeColor = Color.White;
		}

		private int? GetLoggedInUserId()
		{
			if (Session.Role != AppRole.User || Session.UserId == null)
				return null;

			return Session.UserId.Value;
		}

		private void LoadActiveProgram()
		{
			var userId = GetLoggedInUserId();
			if (userId == null)
			{
				lblActiveProgram.Text = "Active Program: - (Please login as User)";
				activeAssignmentCache = null;
				return;
			}

			using (var context = new AppDbContext())
			{
				var active = context.AssignedPrograms
					.Include(a => a.WorkoutProgram)
					.Include(a => a.Trainer)
					.Where(a => a.UserId == userId.Value && a.Status == AssignmentStatus.Active)
					.OrderByDescending(a => a.ApprovedAt)
					.FirstOrDefault();

				activeAssignmentCache = active;

				if (active == null)
				{
					lblActiveProgram.Text = "Active Program: - (No active assignment)";
					return;
				}

				lblActiveProgram.Text =
					$"Active Program: {active.WorkoutProgram.Title} (Trainer: {active.Trainer.Name} {active.Trainer.Surname})";
			}
		}

		private void LoadWorkoutLogs()
		{
			var userId = GetLoggedInUserId();
			if (userId == null)
			{
				dgvHistory.DataSource = null;
				return;
			}

			using (var context = new AppDbContext())
			{
				var logs = context.WorkoutLogs
					.Include(l => l.WorkoutProgram)
					.Include(l => l.Workout)
					.Where(l => l.UserId == userId.Value)
					.OrderByDescending(l => l.CompletedAt)
					.Select(l => new
					{
						l.Id,
						Program = l.WorkoutProgram.Title,
						Workout = l.Workout.Name,          // ✅ workout adı
						l.Sets,
						l.Reps,
						CompletedAt = l.CompletedAt.ToString("yyyy-MM-dd HH:mm")
					})
					.ToList();

				dgvHistory.DataSource = null;
				dgvHistory.DataSource = logs;

				if (dgvHistory.Columns["Id"] != null)
					dgvHistory.Columns["Id"].Visible = false;
			}
		}


		private void btnCompleteDay_Click(object sender, EventArgs e)
		{
			var userId = GetLoggedInUserId();
			if (userId == null)
			{
				MessageBox.Show("Please login as User.");
				return;
			}

			if (activeAssignmentCache == null)
			{
				MessageBox.Show("No active program found.");
				return;
			}

			int dayNo = 1;   // tek gün mantığı
			int sets = 1;    // UI yoksa default
			int reps = 1;    // UI yoksa default

			try
			{
				using (var context = new AppDbContext())
				{
					// ✅ Assignment'ı program ve items ile birlikte çek
					var assignment = context.AssignedPrograms
						.Include(a => a.WorkoutProgram)
							.ThenInclude(p => p.Items)
						.SingleOrDefault(a => a.Id == activeAssignmentCache.Id);

					if (assignment == null)
					{
						MessageBox.Show("Active assignment not found in DB.");
						return;
					}

					if (assignment.Status != AssignmentStatus.Active)
					{
						MessageBox.Show("Only ACTIVE assignments can be completed.");
						return;
					}

					// ✅ DayNo=1 item'ını bul -> WorkoutId buradan gelecek
					var item = assignment.WorkoutProgram.Items
						.FirstOrDefault(i => i.DayNo == dayNo);

					if (item == null)
					{
						MessageBox.Show($"Program item not found for Day {dayNo}.");
						return;
					}

					// ✅ Workout gerçekten var mı?
					bool workoutOk = context.Workouts.Any(w => w.Id == item.WorkoutId);
					if (!workoutOk)
					{
						MessageBox.Show("Workout not found for selected day item.");
						return;
					}

					// ✅ Aynı program tekrar atanmışsa tekrar log atılabilsin:
					// Program bazlı engel koymuyoruz.
					// Sadece çok kısa süre içinde çift tıklama engeli:
					var lastLog = context.WorkoutLogs
						.Where(l => l.UserId == userId.Value &&
									l.WorkoutProgramId == assignment.WorkoutProgramId &&
									l.WorkoutId == item.WorkoutId &&
									l.DayNo == dayNo)
						.OrderByDescending(l => l.CompletedAt)
						.FirstOrDefault();

					if (lastLog != null && (DateTime.Now - lastLog.CompletedAt).TotalSeconds < 10)
					{
						MessageBox.Show("Already completed very recently.");
						return;
					}

					// ✅ Log oluştur (WorkoutId SET EDİLDİ -> FK hatası biter)
					var log = new WorkoutLog
					{
						UserId = userId.Value,
						TrainerId = assignment.TrainerId,
						WorkoutProgramId = assignment.WorkoutProgramId,
						WorkoutId = item.WorkoutId,
						DayNo = dayNo,
						Sets = sets,
						Reps = reps,
						CompletedAt = DateTime.Now
					};

					context.WorkoutLogs.Add(log);

					// Tek gün mantığı: assignment bitti
					assignment.Status = AssignmentStatus.Completed;

					context.SaveChanges();
				}

				MessageBox.Show("Completed! Workout log saved.");

				LoadActiveProgram();
				LoadWorkoutLogs();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Complete Day Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void btnRefreshHistory_Click(object sender, EventArgs e)
		{
			LoadActiveProgram();
			LoadWorkoutLogs();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
