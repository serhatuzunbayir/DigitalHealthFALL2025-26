using System.ComponentModel.DataAnnotations;

namespace DigitalHealthTracker.Web.ViewModels.Admin;

public class AdminUserEditVm
{
	public int Id { get; set; }

	[Required]
	[StringLength(50)]
	public string Name { get; set; } = "";

	[Required]
	[StringLength(50)]
	public string Surname { get; set; } = "";

	[Required]
	[StringLength(20)]
	public string Phone { get; set; } = "";

	[StringLength(120)]
	[EmailAddress]
	public string? Email { get; set; }

	[Range(0, 300)]
	public double HeightCm { get; set; }

	[Range(0, 600)]
	public double WeightKg { get; set; }

	[Range(1900, 2100)]
	public int BirthYear { get; set; }

	[StringLength(2000)]
	public string? MedicalRecord { get; set; }

	public string? Error { get; set; }
}
