using DigitalHealthTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DigitalHealthTracker.Desktop.Services;

using DigitalHealthTracker.Contracts.Auth;



namespace DigitalHealthTracker.Desktop.RegistrationAndLogin
{
    public partial class TrainerRegisterForm : Form
    {
		private readonly AuthApiService _authApi = new AuthApiService();

		public TrainerRegisterForm()
        {
            InitializeComponent();
        }

		private async void btnRegister_Click(object sender, EventArgs e)
		{
			string phone = txtPhone.Text.Trim();

			if (string.IsNullOrWhiteSpace(txtName.Text) ||
				string.IsNullOrWhiteSpace(txtSurname.Text) ||
				string.IsNullOrWhiteSpace(phone) ||
				string.IsNullOrWhiteSpace(txtPassword.Text))
			{
				MessageBox.Show("All fields are required.");
				return;
			}

			if (phone.Length != 10)
			{
				MessageBox.Show("Please enter a valid phone number.");
				return;
			}

			if (txtPassword.Text != txtConfirmPassword.Text)
			{
				MessageBox.Show("Passwords do not match.");
				return;
			}

			try
			{
				await _authApi.RegisterTrainerAsync(new RegisterTrainerRequestDto
				{
					Name = txtName.Text.Trim(),
					Surname = txtSurname.Text.Trim(),
					Phone = phone,
					Email = txtEmail.Text.Trim(),
					Password = txtPassword.Text
				});

				MessageBox.Show("Registration successful. Await admin approval.");
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Register Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void btnCancel_Click(object sender, EventArgs e)
        {
			this.Close();

		}
	}
}
