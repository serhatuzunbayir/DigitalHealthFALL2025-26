namespace DigitalHealthTracker.Api.Dtos.Auth
{
	public class RegisterTrainerRequestDto
	{
		public string Name { get; set; } = "";
		public string Surname { get; set; } = "";
		public string Phone { get; set; } = "";
		public string Email { get; set; } = "";
		public string Password { get; set; } = "";
	}
}
