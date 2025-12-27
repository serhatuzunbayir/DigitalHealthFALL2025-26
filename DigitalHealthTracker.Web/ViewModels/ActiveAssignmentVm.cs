namespace DigitalHealthTracker.Web.ViewModels;

public class ActiveAssignmentVm
{
	public int Id { get; set; }
	public int WorkoutProgramId { get; set; }
	public string ProgramTitle { get; set; } = "";
	public string TrainerName { get; set; } = "";
}
