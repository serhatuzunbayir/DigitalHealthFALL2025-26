namespace DigitalHealthTracker.Web.ViewModels;

public class UserDashboardVm
{
	public HealthVm? Health { get; set; }

	public List<UserActiveAssignmentVm> ActiveAssignments { get; set; } = new();

	public List<UserAssignedProgramRowVm> AssignedPrograms { get; set; } = new();
}

public class UserActiveAssignmentVm
{
	public int Id { get; set; }                  // assignment id
	public int WorkoutProgramId { get; set; }
	public string ProgramTitle { get; set; } = "";
	public string TrainerName { get; set; } = "";
}

public class HealthVm
{
	public double HeightCm { get; set; }
	public double WeightKg { get; set; }
	public double Bmi { get; set; }
	public string Category { get; set; } = "-";

	// mevcut isimler
	public double TargetWeightKg { get; set; }
	public double WeightDiffKg { get; set; }

	// ✅ eski yerler kırılmasın diye alias
	public double TargetWeight => TargetWeightKg;
	public double WeightDiff => WeightDiffKg;
}
