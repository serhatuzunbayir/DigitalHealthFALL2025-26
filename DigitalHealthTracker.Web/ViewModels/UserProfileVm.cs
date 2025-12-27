using System.ComponentModel.DataAnnotations;

namespace DigitalHealthTracker.Web.ViewModels;

public class UserProfileVm
{
	public int Id { get; set; }

	[Required(ErrorMessage = "Name is required.")]
	public string Name { get; set; } = "";

	[Required(ErrorMessage = "Surname is required.")]
	public string Surname { get; set; } = "";

	[Required(ErrorMessage = "Phone is required.")]
	public string Phone { get; set; } = "";

	// optional
	public string? Email { get; set; }

	[Required(ErrorMessage = "Height is required.")]
	[Range(1, 300, ErrorMessage = "Height must be between 1 and 300 cm.")]
	public double HeightCm { get; set; }

	[Required(ErrorMessage = "Weight is required.")]
	[Range(1, 500, ErrorMessage = "Weight must be between 1 and 500 kg.")]
	public double WeightKg { get; set; }

	[Required(ErrorMessage = "Birth year is required.")]
	[Range(1900, 2100, ErrorMessage = "Birth year must be valid.")]
	public int BirthYear { get; set; }

	// optional
	public string? MedicalRecord { get; set; }

	public string? Error { get; set; }
}
