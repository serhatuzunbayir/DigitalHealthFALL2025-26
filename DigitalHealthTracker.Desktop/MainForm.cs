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
    }
}
