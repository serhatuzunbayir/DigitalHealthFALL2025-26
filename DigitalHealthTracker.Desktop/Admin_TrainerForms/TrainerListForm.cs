using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DigitalHealthTracker.Data.Entities;
using DigitalHealthTracker.Desktop.Services;

namespace DigitalHealthTracker.Desktop
{
    public partial class TrainerListForm : Form
    {
        public delegate void TrainerActionHandler(string message);
        public event TrainerActionHandler? TrainerChanged;

        private readonly TrainerApiService _trainerApi = new TrainerApiService();

        public TrainerListForm()
        {
            InitializeComponent();
            ConfigureGrid();
            StyleGrid();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadTrainers();
        }

        private void ConfigureGrid()
        {
            dgvTrainers.AutoGenerateColumns = true;
            dgvTrainers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTrainers.MultiSelect = false;
            dgvTrainers.ReadOnly = true;
            dgvTrainers.AllowUserToAddRows = false;
        }

        private async Task LoadTrainers()
        {
            try
            {
                var trainers = await _trainerApi.GetAllAsync();
                dgvTrainers.DataSource = null;
                dgvTrainers.DataSource = trainers.OrderBy(t => t.Id).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("API Trainer Load Error: " + ex.Message);
            }
        }

        private void StyleGrid()
        {
            dgvTrainers.EnableHeadersVisualStyles = false;
            dgvTrainers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 170, 170);
            dgvTrainers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTrainers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvTrainers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 248);
            dgvTrainers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 150, 150);
            dgvTrainers.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private Trainer? GetSelectedTrainer()
        {
            return dgvTrainers.CurrentRow?.DataBoundItem as Trainer;
        }

		private async void btnEditTrainer_Click(object sender, EventArgs e)
		{
			var selectedTrainer = GetSelectedTrainer();
			if (selectedTrainer == null)
			{
				MessageBox.Show("Please select a trainer to edit...");
				return;
			}

			try
			{
				
				var wasApproved = selectedTrainer.IsApproved;

				using var frm = new TrainerEditForm(selectedTrainer);
				if (frm.ShowDialog() != DialogResult.OK) return;

				// Formdan gelen yeni değerler
				var edited = frm.EditedTrainer;

				// 1) Önce temel alanları update et (Name/Surname/Phone/Email)
				await _trainerApi.UpdateAsync(selectedTrainer.Id, edited);

				// 2) Checkbox değiştiyse approve/unapprove endpoint çağır
				if (edited.IsApproved != wasApproved)
				{
					if (edited.IsApproved)
						await _trainerApi.ApproveAsync(selectedTrainer.Id);
					else
						await _trainerApi.UnapproveAsync(selectedTrainer.Id);
				}

				await LoadTrainers();

				TrainerChanged?.Invoke($"Trainer '{edited.Name} {edited.Surname}' was updated.");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Edit Trainer Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private async void btnDeleteTrainer_Click(object sender, EventArgs e)
        {
            var selectedTrainer = GetSelectedTrainer();
            if (selectedTrainer == null)
            {
                MessageBox.Show("Please select a trainer to delete...");
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure to delete trainer: '{selectedTrainer.Name} {selectedTrainer.Surname}' ?",
                "Delete Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                await _trainerApi.DeleteAsync(selectedTrainer.Id);
                await LoadTrainers();
                TrainerChanged?.Invoke($"Trainer '{selectedTrainer.Name} {selectedTrainer.Surname}' was deleted.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Delete Trainer Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();    
        }
    }
}
