using DigitalHealthTracker.Desktop.Admin_TrainerForms;
using DigitalHealthTracker.Desktop.RegistrationAndLogin;
using DigitalHealthTracker.Desktop.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalHealthTracker.Desktop
{
    public partial class MainForm : Form
    {
        private readonly UserApiService _userApi = new UserApiService();
        private readonly TrainerApiService _trainerApi = new TrainerApiService();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            var frm = new UserListForm();
            frm.UserChanged += message => MessageBox.Show(message, "User Event Info");
            frm.ShowDialog();
        }

        private void btnManageTrainers_Click(object sender, EventArgs e)
        {
            var frm = new TrainerListForm();
            frm.TrainerChanged += message => MessageBox.Show(message, "Trainer Event Info");
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

        private void btnApproveAssignment_Click(object sender, EventArgs e)
        {
            var frm = new UserAssignmentsForm();
            frm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Admin
            btnManageUsers.Visible = Session.Role == AppRole.Admin;
            btnManageTrainers.Visible = Session.Role == AppRole.Admin;
            btnApproveTrainers.Visible = Session.Role == AppRole.Admin;

            // Trainer
            btnManageWorkouts.Visible = Session.Role == AppRole.Trainer;
            btnManagePrograms.Visible = Session.Role == AppRole.Trainer;
            btnAssignProgram.Visible = Session.Role == AppRole.Trainer;
            btnStudentLogs.Visible = Session.Role == AppRole.Trainer;


            // User
            btnUserWorkout.Visible = Session.Role == AppRole.User;
            btnApproveAssignment.Visible = Session.Role == AppRole.User;
			lblBmiValueTxt.Visible = Session.Role == AppRole.User;
			lblTargetWeightValueTxt.Visible = Session.Role == AppRole.User;
			lblWeightDiffValueTxt.Visible = Session.Role == AppRole.User;
			lblBmiCategoryValueTxt.Visible = Session.Role == AppRole.User;

            // ✅ My Profile (User + Trainer görsün, Admin görmesin)
            btnEditMyProfile.Visible = (Session.Role == AppRole.User || Session.Role == AppRole.Trainer);

            // ✅ BMI panelini sadece User görsün
            bool showUserMetrics = Session.Role == AppRole.User;
            lblBmiValue.Visible = showUserMetrics;
            lblTargetWeightValue.Visible = showUserMetrics;
            lblWeightDiffValue.Visible = showUserMetrics;
            lblBmiCategoryValue.Visible = showUserMetrics;

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
                btnApproveAssignment.Visible = false;

                btnEditMyProfile.Visible = false;

                lblBmiValue.Visible = false;
                lblTargetWeightValue.Visible = false;
                lblWeightDiffValue.Visible = false;
                lblBmiCategoryValue.Visible = false;
            }

            // ✅ User ise ilk açılışta da güncelle
            _ = RefreshUserMetricsAsync();
        }

        private async Task RefreshUserMetricsAsync()
        {
            if (Session.Role != AppRole.User) return;
            if (Session.UserId == null) return;

            var user = await _userApi.GetByIdAsync(Session.UserId.Value);
            if (user == null) return;

            lblBmiValue.Text = user.Bmi.ToString("0.00");
            lblTargetWeightValue.Text = user.TargetWeightKg.ToString("0.0");
            lblWeightDiffValue.Text = user.WeightDiffKg.ToString("0.0");
            lblBmiCategoryValue.Text = user.BmiCategory;
        }

        private async void btnEditMyProfile_Click(object sender, EventArgs e)
        {
            try
            {
                // ================= USER =================
                if (Session.Role == AppRole.User)
                {
                    if (Session.UserId == null)
                    {
                        MessageBox.Show("Session error: UserId is null.");
                        return;
                    }

                    var user = await _userApi.GetByIdAsync(Session.UserId.Value);
                    if (user == null)
                    {
                        MessageBox.Show("User not found.");
                        return;
                    }

                    using var frm = new UserEditForm(user);
                    if (frm.ShowDialog() != DialogResult.OK)
                        return;

                    await _userApi.UpdateAsync(user.Id, frm.EditedUser);

                    // ✅ canlı güncelle
                    await RefreshUserMetricsAsync();

                    MessageBox.Show("Profile updated.");
                }

                // ================= TRAINER =================
                else if (Session.Role == AppRole.Trainer)
                {
                    if (Session.TrainerId == null)
                    {
                        MessageBox.Show("Session error: TrainerId is null.");
                        return;
                    }

                    var trainer = await _trainerApi.GetByIdAsync(Session.TrainerId.Value);
                    if (trainer == null)
                    {
                        MessageBox.Show("Trainer not found.");
                        return;
                    }

                    using var frm = new TrainerEditForm(trainer, hideApproval: true);
                    if (frm.ShowDialog() != DialogResult.OK)
                        return;

                    // 🔒 Trainer admin onayı değiştiremez
                    frm.EditedTrainer.IsApproved = trainer.IsApproved;

                    await _trainerApi.UpdateAsync(trainer.Id, frm.EditedTrainer);
                    MessageBox.Show("Profile updated.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Profile Edit Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnStudentLogs_Click(object sender, EventArgs e)
        {
            using var frm = new TrainerUserLogsForm();
            frm.ShowDialog();
        }

        private void lblTargetWeightValue_Click(object sender, EventArgs e)
        {

        }
    }
}
