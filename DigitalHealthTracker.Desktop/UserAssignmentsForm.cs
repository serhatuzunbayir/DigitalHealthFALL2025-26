using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;


namespace DigitalHealthTracker.Desktop
{
    public partial class UserAssignmentsForm : Form
    {
        public UserAssignmentsForm()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadPendingAssignments();
        }

        private void ConfigureGrid()
        {
            dgvPending.AutoGenerateColumns = true;
            dgvPending.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPending.MultiSelect = false;
            dgvPending.ReadOnly = true;
            dgvPending.AllowUserToAddRows = false;
        }

        private void LoadPendingAssignments()
        {
            if (Session.Role != AppRole.User || Session.UserId == null)
            {
                MessageBox.Show("Please login as User.");
                dgvPending.DataSource = null;
                return;
            }

            using (var context = new AppDbContext())
            {
                int userId = Session.UserId.Value;

                var list = context.AssignedPrograms
                    .Include(a => a.Trainer)
                    .Include(a => a.WorkoutProgram)
                    .Where(a => a.UserId == userId && a.Status == AssignmentStatus.Pending)
                    .OrderByDescending(a => a.AssignedAt)
                    .Select(a => new
                    {
                        a.Id,
                        Program = a.WorkoutProgram.Title,
                        Trainer = a.Trainer.Name + " " + a.Trainer.Surname,
                        AssignedAt = a.AssignedAt.ToString("yyyy-MM-dd HH:mm")
                    })
                    .ToList();

                dgvPending.DataSource = null;
                dgvPending.DataSource = list;

                if (dgvPending.Columns["Id"] != null)
                    dgvPending.Columns["Id"].Visible = false;
            }
        }

        private void btnApproveSelected_Click(object sender, EventArgs e)
        {
            if (dgvPending.CurrentRow == null)
            {
                MessageBox.Show("Please select an assignment.");
                return;
            }

            int assignmentId = Convert.ToInt32(dgvPending.CurrentRow.Cells["Id"].Value);

            try
            {
                using (var context = new AppDbContext())
                {
                    int userId = Session.UserId!.Value;

                    var assignment = context.AssignedPrograms
                        .SingleOrDefault(a => a.Id == assignmentId);

                    if (assignment == null)
                    {
                        MessageBox.Show("Assignment not found.");
                        return;
                    }

                    // ✅ güvenlik: user sadece kendi assignment'ını approve edebilsin
                    if (assignment.UserId != userId)
                    {
                        MessageBox.Show("You can only approve your own assignments.");
                        return;
                    }

                    if (assignment.Status != AssignmentStatus.Pending)
                    {
                        MessageBox.Show("Only Pending assignments can be approved.");
                        return;
                    }

                    assignment.Status = AssignmentStatus.Active;
                    assignment.ApprovedAt = DateTime.Now;

                    context.SaveChanges();
                }

                MessageBox.Show("Approved! Status is now ACTIVE.");
                LoadPendingAssignments();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Approve Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPendingAssignments();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
			Close();
		}
	}
}
