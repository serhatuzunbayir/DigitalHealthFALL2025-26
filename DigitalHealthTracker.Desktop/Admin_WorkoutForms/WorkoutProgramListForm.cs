using DigitalHealthTracker.Data.Entities;
using DigitalHealthTracker.Desktop.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalHealthTracker.Desktop
{
    public partial class WorkoutProgramListForm : Form
    {
        private readonly WorkoutProgramApiService _programApi = new WorkoutProgramApiService();

        public WorkoutProgramListForm()
        {
            InitializeComponent();
            ConfigureGrid();
            StyleGrid();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadProgramsForLoggedInTrainer();
        }

        private void ConfigureGrid()
        {
            dgvPrograms.AutoGenerateColumns = true;
            dgvPrograms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPrograms.MultiSelect = false;
            dgvPrograms.ReadOnly = true;
            dgvPrograms.AllowUserToAddRows = false;
        }

        private void StyleGrid()
        {
            dgvPrograms.EnableHeadersVisualStyles = false;
            dgvPrograms.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 170, 170);
            dgvPrograms.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPrograms.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvPrograms.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 248);
            dgvPrograms.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 150, 150);
            dgvPrograms.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private int GetLoggedInTrainerId()
        {
            if (Session.Role != AppRole.Trainer || Session.TrainerId == null)
                throw new InvalidOperationException("Please login as Trainer.");

            return Session.TrainerId.Value;
        }

        private WorkoutProgramListDto? GetSelectedProgram()
        {
            return dgvPrograms.CurrentRow?.DataBoundItem as WorkoutProgramListDto;
        }

        private async Task LoadProgramsForLoggedInTrainer()
        {
            int trainerId;
            try { trainerId = GetLoggedInTrainerId(); }
            catch
            {
                MessageBox.Show("Please login as Trainer.");
                dgvPrograms.DataSource = null;
                return;
            }

            var programs = await _programApi.GetByTrainerAsync(trainerId);

            dgvPrograms.DataSource = null;
            dgvPrograms.DataSource = programs;

            // sadece grid temizliği
            if (dgvPrograms.Columns["TrainerId"] != null) dgvPrograms.Columns["TrainerId"].Visible = false;
        }

        private async void btnLoadPrograms_Click(object sender, EventArgs e)
        {
            await LoadProgramsForLoggedInTrainer();
        }

        private void btnAddProgram_Click(object sender, EventArgs e)
        {
            int trainerId;
            try { trainerId = GetLoggedInTrainerId(); }
            catch { MessageBox.Show("Please login as Trainer."); return; }

            using (var frm = new WorkoutProgramEditForm(trainerId))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = LoadProgramsForLoggedInTrainer();
            }
        }

        private void btnEditProgram_Click(object sender, EventArgs e)
        {
            int trainerId;
            try { trainerId = GetLoggedInTrainerId(); }
            catch { MessageBox.Show("Please login as Trainer."); return; }

            var selected = GetSelectedProgram();
            if (selected == null) { MessageBox.Show("Please select a program to edit..."); return; }

            if (selected.TrainerId != trainerId)
            {
                MessageBox.Show("You can only edit your own programs.");
                return;
            }

            // WorkoutProgramEditForm edit ctor senin eski kodun WorkoutProgram isterdi.
            // Minimal değişiklik: fake entity veriyoruz sadece Id için.
            var temp = new WorkoutProgram { Id = selected.Id };

            using (var frm = new WorkoutProgramEditForm(trainerId, temp))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = LoadProgramsForLoggedInTrainer();
            }
        }

        private async void btnDeleteProgram_Click(object sender, EventArgs e)
        {
            int trainerId;
            try { trainerId = GetLoggedInTrainerId(); }
            catch { MessageBox.Show("Please login as Trainer."); return; }

            var selected = GetSelectedProgram();
            if (selected == null) { MessageBox.Show("Please select a program to delete..."); return; }

            if (selected.TrainerId != trainerId)
            {
                MessageBox.Show("You can only delete your own programs.");
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure to delete program: '{selected.Title}' ?",
                "Delete Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                await _programApi.DeleteAsync(selected.Id);
                await LoadProgramsForLoggedInTrainer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Program Error");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
		}
    }
}
