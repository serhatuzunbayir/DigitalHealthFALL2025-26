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
}
