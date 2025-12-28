using System.ComponentModel.DataAnnotations;

namespace DigitalHealthTracker.Web.ViewModels.Trainer;

public class WorkoutProgramEditVm
{
	public int Id { get; set; }

	[Required]
	public int TrainerId { get; set; }

	[Required, StringLength(120)]
	public string Title { get; set; } = "";

	public List<WorkoutProgramItemEditVm> Items { get; set; } = new();

	// Workouts dropdown için (view’de select dolduracağız)
	public List<WorkoutLookupVm> Workouts { get; set; } = new();
}

public class WorkoutProgramItemEditVm
{
	[Required]
	public int WorkoutId { get; set; }

	// UI kullanmıyor ama API bekliyor
	[Range(1, 365)]
	public int DayNo { get; set; } = 1;

	[Range(1, 100)]
	public int Sets { get; set; } = 3;

	[Range(1, 500)]
	public int Reps { get; set; } = 10;
}

public class WorkoutLookupVm
{
	public int Id { get; set; }
	public string Name { get; set; } = "";
}
