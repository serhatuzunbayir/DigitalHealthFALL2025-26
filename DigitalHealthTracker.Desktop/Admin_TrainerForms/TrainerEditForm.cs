using System;
using System.Windows.Forms;
using DigitalHealthTracker.Data.Entities;

namespace DigitalHealthTracker.Desktop
{
	public partial class TrainerEditForm : Form
	{
		private int? trainerIdEdit;
		private Trainer _trainer = new Trainer();

		public Trainer EditedTrainer => _trainer;

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
			if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtSurname.Text))
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

			_trainer = new Trainer
			{
				Id = trainerIdEdit ?? 0,
				Name = txtName.Text.Trim(),
				Surname = txtSurname.Text.Trim(),
				Phone = txtPhone.Text.Trim(),
				Email = txtEmail.Text.Trim(),
				BirthYear = birthYear,
				IsApproved = chkIsApproved.Checked
			};

			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
