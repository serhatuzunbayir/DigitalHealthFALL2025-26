namespace DigitalHealthTracker.Web.ViewModels;

public class UserAssignedProgramRowVm
{
	public int Id { get; set; }
	public int WorkoutProgramId { get; set; }
	public string ProgramTitle { get; set; } = "";
	public string TrainerName { get; set; } = "";
	public string Status { get; set; } = "";
	public string AssignedAt { get; set; } = "";
	public string ApprovedAt { get; set; } = "";
}
