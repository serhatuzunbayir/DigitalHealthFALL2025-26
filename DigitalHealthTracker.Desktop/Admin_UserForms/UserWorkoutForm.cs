using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using DigitalHealthTracker.Desktop.Services;

namespace DigitalHealthTracker.Desktop
{
	public partial class UserWorkoutForm : Form
	{
		private const string ApiBaseUrl = "https://localhost:7193";

		private readonly AssignedProgramApiService _assignedApi = new AssignedProgramApiService();
		private readonly WorkoutLogApiService _logApi = new WorkoutLogApiService();
		private readonly HttpClient _http;

		// UI (runtime)
		private readonly ComboBox cmbActivePrograms = new ComboBox();
		private readonly Label lblSelectedInfo = new Label();
		private readonly DataGridView dgvProgramItems = new DataGridView();

		private readonly SplitContainer split = new SplitContainer();
		private readonly TableLayoutPanel topLayout = new TableLayoutPanel();
		private readonly FlowLayoutPanel bottomButtons = new FlowLayoutPanel();

		// data
		private List<ActiveListDto> _activeList = new();
		private Dictionary<int, string> _workoutNameMap = new();
		private ActiveComboItem? _selectedActive = null;

		public UserWorkoutForm()
		{
			InitializeComponent();

			StartPosition = FormStartPosition.CenterScreen;
			ClientSize = new Size(900, 500);
			MinimumSize = new Size(900, 500);

			_http = CreateApiClient();

			ConfigureHistoryGrid();
			StyleHistoryGrid();

			BuildResponsiveUi();

			Shown += (_, __) =>
			{
				BeginInvoke(new Action(() => ApplySplitterLayoutSafely()));
			};
			Resize += (_, __) => ApplySplitterLayoutSafely();
		}

		private HttpClient CreateApiClient()
		{
			var handler = new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback =
					HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
			};

			return new HttpClient(handler) { BaseAddress = new Uri(ApiBaseUrl) };
		}

		protected override async void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			await RefreshAll();
		}

		// -----------------------------
		// LAYOUT
		// -----------------------------
		private void BuildResponsiveUi()
		{
			split.Dock = DockStyle.Fill;
			split.Orientation = Orientation.Horizontal;
			split.SplitterWidth = 6;

			Controls.Add(split);

			// Panel1
			topLayout.Dock = DockStyle.Fill;
			topLayout.Padding = new Padding(12);
			topLayout.ColumnCount = 2;
			topLayout.RowCount = 4;

			topLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
			topLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));

			topLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
			topLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
			topLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
			topLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

			lblActiveProgram.Text = "Active Program: -";
			lblActiveProgram.AutoSize = true;

			topLayout.Controls.Add(lblActiveProgram, 0, 0);
			topLayout.SetColumnSpan(lblActiveProgram, 2);

			cmbActivePrograms.Dock = DockStyle.Fill;
			cmbActivePrograms.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbActivePrograms.SelectedIndexChanged += async (_, __) => await OnSelectedActiveChangedAsync();
			topLayout.Controls.Add(cmbActivePrograms, 0, 1);

			btnCompleteDay.Text = "Complete Selected";
			btnCompleteDay.Dock = DockStyle.Fill;
			topLayout.Controls.Add(btnCompleteDay, 1, 1);

			lblSelectedInfo.Text = "";
			lblSelectedInfo.AutoSize = true;
			lblSelectedInfo.ForeColor = Color.DimGray;
			topLayout.Controls.Add(lblSelectedInfo, 0, 2);
			topLayout.SetColumnSpan(lblSelectedInfo, 2);

			ConfigureProgramItemsGrid();
			dgvProgramItems.Dock = DockStyle.Fill;
			topLayout.Controls.Add(dgvProgramItems, 0, 3);
			topLayout.SetColumnSpan(dgvProgramItems, 2);

			split.Panel1.Controls.Add(topLayout);

			// Panel2
			var panel2 = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12) };
			split.Panel2.Controls.Add(panel2);

			dgvHistory.Dock = DockStyle.Fill;
			panel2.Controls.Add(dgvHistory);

			bottomButtons.Dock = DockStyle.Bottom;
			bottomButtons.Height = 42;
			bottomButtons.FlowDirection = FlowDirection.RightToLeft;
			bottomButtons.Padding = new Padding(0, 8, 0, 0);

			btnClose.Text = "Close";
			btnRefreshHistory.Text = "Refresh";
			btnClose.Width = 120;
			btnRefreshHistory.Width = 120;

			bottomButtons.Controls.Add(btnClose);
			bottomButtons.Controls.Add(btnRefreshHistory);

			panel2.Controls.Add(bottomButtons);
		}

		private void ApplySplitterLayoutSafely()
		{
			if (!IsHandleCreated) return;

			try
			{
				int total = split.ClientSize.Height;
				if (total <= 0) return;

				int panel1Min = 180;
				int panel2Min = 160;

				split.Panel1MinSize = panel1Min;
				split.Panel2MinSize = panel2Min;

				int desired = 240;
				int max = total - panel2Min - split.SplitterWidth;
				int min = panel1Min;

				if (max <= min) return;

				int safe = Math.Max(min, Math.Min(desired, max));

				if (split.SplitterDistance != safe)
					split.SplitterDistance = safe;
			}
			catch { }
		}

		private void ConfigureProgramItemsGrid()
		{
			dgvProgramItems.ReadOnly = true;
			dgvProgramItems.AllowUserToAddRows = false;
			dgvProgramItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvProgramItems.MultiSelect = false;
			dgvProgramItems.AutoGenerateColumns = false;

			dgvProgramItems.Columns.Clear();
			dgvProgramItems.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Workout", DataPropertyName = "WorkoutName", Width = 300 });
			dgvProgramItems.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Sets", DataPropertyName = "Sets", Width = 70 });
			dgvProgramItems.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Reps", DataPropertyName = "Reps", Width = 70 });
			dgvProgramItems.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "DayNo", DataPropertyName = "DayNo", Width = 70 });
		}

		private void ConfigureHistoryGrid()
		{
			dgvHistory.AutoGenerateColumns = true;
			dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvHistory.MultiSelect = false;
			dgvHistory.ReadOnly = true;
			dgvHistory.AllowUserToAddRows = false;
		}

		private void StyleHistoryGrid()
		{
			dgvHistory.EnableHeadersVisualStyles = false;
			dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 170, 170);
			dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
			dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

			dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 248);
			dgvHistory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 150, 150);
			dgvHistory.DefaultCellStyle.SelectionForeColor = Color.White;
		}

		// -----------------------------
		// BUSINESS
		// -----------------------------
		private int? GetLoggedInUserId()
		{
			if (Session.Role != AppRole.User || Session.UserId == null)
				return null;
			return Session.UserId.Value;
		}

		private async Task RefreshAll()
		{
			await LoadActiveList();
			await LoadWorkoutHistory();
		}

		// ✅ BURASI ÖNEMLİ: artık /active-list endpoint'i kullanıyoruz
		private async Task LoadActiveList()
		{
			var userId = GetLoggedInUserId();
			if (userId == null)
			{
				lblActiveProgram.Text = "Active Program: - (Please login as User)";
				cmbActivePrograms.DataSource = null;
				dgvProgramItems.DataSource = null;
				lblSelectedInfo.Text = "";
				return;
			}

			try
			{
				// API: GET /api/AssignedPrograms/user/{userId}/active-list
				var raw = await _http.GetStringAsync($"/api/AssignedPrograms/user/{userId.Value}/active-list");
				_activeList = ParseActiveList(raw);

				if (_activeList.Count == 0)
				{
					lblActiveProgram.Text = "Active Program: - (No active assignment)";
					lblSelectedInfo.Text = "No active assignment found.";
					cmbActivePrograms.DataSource = null;
					dgvProgramItems.DataSource = null;
					_selectedActive = null;
					return;
				}

				lblActiveProgram.Text = "Active Program: Select from the list ↓";

				// Workouts map (isim için)
				var workouts = await (_http.GetFromJsonAsync<List<WorkoutLookupDto>>("/api/Workouts")
					?? Task.FromResult(new List<WorkoutLookupDto>()));

				_workoutNameMap = workouts
					.GroupBy(w => w.Id)
					.ToDictionary(g => g.Key, g => g.First().Name ?? g.First().Title ?? $"Workout #{g.Key}");

				var combo = _activeList
					.OrderByDescending(x => x.Id)
					.Select(x => new ActiveComboItem
					{
						AssignmentId = x.Id,
						ProgramId = x.WorkoutProgramId,
						ProgramTitle = x.ProgramTitle ?? $"Program #{x.WorkoutProgramId}",
						TrainerName = x.TrainerName ?? "-"
					})
					.ToList();

				cmbActivePrograms.DataSource = null;
				cmbActivePrograms.DataSource = combo;
				cmbActivePrograms.DisplayMember = nameof(ActiveComboItem.Display);
				cmbActivePrograms.ValueMember = nameof(ActiveComboItem.AssignmentId);

				cmbActivePrograms.SelectedIndex = 0;
				await OnSelectedActiveChangedAsync();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "API ERROR (Active List)", MessageBoxButtons.OK, MessageBoxIcon.Error);
				lblActiveProgram.Text = "Active Program: - (API error)";
				cmbActivePrograms.DataSource = null;
				dgvProgramItems.DataSource = null;
				lblSelectedInfo.Text = "";
			}
		}

		private async Task OnSelectedActiveChangedAsync()
		{
			if (cmbActivePrograms.SelectedItem is not ActiveComboItem item)
			{
				_selectedActive = null;
				dgvProgramItems.DataSource = null;
				lblSelectedInfo.Text = "";
				return;
			}

			_selectedActive = item;
			lblSelectedInfo.Text = $"Selected: {item.ProgramTitle} | Trainer: {item.TrainerName} | AssignId: {item.AssignmentId}";

			try
			{
				var detail = await _http.GetFromJsonAsync<WorkoutProgramDetailDto>($"/api/WorkoutPrograms/{item.ProgramId}");
				if (detail == null)
				{
					dgvProgramItems.DataSource = null;
					return;
				}

				var rows = (detail.Items ?? new List<WorkoutProgramItemDto>())
					.Select(it => new ProgramItemRow
					{
						WorkoutId = it.WorkoutId,
						WorkoutName = _workoutNameMap.TryGetValue(it.WorkoutId, out var n) ? n : $"Workout #{it.WorkoutId}",
						Sets = it.Sets,
						Reps = it.Reps,
						DayNo = it.DayNo
					})
					.ToList();

				dgvProgramItems.DataSource = null;
				dgvProgramItems.DataSource = rows;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "API ERROR (Program Detail)", MessageBoxButtons.OK, MessageBoxIcon.Error);
				dgvProgramItems.DataSource = null;
			}
		}

		private async Task LoadWorkoutHistory()
		{
			var userId = GetLoggedInUserId();
			if (userId == null)
			{
				dgvHistory.DataSource = null;
				return;
			}

			try
			{
				var logs = await _logApi.GetUserHistoryAsync(userId.Value);
				dgvHistory.DataSource = null;
				dgvHistory.DataSource = logs;

				if (dgvHistory.Columns["Id"] != null)
					dgvHistory.Columns["Id"].Visible = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "API ERROR (Logs)", MessageBoxButtons.OK, MessageBoxIcon.Error);
				dgvHistory.DataSource = null;
			}
		}

		private async void btnCompleteDay_Click(object sender, EventArgs e)
		{
			var userId = GetLoggedInUserId();
			if (userId == null)
			{
				MessageBox.Show("Please login as User.");
				return;
			}

			if (_selectedActive == null)
			{
				MessageBox.Show("Select an active program first.");
				return;
			}

			try
			{
				await _assignedApi.CompleteWithLogsAsync(_selectedActive.AssignmentId, userId.Value);
				MessageBox.Show("Program COMPLETED. All workouts logged ✅");
				await RefreshAll();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Complete Program Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private async void btnRefreshHistory_Click(object sender, EventArgs e) => await RefreshAll();
		private void btnClose_Click(object sender, EventArgs e) => Close();

		// -----------------------------
		// Parse active-list JSON
		// -----------------------------
		private static List<ActiveListDto> ParseActiveList(string json)
		{
			var list = new List<ActiveListDto>();
			if (string.IsNullOrWhiteSpace(json)) return list;

			using var doc = JsonDocument.Parse(json);
			if (doc.RootElement.ValueKind != JsonValueKind.Array) return list;

			foreach (var el in doc.RootElement.EnumerateArray())
			{
				if (el.ValueKind != JsonValueKind.Object) continue;

				list.Add(new ActiveListDto
				{
					Id = GetInt(el, "id", "Id"),
					WorkoutProgramId = GetInt(el, "workoutProgramId", "WorkoutProgramId"),
					ProgramTitle = GetString(el, "programTitle", "ProgramTitle", "workoutProgramTitle", "WorkoutProgramTitle"),
					TrainerName = GetString(el, "trainerName", "TrainerName")
				});
			}

			return list.Where(x => x.Id > 0 && x.WorkoutProgramId > 0).ToList();
		}

		private static int GetInt(JsonElement obj, params string[] keys)
		{
			foreach (var p in obj.EnumerateObject())
				foreach (var k in keys)
					if (string.Equals(p.Name, k, StringComparison.OrdinalIgnoreCase))
					{
						if (p.Value.ValueKind == JsonValueKind.Number && p.Value.TryGetInt32(out var n)) return n;
						if (p.Value.ValueKind == JsonValueKind.String && int.TryParse(p.Value.GetString(), out var s)) return s;
					}
			return 0;
		}

		private static string? GetString(JsonElement obj, params string[] keys)
		{
			foreach (var p in obj.EnumerateObject())
				foreach (var k in keys)
					if (string.Equals(p.Name, k, StringComparison.OrdinalIgnoreCase))
					{
						if (p.Value.ValueKind == JsonValueKind.String) return p.Value.GetString();
						if (p.Value.ValueKind == JsonValueKind.Number) return p.Value.GetRawText();
					}
			return null;
		}

		// -----------------------------
		// DTOs
		// -----------------------------
		private sealed class ActiveListDto
		{
			public int Id { get; set; }
			public int WorkoutProgramId { get; set; }
			public string? ProgramTitle { get; set; }
			public string? TrainerName { get; set; }
		}

		private sealed class WorkoutProgramDetailDto
		{
			[JsonPropertyName("items")] public List<WorkoutProgramItemDto>? Items { get; set; }
		}

		private sealed class WorkoutProgramItemDto
		{
			[JsonPropertyName("workoutId")] public int WorkoutId { get; set; }
			[JsonPropertyName("dayNo")] public int DayNo { get; set; }
			[JsonPropertyName("sets")] public int Sets { get; set; }
			[JsonPropertyName("reps")] public int Reps { get; set; }
		}

		private sealed class WorkoutLookupDto
		{
			[JsonPropertyName("id")] public int Id { get; set; }
			[JsonPropertyName("name")] public string? Name { get; set; }
			[JsonPropertyName("title")] public string? Title { get; set; }
		}

		private sealed class ActiveComboItem
		{
			public int AssignmentId { get; set; }
			public int ProgramId { get; set; }
			public string ProgramTitle { get; set; } = "";
			public string TrainerName { get; set; } = "";
			public string Display => $"{ProgramTitle} | Trainer: {TrainerName} | AssignId: {AssignmentId}";
		}

		private sealed class ProgramItemRow
		{
			public int WorkoutId { get; set; }
			public string WorkoutName { get; set; } = "";
			public int Sets { get; set; }
			public int Reps { get; set; }
			public int DayNo { get; set; }
		}
	}
}
