using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalHealthTracker.Api.Controllers;

[ApiController]
[Route("api/Admin/trainers")]
public class AdminTrainersController : ControllerBase
{
	private readonly AppDbContext _context;
	public AdminTrainersController(AppDbContext context) => _context = context;

	// =========================
	// LIST
	// GET /api/Admin/trainers
	// =========================
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var list = await _context.Trainers
			.OrderByDescending(t => t.Id)
			.Select(t => new AdminTrainerRowDto
			{
				Id = t.Id,
				Name = t.Name,
				Surname = t.Surname,
				Phone = t.Phone,
				Email = t.Email,
				IsApproved = t.IsApproved
			})
			.ToListAsync();

		return Ok(list);
	}

	// =========================
	// DETAIL
	// GET /api/Admin/trainers/{id}
	// =========================
	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetById(int id)
	{
		var t = await _context.Trainers.SingleOrDefaultAsync(x => x.Id == id);
		if (t == null) return NotFound("Trainer not found.");

		return Ok(new AdminTrainerEditDto
		{
			Id = t.Id,
			Name = t.Name ?? "",
			Surname = t.Surname ?? "",
			Phone = t.Phone ?? "",
			Email = t.Email,
			IsApproved = t.IsApproved
		});
	}

	// =========================
	// UPDATE
	// PUT /api/Admin/trainers/{id}
	// =========================
	[HttpPut("{id:int}")]
	public async Task<IActionResult> Update(int id, [FromBody] AdminTrainerEditDto dto)
	{
		if (id != dto.Id) return BadRequest("Id mismatch.");

		var t = await _context.Trainers.SingleOrDefaultAsync(x => x.Id == id);
		if (t == null) return NotFound("Trainer not found.");

		// basic validations (minimal)
		if (string.IsNullOrWhiteSpace(dto.Name) ||
			string.IsNullOrWhiteSpace(dto.Surname) ||
			string.IsNullOrWhiteSpace(dto.Phone))
		{
			return BadRequest("Name, Surname and Phone are required.");
		}

		// phone uniqueness (users/trainers/admins)
		var phone = dto.Phone.Trim();
		var phoneTaken =
			await _context.Users.AnyAsync(u => u.Phone == phone) ||
			await _context.Admins.AnyAsync(a => a.Phone == phone) ||
			await _context.Trainers.AnyAsync(tr => tr.Phone == phone && tr.Id != id);

		if (phoneTaken)
			return Conflict("This phone number is already registered.");

		t.Name = dto.Name.Trim();
		t.Surname = dto.Surname.Trim();
		t.Phone = phone;

		// Email optional
		t.Email = string.IsNullOrWhiteSpace(dto.Email) ? null : dto.Email.Trim();

		// approval can be edited here too (optional)
		t.IsApproved = dto.IsApproved;

		await _context.SaveChangesAsync();
		return Ok();
	}

	// =========================
	// TOGGLE APPROVAL
	// PUT /api/Admin/trainers/{id}/toggle-approval
	// =========================
	[HttpPut("{id:int}/toggle-approval")]
	public async Task<IActionResult> ToggleApproval(int id)
	{
		var t = await _context.Trainers.SingleOrDefaultAsync(x => x.Id == id);
		if (t == null) return NotFound("Trainer not found.");

		t.IsApproved = !t.IsApproved;
		await _context.SaveChangesAsync();

		return Ok(new { t.Id, t.IsApproved });
	}

	// =========================
	// DELETE (safe-ish)
	// DELETE /api/Admin/trainers/{id}
	// =========================
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		var trainer = await _context.Trainers.SingleOrDefaultAsync(x => x.Id == id);
		if (trainer == null) return NotFound("Trainer not found.");

		// FK sorun çıkarmasın diye bağlı kayıtları da temizliyoruz.
		// (Senin modeline göre gerekirse burayı genişletiriz)
		var assignments = await _context.AssignedPrograms.Where(a => a.TrainerId == id).ToListAsync();
		if (assignments.Count > 0)
			_context.AssignedPrograms.RemoveRange(assignments);

		var logs = await _context.WorkoutLogs.Where(l => l.TrainerId == id).ToListAsync();
		if (logs.Count > 0)
			_context.WorkoutLogs.RemoveRange(logs);

		var programs = await _context.WorkoutPrograms
			.Include(p => p.Items)
			.Where(p => p.TrainerId == id)
			.ToListAsync();

		foreach (var p in programs)
		{
			if (p.Items != null && p.Items.Count > 0)
				_context.WorkoutProgramItems.RemoveRange(p.Items);
		}
		if (programs.Count > 0)
			_context.WorkoutPrograms.RemoveRange(programs);

		_context.Trainers.Remove(trainer);
		await _context.SaveChangesAsync();

		return Ok();
	}

	// =========================
	// API DTOs (local)
	// =========================
	public class AdminTrainerRowDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = "";
		public string Surname { get; set; } = "";
		public string Phone { get; set; } = "";
		public string? Email { get; set; }
		public bool IsApproved { get; set; }
	}

	public class AdminTrainerEditDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = "";
		public string Surname { get; set; } = "";
		public string Phone { get; set; } = "";
		public string? Email { get; set; }
		public bool IsApproved { get; set; }
	}
}
