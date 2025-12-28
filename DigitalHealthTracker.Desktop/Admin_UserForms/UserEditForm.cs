using DigitalHealthTracker.Data.Entities;
using System;
using System.Windows.Forms;

namespace DigitalHealthTracker.Desktop
{
	public partial class UserEditForm : Form
	{
		private int? userIdEdit;
		private User _user = new User();

		public User EditedUser => _user;

		// Add
		public UserEditForm()
		{
			InitializeComponent();
		}

		// Edit
		public UserEditForm(User tempUser) : this()
		{
			userIdEdit = tempUser.Id;

			txtName.Text = tempUser.Name;
			txtSurname.Text = tempUser.Surname;
			txtEmail.Text = tempUser.Email;
			txtPhone.Text = tempUser.Phone;
			txtMedicalRecord.Text = tempUser.MedicalRecord;
			txtBirthYear.Text = tempUser.BirthYear > 0 ? tempUser.BirthYear.ToString() : "";
			txtHeightCm.Text = tempUser.HeightCm > 0 ? tempUser.HeightCm.ToString() : "";
			txtWeightKg.Text = tempUser.WeightKg > 0 ? tempUser.WeightKg.ToString() : "";
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtSurname.Text))
			{
				MessageBox.Show("Name and Surname is required.");
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

			if (string.IsNullOrWhiteSpace(txtHeightCm.Text))
			{
				MessageBox.Show("Height is required.");
				return;
			}
			if (!double.TryParse(txtHeightCm.Text, out double heightCm) || heightCm <= 0)
			{
				MessageBox.Show("Please enter a valid height in cm (> 0).");
				return;
			}

			if (string.IsNullOrWhiteSpace(txtWeightKg.Text))
			{
				MessageBox.Show("Weight is required.");
				return;
			}
			if (!double.TryParse(txtWeightKg.Text, out double weightKg) || weightKg <= 0)
			{
				MessageBox.Show("Please enter a valid weight in kg (> 0).");
				return;
			}

			_user = new User
			{
				Id = userIdEdit ?? 0,
				Name = txtName.Text.Trim(),
				Surname = txtSurname.Text.Trim(),
				Email = txtEmail.Text.Trim(),
				Phone = txtPhone.Text.Trim(),
				MedicalRecord = txtMedicalRecord.Text ?? "",
				BirthYear = birthYear,
				HeightCm = heightCm,
				WeightKg = weightKg
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
