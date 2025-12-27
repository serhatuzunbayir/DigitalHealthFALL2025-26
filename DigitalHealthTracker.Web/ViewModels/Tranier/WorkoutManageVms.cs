using System.ComponentModel.DataAnnotations;

namespace DigitalHealthTracker.Web.ViewModels.Trainer;

public class WorkoutListRowVm
{
	public int Id { get; set; }
	public string Name { get; set; } = "";
	public string? Description { get; set; }
}

public class WorkoutEditVm
{
	public int Id { get; set; }

	[Required]
	[StringLength(80)]
	public string Name { get; set; } = "";

	[StringLength(500)]
	public string? Description { get; set; }

	// UI için basit hata göstermek istersen
	public string? Error { get; set; }
}
