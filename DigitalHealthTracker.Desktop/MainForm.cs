using DigitalHealthTracker.Desktop.RegistrationAndLogin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DigitalHealthTracker.Desktop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }



        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            var frm = new UserListForm();
            frm.UserChanged += message => MessageBox.Show(message, "User Event Info"); ; // eventi ekleme ve Lambda
            frm.ShowDialog();
        }

        private void btnManageTrainers_Click(object sender, EventArgs e)
        {
            var frm = new TrainerListForm();
            frm.TrainerChanged += message => MessageBox.Show(message, "Trainer Event Info"); // eventi ekleme ve Lambda
            frm.ShowDialog();
        }



        private void btnApproveTrainers_Click(object sender, EventArgs e)
        {
            var frm = new TrainerApprovalForm();
            frm.ShowDialog();
        }

        private void btnManageWorkouts_Click(object sender, EventArgs e)
        {
            var frm = new WorkoutListForm();
            frm.ShowDialog();
        }

        private void btnManagePrograms_Click(object sender, EventArgs e)
        {
            var frm = new WorkoutProgramListForm();
            frm.ShowDialog();
        }

        private void btnAssignProgram_Click(object sender, EventArgs e)
        {
            var frm = new AssignProgramForm();
            frm.ShowDialog();

        }

        private void btnUserWorkout_Click(object sender, EventArgs e)
        {
            var frm = new UserWorkoutForm();
            frm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Admin
            btnManageUsers.Visible = Session.Role == AppRole.Admin;
            btnManageTrainers.Visible = Session.Role == AppRole.Admin;
            btnApproveTrainers.Visible = Session.Role == AppRole.Admin;
            //btnManageWorkouts.Visible = Session.Role == AppRole.Admin;
            //btnManagePrograms.Visible = Session.Role == AppRole.Admin;
            //btnAssignProgram.Visible = Session.Role == AppRole.Admin;
            //btnUserWorkout.Visible = Session.Role == AppRole.Admin;


            // Trainer
            btnManageWorkouts.Visible = Session.Role == AppRole.Trainer;
            btnManagePrograms.Visible = Session.Role == AppRole.Trainer;
            btnAssignProgram.Visible = Session.Role == AppRole.Trainer;
            //btnUserWorkout.Visible = Session.Role == AppRole.Trainer;



            // User
            btnUserWorkout.Visible = Session.Role == AppRole.User;
            btnApproveAssignment.Visible = Session.Role == AppRole.User;


            lblTitle.Text = $"Welcome, {Session.DisplayName}";
            lblSubTitle.Text = $"{Session.Role} Dashboard";

            if (Session.Role == AppRole.None)
            {
                btnManageUsers.Visible = false;
                btnManageTrainers.Visible = false;
                btnApproveTrainers.Visible = false;

                btnManageWorkouts.Visible = false;
                btnManagePrograms.Visible = false;
                btnAssignProgram.Visible = false;

                btnUserWorkout.Visible = false;
            }
        }

        private void btnApproveAssignment_Click(object sender, EventArgs e)
        {
            var frm = new UserAssignmentsForm();
            frm.ShowDialog();
        }

		private void btnLogout_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show(
				"Are you sure you want to logout?",
				"Logout",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (result != DialogResult.Yes)
				return;

			Session.Clear();

			this.Close();
		}

	}
}
