using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;

namespace DigitalHealthTracker.Desktop
{
    public partial class TrainerEditForm : Form
    {
        private int? trainerIdEdit;
        public TrainerEditForm()
        {
            InitializeComponent();
        }

		public TrainerEditForm(Trainer tempTrainer) : this()
		{
			trainerIdEdit = tempTrainer.Id;

			txtName.Text = tempTrainer.Name;
			txtSurname.Text = tempTrainer.Surname;
			txtPhone.Text = tempTrainer.Phone;
			txtEmail.Text = tempTrainer.Email;
			txtBirthYear.Text = tempTrainer.BirthYear > 0 ? tempTrainer.BirthYear.ToString() : "";
			chkIsApproved.Checked = tempTrainer.IsApproved;
		}

		private void btnSave_Click(object sender, EventArgs e)
        {
			if (string.IsNullOrWhiteSpace(txtName.Text) ||
				string.IsNullOrWhiteSpace(txtSurname.Text))
			{
				MessageBox.Show("Name and Surname are required.");
				return;
			}

			if (string.IsNullOrWhiteSpace(txtPhone.Text))
			{
				MessageBox.Show("Phone number is required.");
				return;
			}

			if (string.IsNullOrWhiteSpace(txtBirthYear.Text))
			{
				MessageBox.Show("Birth Year is required.");
				return;
			}

			if (!int.TryParse(txtBirthYear.Text, out int birthYear) ||
				birthYear < 1900 || birthYear > DateTime.Now.Year)
			{
				MessageBox.Show("Please enter a valid Birth Year (e.g., 1998).");
				return;
			}

			using (var context = new AppDbContext())
			{
				Trainer trainer;

				if (trainerIdEdit == null)
				{
					// ADD
					trainer = new Trainer();
					context.Trainers.Add(trainer);
				}
				else
				{
					// EDIT
					trainer = context.Trainers.Find(trainerIdEdit.Value);

					if (trainer == null)
					{
						MessageBox.Show("Trainer not found in DataBase.");
						return;
					}
				}

				// Ortak alanlar
				trainer.Name = txtName.Text;
				trainer.Surname = txtSurname.Text;
				trainer.Phone = txtPhone.Text;
				trainer.Email = txtEmail.Text;
				trainer.BirthYear = birthYear;
				trainer.IsApproved = chkIsApproved.Checked;

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
