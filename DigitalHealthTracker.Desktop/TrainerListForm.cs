using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;

namespace DigitalHealthTracker.Desktop
{

    public partial class TrainerListForm : Form
    {

		public delegate void TrainerActionHandler(string message);
		public event TrainerActionHandler? TrainerChanged;
		public TrainerListForm()
        {
            InitializeComponent();
			ConfigureGrid();
			StyleGrid();
			LoadTrainers();
		}

		private void ConfigureGrid()
		{
			dgvTrainers.AutoGenerateColumns = true;
			dgvTrainers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvTrainers.MultiSelect = false;
			dgvTrainers.ReadOnly = true;
			dgvTrainers.AllowUserToAddRows = false;
		}


		private void LoadTrainers()
		{
			using (var context = new AppDbContext())
			{
				var trainers = context.Trainers
									  .OrderBy(t => t.Id)
									  .ToList();

				dgvTrainers.DataSource = null;
				dgvTrainers.DataSource = trainers;
			}
		}

		private void StyleGrid()
		{
			dgvTrainers.EnableHeadersVisualStyles = false;
			dgvTrainers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 170, 170);  // Turkuaz
			dgvTrainers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
			dgvTrainers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

			dgvTrainers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 248);
			dgvTrainers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 150, 150);
			dgvTrainers.DefaultCellStyle.SelectionForeColor = Color.White;
		}

		private Trainer? GetSelectedTrainer()
		{
			if (dgvTrainers.CurrentRow == null)
				return null;

			return dgvTrainers.CurrentRow.DataBoundItem as Trainer;
		}

		private void btnAddTrainer_Click(object sender, EventArgs e)
        {
			try
			{
				using (var frm = new TrainerEditForm())
				{
					var result = frm.ShowDialog();

					if (result == DialogResult.OK)
					{
						LoadTrainers();
						//El yapımı event
						TrainerChanged?.Invoke("A new trainer was added.");

					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Add Trainer Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnEditTrainer_Click(object sender, EventArgs e)
        {
			var selectedTrainer = GetSelectedTrainer();

			if (selectedTrainer == null)
			{
				MessageBox.Show("Please select a trainer to edit...");
				return;
			}

			try
			{
				using (var frm = new TrainerEditForm(selectedTrainer))
				{
					var result = frm.ShowDialog();

					if (result == DialogResult.OK)
					{
						LoadTrainers();
						//El Yapımı Event
						TrainerChanged?.Invoke($"Trainer '{selectedTrainer.Name} {selectedTrainer.Surname}' was updated.");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Edit Trainer Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void btnDeleteTrainer_Click(object sender, EventArgs e)
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

			if (result != DialogResult.Yes)
				return;

			try
			{
				using (var context = new AppDbContext())
				{
					var trainer = context.Trainers
										 .SingleOrDefault(t => t.Id == selectedTrainer.Id);

					if (trainer == null)
					{
						MessageBox.Show("Trainer not found in database.");
						return;
					}

					context.Trainers.Remove(trainer);
					context.SaveChanges();
				}

				LoadTrainers();
				//El Yapımı Event
				TrainerChanged?.Invoke($"Trainer '{selectedTrainer.Name} {selectedTrainer.Surname}' was deleted.");

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Delete Trainer Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
    }
}
