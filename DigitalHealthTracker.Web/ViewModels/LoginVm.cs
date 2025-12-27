using System.ComponentModel.DataAnnotations;

namespace DigitalHealthTracker.Web.ViewModels;

public class LoginVm
{
	[Required]
	public string Phone { get; set; } = "";

	[Required]
	[DataType(DataType.Password)]
	public string Password { get; set; } = "";

	[Required]
	public string Role { get; set; } = "User"; // Admin | Trainer | User

	public string? Error { get; set; }
}
