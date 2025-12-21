using DigitalHealthTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DigitalHealthTracker.Desktop
{
    public partial class TrainerApprovalForm : Form
    {
        public TrainerApprovalForm()
        {
            InitializeComponent();
        }

        private void TrainerApprovalForm_Load(object sender, EventArgs e)
        {
            LoadPendingTrainers();
        }

        private void LoadPendingTrainers()
        {
            using (var db = new AppDbContext())
            {
                var pendingTrainers = db.Trainers
                    .Where(t => t.IsApproved == false)
                    .Select(t => new
                    {
                        t.Id,
                        t.Name,
                        t.Surname,
                        t.Email
                    })
                    .ToList();

                dgvTrainers.DataSource = pendingTrainers;
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
			if (dgvTrainers.CurrentRow == null)
			{
				MessageBox.Show("Please select a trainer.");
				return;
			}

			int trainerId = (int)dgvTrainers.CurrentRow.Cells["Id"].Value;

			using (var db = new AppDbContext())
			{
				var trainer = db.Trainers.Find(trainerId);

				if (trainer == null)
				{
					MessageBox.Show("Trainer not found.");
					return;
				}

				trainer.IsApproved = true;
				db.SaveChanges();
			}

			MessageBox.Show("Trainer approved successfully.");
			LoadPendingTrainers();
		}
    }
}
