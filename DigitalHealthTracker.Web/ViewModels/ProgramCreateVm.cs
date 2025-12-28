using System.ComponentModel.DataAnnotations;

namespace DigitalHealthTracker.Web.ViewModels;

public class ProgramCreateVm
{
	[Required(ErrorMessage = "Title is required.")]
	public string Title { get; set; } = "";

	[Range(1, int.MaxValue, ErrorMessage = "Workout is required.")]
	public int WorkoutId { get; set; }

	[Range(1, 50, ErrorMessage = "Sets must be >= 1.")]
	public int Sets { get; set; } = 3;

	[Range(1, 500, ErrorMessage = "Reps must be >= 1.")]
	public int Reps { get; set; } = 10;

	// dropdown
	public List<WorkoutPickVm> Workouts { get; set; } = new();

	public string? Error { get; set; }
}

public class WorkoutPickVm
{
	public int Id { get; set; }
	public string Name { get; set; } = "";
}
