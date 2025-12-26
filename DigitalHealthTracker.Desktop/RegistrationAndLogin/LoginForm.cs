using DigitalHealthTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DigitalHealthTracker.Desktop.Services;
using System.Threading.Tasks;



namespace DigitalHealthTracker.Desktop.RegistrationAndLogin
{
    public partial class LoginForm : Form
    {
        private readonly AuthApiService _authApi = new AuthApiService();

        public LoginForm()
        {
            InitializeComponent();
        }

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            cmbRole.Items.Clear();
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Trainer");
            cmbRole.Items.Add("User");
            cmbRole.SelectedIndex = 2; // default User
            await CheckApiAndUpdateUi();
        }

        // On form shown, check API status
        private async Task CheckApiAndUpdateUi()
        {
            bool ok = await _authApi.PingAsync();

            btnLogin.Enabled = ok;
            btnRegisterUser.Enabled = ok;
            btnRegisterTrainer.Enabled = ok;

            lblServerStatus.Text = ok ? "Server: Online" : "Server: Offline (start API)";
        }

        private async void btnLogin_Click(object sender, EventArgs e)
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
                Session.Clear();

                var resp = await _authApi.LoginAsync(new LoginRequestDto
                {
                    Role = roleText,
                    Phone = phone,
                    Password = password
                });

                if (resp.Role == "Admin")
                {
                    Session.Role = AppRole.Admin;
                    Session.AdminId = resp.Id;
                }
                else if (resp.Role == "Trainer")
                {
                    Session.Role = AppRole.Trainer;
                    Session.TrainerId = resp.Id;
                }
                else
                {
                    Session.Role = AppRole.User;
                    Session.UserId = resp.Id;
                }

                Session.DisplayName = resp.DisplayName;

                this.Hide();

                using (var main = new MainForm())
                    main.ShowDialog();

                Session.Clear();
                txtPassword.Text = "";
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Error",
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

        private async void lblServerStatus_Click(object sender, EventArgs e)
        {
			await CheckApiAndUpdateUi();
		}
    }
}
