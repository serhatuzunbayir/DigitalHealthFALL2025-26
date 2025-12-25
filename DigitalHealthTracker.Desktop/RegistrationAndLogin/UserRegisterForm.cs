using DigitalHealthTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DigitalHealthTracker.Data.Entities;
using DigitalHealthTracker.Data.Infrastructure;
using System.Linq;



namespace DigitalHealthTracker.Desktop.RegistrationAndLogin
{
    public partial class UserRegisterForm : Form
    {
        public UserRegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
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

            using (var context = new AppDbContext())
            {
                bool phoneExists =
                    context.Users.Any(u => u.Phone == phone) ||
                    context.Trainers.Any(t => t.Phone == phone) ||
                    context.Admins.Any(a => a.Phone == phone);

                if (phoneExists)
                {
                    MessageBox.Show("This phone number is already registered.");
                    return;
                }

                var user = new User
                {
                    Name = txtName.Text.Trim(),
                    Surname = txtSurname.Text.Trim(),
                    Phone = phone,
                    Email = txtEmail.Text.Trim(),
                    PasswordHash = PasswordHasher.HashPassword(txtPassword.Text)
                };

                context.Users.Add(user);
                context.SaveChanges();
            }

            MessageBox.Show("Registration successful. You can now log in.");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
			this.Close();
		}
    }
}
