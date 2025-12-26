namespace DigitalHealthTracker.Api.Dtos.Auth
{
	public class LoginRequestDto
	{
		public string Phone { get; set; } = "";
		public string Password { get; set; } = "";
		public string Role { get; set; } = ""; // Admin | Trainer | User
	}
}
