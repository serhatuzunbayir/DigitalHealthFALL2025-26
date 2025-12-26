namespace DigitalHealthTracker.Api.Dtos.Auth
{
	public class LoginResponseDto
	{
		public int Id { get; set; }
		public string Role { get; set; } = "";
		public string DisplayName { get; set; } = "";
	}
}
