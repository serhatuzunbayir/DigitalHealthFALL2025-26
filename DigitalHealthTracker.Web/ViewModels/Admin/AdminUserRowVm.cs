namespace DigitalHealthTracker.Web.ViewModels.Admin;

public class AdminUserRowVm
{
	public int Id { get; set; }
	public string Name { get; set; } = "";
	public string Surname { get; set; } = "";
	public string Phone { get; set; } = "";
	public string? Email { get; set; }

	public double? HeightCm { get; set; }
	public double? WeightKg { get; set; }
	public int? BirthYear { get; set; }
}
