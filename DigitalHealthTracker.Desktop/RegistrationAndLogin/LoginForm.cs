using DigitalHealthTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DigitalHealthTracker.Data.Infrastructure;


namespace DigitalHealthTracker.Desktop.RegistrationAndLogin
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            cmbRole.Items.Clear();
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Trainer");
            cmbRole.Items.Add("User");
            cmbRole.SelectedIndex = 2; // default User
        }

		private void btnLogin_Click(object sender, EventArgs e)
		{
			string phone = txtPhone.Text.Trim();
			string password = txtPassword.Text;

			if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password))
			{
				MessageBox.Show("Phone and Password are required.");
				return;
			}

			if (cmbRole.SelectedItem == null)
			{
				MessageBox.Show("Please select a role.");
				return;
			}

			string roleText = cmbRole.SelectedItem.ToString()!;

			try
			{
				using (var context = new AppDbContext())
				{
					Session.Clear();

					if (roleText == "Admin")
					{
						var admin = context.Admins.SingleOrDefault(a => a.Phone == phone);
						if (admin == null || !PasswordHasher.VerifyPassword(password, admin.PasswordHash))
						{
							MessageBox.Show("Invalid admin credentials.");
							return;
						}

						Session.Role = AppRole.Admin;
						Session.AdminId = admin.Id;
						Session.DisplayName = admin.Name;
					}
					else if (roleText == "Trainer")
					{
						var trainer = context.Trainers.SingleOrDefault(t => t.Phone == phone);
						if (trainer == null || !PasswordHasher.VerifyPassword(password, trainer.PasswordHash))
						{
							MessageBox.Show("Invalid trainer credentials.");
							return;
						}

						if (!trainer.IsApproved)
						{
							MessageBox.Show("Your trainer account is not approved yet.");
							return;
						}

						Session.Role = AppRole.Trainer;
						Session.TrainerId = trainer.Id;
						Session.DisplayName = $"{trainer.Name} {trainer.Surname}";
					}
					else // User
					{
						var user = context.Users.SingleOrDefault(u => u.Phone == phone);
						if (user == null || !PasswordHasher.VerifyPassword(password, user.PasswordHash))
						{
							MessageBox.Show("Invalid user credentials.");
							return;
						}

						Session.Role = AppRole.User;
						Session.UserId = user.Id;
						Session.DisplayName = $"{user.Name} {user.Surname}";
					}
				}

				// ✅ Login başarılı -> MainForm'a geç, sonra tekrar login ekranına dön
				this.Hide();

				using (var main = new MainForm())
				{
					main.ShowDialog(); // Logout -> main kapanınca buraya döner
				}

				// main kapandıysa (logout), session temizle ve login ekranını geri aç
				Session.Clear();
				txtPassword.Text = "";
				this.Show();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Login Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void btnRegisterUser_Click(object sender, EventArgs e)
        {
            using (var frm = new UserRegisterForm())
                frm.ShowDialog();
        }

        private void btnRegisterTrainer_Click(object sender, EventArgs e)
        {
            using (var frm = new TrainerRegisterForm())
                frm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
    }
}
