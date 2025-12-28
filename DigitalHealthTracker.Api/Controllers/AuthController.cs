using DigitalHealthTracker.Contracts.Auth;
using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using DigitalHealthTracker.Data.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalHealthTracker.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly AppDbContext _context;
		public AuthController(AppDbContext context) => _context = context;

		// POST: /api/Auth/login
		[HttpPost("login")]
		public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto req)
		{
			var role = (req.Role ?? "").Trim();
			var phone = (req.Phone ?? "").Trim();
			var password = req.Password ?? "";

			if (string.IsNullOrWhiteSpace(role) ||
				string.IsNullOrWhiteSpace(phone) ||
				string.IsNullOrWhiteSpace(password))
			{
				return BadRequest("Role, Phone and Password are required.");
			}

			if (role == "Admin")
			{
				var admin = await _context.Admins.SingleOrDefaultAsync(a => a.Phone == phone);
				if (admin == null || !PasswordHasher.VerifyPassword(password, admin.PasswordHash))
					return Unauthorized("Invalid admin credentials.");

				return Ok(new LoginResponseDto
				{
					Role = "Admin",
					Id = admin.Id,
					DisplayName = admin.Name
				});
			}

			if (role == "Trainer")
			{
				var trainer = await _context.Trainers.SingleOrDefaultAsync(t => t.Phone == phone);
				if (trainer == null || !PasswordHasher.VerifyPassword(password, trainer.PasswordHash))
					return Unauthorized("Invalid trainer credentials.");

				// ✅ FIX: Forbid() KULLANMA (auth handler yoksa patlar)
				// Bunun yerine 403 + message dön.
				if (!trainer.IsApproved)
					return StatusCode(403, "Trainer is not approved yet.");

				return Ok(new LoginResponseDto
				{
					Role = "Trainer",
					Id = trainer.Id,
					DisplayName = $"{trainer.Name} {trainer.Surname}"
				});
			}

			if (role == "User")
			{
				var user = await _context.Users.SingleOrDefaultAsync(u => u.Phone == phone);
				if (user == null || !PasswordHasher.VerifyPassword(password, user.PasswordHash))
					return Unauthorized("Invalid user credentials.");

				return Ok(new LoginResponseDto
				{
					Role = "User",
					Id = user.Id,
					DisplayName = $"{user.Name} {user.Surname}"
				});
			}

			return BadRequest("Invalid role. Use: Admin, Trainer, User.");
		}

		// POST: /api/Auth/register-user
		[HttpPost("register-user")]
		public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequestDto req)
		{
			var phone = (req.Phone ?? "").Trim();

			if (string.IsNullOrWhiteSpace(req.Name) ||
				string.IsNullOrWhiteSpace(req.Surname) ||
				string.IsNullOrWhiteSpace(phone) ||
				string.IsNullOrWhiteSpace(req.Password))
			{
				return BadRequest("All fields are required.");
			}

			if (phone.Length != 10)
				return BadRequest("Please enter a valid phone number.");

			var phoneExists =
				await _context.Users.AnyAsync(u => u.Phone == phone) ||
				await _context.Trainers.AnyAsync(t => t.Phone == phone) ||
				await _context.Admins.AnyAsync(a => a.Phone == phone);

			if (phoneExists)
				return Conflict("This phone number is already registered.");

			var user = new User
			{
				Name = req.Name.Trim(),
				Surname = req.Surname.Trim(),
				Phone = phone,
				Email = (req.Email ?? "").Trim(),
				PasswordHash = PasswordHasher.HashPassword(req.Password)
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return Ok(new { user.Id });
		}

		// POST: /api/Auth/register-trainer
		[HttpPost("register-trainer")]
		public async Task<IActionResult> RegisterTrainer([FromBody] RegisterTrainerRequestDto req)
		{
			var phone = (req.Phone ?? "").Trim();

			if (string.IsNullOrWhiteSpace(req.Name) ||
				string.IsNullOrWhiteSpace(req.Surname) ||
				string.IsNullOrWhiteSpace(phone) ||
				string.IsNullOrWhiteSpace(req.Password))
			{
				return BadRequest("All fields are required.");
			}

			if (phone.Length != 10)
				return BadRequest("Please enter a valid phone number.");

			var phoneExists =
				await _context.Users.AnyAsync(u => u.Phone == phone) ||
				await _context.Trainers.AnyAsync(t => t.Phone == phone) ||
				await _context.Admins.AnyAsync(a => a.Phone == phone);

			if (phoneExists)
				return Conflict("This phone number is already registered.");

			var trainer = new Trainer
			{
				Name = req.Name.Trim(),
				Surname = req.Surname.Trim(),
				Phone = phone,
				Email = (req.Email ?? "").Trim(),
				PasswordHash = PasswordHasher.HashPassword(req.Password),
				IsApproved = false
			};

			_context.Trainers.Add(trainer);
			await _context.SaveChangesAsync();

			return Ok(new { trainer.Id });
		}
	}
}
