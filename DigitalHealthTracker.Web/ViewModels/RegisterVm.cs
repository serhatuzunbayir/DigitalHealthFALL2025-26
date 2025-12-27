using System.ComponentModel.DataAnnotations;

namespace DigitalHealthTracker.Web.ViewModels;

public class RegisterVm
{
	[Required]
	public string Name { get; set; } = "";

	[Required]
	public string Surname { get; set; } = "";

	[Required]
	public string Phone { get; set; } = "";

	[Required, EmailAddress]
	public string Email { get; set; } = "";

	[Required, MinLength(4)]
	public string Password { get; set; } = "";

	[Required, Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
	public string ConfirmPassword { get; set; } = "";

	public string Error { get; set; } = "";
}
