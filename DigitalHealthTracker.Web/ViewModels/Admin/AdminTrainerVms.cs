using System.ComponentModel.DataAnnotations;

namespace DigitalHealthTracker.Web.ViewModels.Admin;

public class AdminTrainerRowVm
{
	public int Id { get; set; }
	public string Name { get; set; } = "";
	public string Surname { get; set; } = "";
	public string Phone { get; set; } = "";
	public string? Email { get; set; }
	public bool IsApproved { get; set; }
}

public class AdminTrainerEditVm
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

	public bool IsApproved { get; set; }

	public string? Error { get; set; }
}
