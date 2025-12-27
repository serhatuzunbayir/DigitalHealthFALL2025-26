namespace DigitalHealthTracker.Web.ViewModels;

public class AssignProgramVm
{
	public int UserId { get; set; }
	public int WorkoutProgramId { get; set; }

	// dropdown için
	public List<UserLookupVm> Users { get; set; } = new();
	public List<ProgramPickVm> Programs { get; set; } = new();

	public string? Error { get; set; }
}

public class ProgramPickVm
{
	public int Id { get; set; }
	public string Title { get; set; } = "";
}
