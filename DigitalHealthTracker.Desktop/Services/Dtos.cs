namespace DigitalHealthTracker.Desktop.Services
{
	// User -> Pending Assignments Grid
	public class UserPendingAssignmentDto
	{
		public int Id { get; set; }
		public string Program { get; set; } = "";
		public string Trainer { get; set; } = "";
		public string AssignedAt { get; set; } = "";
	}

	// API: /api/AssignedPrograms/user/{userId}
	public class UserAssignedProgramRowDto
	{
		public int Id { get; set; }
		public int WorkoutProgramId { get; set; }
		public string ProgramTitle { get; set; } = "";
		public string TrainerName { get; set; } = "";
		public string Status { get; set; } = "";
		public string AssignedAt { get; set; } = "";
		public string ApprovedAt { get; set; } = "";
	}

	// Active program info
	public class ActiveAssignmentDto
	{
		public int Id { get; set; }
		public int WorkoutProgramId { get; set; }
		public string ProgramTitle { get; set; } = "";
		public string TrainerName { get; set; } = "";
	}

	// Workout Logs Grid (Trainer -> User Logs, User -> History)
	public class WorkoutLogRowDto
	{
		public int Id { get; set; }
		public string ProgramTitle { get; set; } = "";
		public string WorkoutName { get; set; } = "";
		public int DayNo { get; set; }
		public int Sets { get; set; }
		public int Reps { get; set; }
		public string CompletedAt { get; set; } = "";
	}

	public class UserLookupDto
	{
		public int Id { get; set; }
		public string FullName { get; set; } = "";
		public double HeightCm { get; set; }
		public double WeightKg { get; set; }
	}
}
