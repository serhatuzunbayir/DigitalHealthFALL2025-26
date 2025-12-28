using System.Text.Json.Serialization;

namespace DigitalHealthTracker.Web.ViewModels;

public class TrainerPendingVm
{
	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("surname")]
	public string? Surname { get; set; }

	[JsonPropertyName("displayName")]
	public string? DisplayName { get; set; }

	[JsonPropertyName("fullName")]
	public string? FullName { get; set; }

	[JsonPropertyName("phone")]
	public string? Phone { get; set; }

	// ✅ EKLENDİ
	[JsonPropertyName("email")]
	public string? Email { get; set; }

	// ✅ View için garantili isim
	[JsonIgnore]
	public string Display
	{
		get
		{
			if (!string.IsNullOrWhiteSpace(DisplayName)) return DisplayName!;
			if (!string.IsNullOrWhiteSpace(FullName)) return FullName!;
			var combined = $"{Name} {Surname}".Trim();
			return string.IsNullOrWhiteSpace(combined) ? "-" : combined;
		}
	}
}
