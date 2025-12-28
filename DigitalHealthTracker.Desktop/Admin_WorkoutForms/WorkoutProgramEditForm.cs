using DigitalHealthTracker.Data.Entities;
using DigitalHealthTracker.Desktop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalHealthTracker.Desktop
{
	public partial class WorkoutProgramEditForm : Form
	{
		private int trainerId;
		private int? programIdEdit;

		private readonly List<WorkoutProgramItem> tempItems = new List<WorkoutProgramItem>();

		private readonly WorkoutApiService _workoutApi = new WorkoutApiService();
		private readonly WorkoutProgramApiService _programApi = new WorkoutProgramApiService();

		public WorkoutProgramEditForm(int trainerId)
		{
			InitializeComponent();
			this.trainerId = trainerId;

			ConfigureItemsGrid();
		}

		public WorkoutProgramEditForm(int trainerId, WorkoutProgram tempProgram) : this(trainerId)
		{
			programIdEdit = tempProgram.Id;
		}

		protected override async void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			await LoadWorkoutsToCombo();

			if (programIdEdit != null)
				await LoadProgramForEdit(programIdEdit.Value);
		}

		private void ConfigureItemsGrid()
		{
			dgvItems.AutoGenerateColumns = true;
			dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvItems.MultiSelect = false;
			dgvItems.ReadOnly = true;
			dgvItems.AllowUserToAddRows = false;
		}

		private async Task LoadWorkoutsToCombo()
		{
			var workouts = await _workoutApi.GetAllAsync();
			cmbWorkouts.DataSource = workouts;
			cmbWorkouts.DisplayMember = "Name";
			cmbWorkouts.ValueMember = "Id";
		}

		private async Task LoadProgramForEdit(int programId)
		{
			var program = await _programApi.GetByIdAsync(programId);
			if (program == null)
			{
				MessageBox.Show("Program not found.");
				Close();
				return;
			}

			txtTitle.Text = program.Title;

			tempItems.Clear();
			foreach (var it in program.Items)
			{
				tempItems.Add(new WorkoutProgramItem
				{
					WorkoutId = it.WorkoutId,
					DayNo = it.DayNo,
					Sets = it.Sets,
					Reps = it.Reps
				});
			}

			RefreshItemsGrid();
		}

		private void RefreshItemsGrid()
		{
			// Combo datasından (Id, Name) map çıkar (DTO'ya bağımlı kalmadan)
			var workoutMap = new Dictionary<int, string>();

			if (cmbWorkouts.DataSource is System.Collections.IEnumerable items)
			{
				foreach (var obj in items)
				{
					var t = obj.GetType();
					var idProp = t.GetProperty("Id");
					var nameProp = t.GetProperty("Name");

					if (idProp == null || nameProp == null) continue;

					int id = (int)idProp.GetValue(obj);
					string name = (string)(nameProp.GetValue(obj) ?? "");
					if (!workoutMap.ContainsKey(id))
						workoutMap.Add(id, name);
				}
			}

			var view = tempItems
				.Select(it => new
				{
					it.WorkoutId,
					WorkoutName = workoutMap.ContainsKey(it.WorkoutId) ? workoutMap[it.WorkoutId] : "",
					it.DayNo,
					it.Sets,
					it.Reps
				})
				.OrderBy(x => x.DayNo)
				.ThenBy(x => x.WorkoutName)
				.ToList();

			dgvItems.DataSource = null;
			dgvItems.DataSource = view;
		}


		private void btnAddItem_Click(object sender, EventArgs e)
		{
			if (cmbWorkouts.SelectedValue == null)
			{
				MessageBox.Show("Please select a workout.");
				return;
			}

			if (!int.TryParse(txtSets.Text, out int sets) || sets <= 0)
			{
				MessageBox.Show("Sets must be a positive number.");
				return;
			}

			if (!int.TryParse(txtReps.Text, out int reps) || reps <= 0)
			{
				MessageBox.Show("Reps must be a positive number.");
				return;
			}

			int workoutId = (int)cmbWorkouts.SelectedValue;

			tempItems.Add(new WorkoutProgramItem
			{
				WorkoutId = workoutId,
				DayNo = 1, // sende hep 1; istersen txtDayNo ekleriz
				Sets = sets,
				Reps = reps
			});

			RefreshItemsGrid();
		}

		private async void btnSave_Click(object sender, EventArgs e)
		{
			var title = txtTitle.Text.Trim();
			if (string.IsNullOrWhiteSpace(title))
			{
				MessageBox.Show("Program title is required.");
				return;
			}

			if (tempItems.Count == 0)
			{
				MessageBox.Show("Please add at least one workout item.");
				return;
			}

			var req = new WorkoutProgramSaveRequest
			{
				TrainerId = trainerId,
				Title = title,
				Items = tempItems.Select(it => new WorkoutProgramItemSave
				{
					WorkoutId = it.WorkoutId,
					DayNo = it.DayNo,
					Sets = it.Sets,
					Reps = it.Reps
				}).ToList()
			};

			try
			{
				if (programIdEdit == null)
				{
					await _programApi.CreateAsync(req);
				}
				else
				{
					await _programApi.UpdateAsync(programIdEdit.Value, req);
				}

				DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Save Error");
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void btnRemoveItem_Click(object sender, EventArgs e)
		{
			if (dgvItems.CurrentRow == null)
			{
				MessageBox.Show("Please select an item to remove.");
				return;
			}

			int workoutId = Convert.ToInt32(dgvItems.CurrentRow.Cells["WorkoutId"].Value);
			int dayNo = Convert.ToInt32(dgvItems.CurrentRow.Cells["DayNo"].Value);
			int sets = Convert.ToInt32(dgvItems.CurrentRow.Cells["Sets"].Value);
			int reps = Convert.ToInt32(dgvItems.CurrentRow.Cells["Reps"].Value);

			var item = tempItems.FirstOrDefault(x =>
				x.WorkoutId == workoutId &&
				x.DayNo == dayNo &&
				x.Sets == sets &&
				x.Reps == reps);

			if (item == null)
			{
				MessageBox.Show("Selected item not found.");
				return;
			}

			tempItems.Remove(item);
			RefreshItemsGrid();
		}
	}
}
