using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DigitalHealthTracker.Desktop
{
    public partial class UserEditForm : Form
    {
		private int? userIdEdit;

		// Add için constructor
		public UserEditForm()
        {
            InitializeComponent();
        }

        // Edit için constructor
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


			// Doğum yılı / boy / kilo kontrollü aldık
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


			// user'ı formdan alıp initialize ettik
			using (var context = new AppDbContext())
			{
				User user;

				if (userIdEdit == null)
				{
					// ADD way
					user = new User();
					context.Users.Add(user);
				}
				else
				{
					// EDIT way
					user = context.Users.Find(userIdEdit.Value);

					if (user == null)
					{
						MessageBox.Show("User not found in DataBase.");
						return;
					}

					
				}

				user.Name = txtName.Text;
				user.Surname = txtSurname.Text;
				user.Email = txtEmail.Text;
				user.Phone = txtPhone.Text;
				user.MedicalRecord = txtMedicalRecord.Text ?? "";
				user.BirthYear = birthYear;
				user.HeightCm = heightCm;
				user.WeightKg = weightKg;

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
