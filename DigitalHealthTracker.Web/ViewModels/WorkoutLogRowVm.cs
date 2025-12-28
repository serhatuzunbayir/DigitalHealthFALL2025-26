namespace DigitalHealthTracker.Web.ViewModels;

public class WorkoutLogRowVm
{
	public int Id { get; set; }
	public string ProgramTitle { get; set; } = "";
	public string WorkoutName { get; set; } = "";
	public int DayNo { get; set; }
	public int Sets { get; set; }
	public int Reps { get; set; }
	public string CompletedAt { get; set; } = "";
}
