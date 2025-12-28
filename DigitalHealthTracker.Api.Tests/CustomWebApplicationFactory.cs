using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Data.Sqlite;
using DigitalHealthTracker.Data;

namespace DigitalHealthTracker.Api.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
	private SqliteConnection? _connection;

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureServices(services =>
		{
			// ✅ TEST AUTH (Admin rolü)
			services.AddAuthentication("Test")
				.AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
					"Test", _ => { });
			services.AddAuthorization();

			// ❌ Gerçek DbContext kaydını kaldır
			var dbDescriptor = services.SingleOrDefault(
				d => d.ServiceType == typeof(DbContextOptions<AppDbContext>)
			);
			if (dbDescriptor != null)
				services.Remove(dbDescriptor);

			// ✅ TEK paylaşılan SQLite in-memory connection
			_connection = new SqliteConnection("DataSource=:memory:");
			_connection.Open();
			services.AddSingleton(_connection);

			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseSqlite(_connection);
			});

			// ✅ MIGRATION ile schema oluştur (EnsureCreated YOK)
			var sp = services.BuildServiceProvider();
			using var scope = sp.CreateScope();
			var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

			db.Database.Migrate();
		});
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
		if (disposing)
		{
			_connection?.Dispose();
			_connection = null;
		}
	}

	public void SeedTrainer(string name = "Test", string surname = "Trainer", string phone = "5550000000")
	{
		using var scope = Services.CreateScope();
		var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

		db.Trainers.Add(new DigitalHealthTracker.Data.Entities.Trainer
		{
			Name = name,
			Surname = surname,
			Phone = phone,
			Email = "test@trainer.com",
			IsApproved = false
		});

		db.SaveChanges();
	}


	public void SeedWorkoutLogForTrainer(int trainerId)
	{
		using var scope = Services.CreateScope();
		var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

		void SaveStep(string step)
		{
			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateException ex)
			{
				var msg = ex.InnerException?.Message ?? ex.Message;
				throw new Exception($"SeedWorkoutLogForTrainer FAILED at step: {step} | {msg}", ex);
			}
		}

		// 0) Trainer var mı?
		var trainer = db.Trainers.First(t => t.Id == trainerId);

		// 1) User
		var user = new DigitalHealthTracker.Data.Entities.User
		{
			Name = "Test",
			Surname = "User",
			Phone = Guid.NewGuid().ToString(),
			PasswordHash = "x"
		};
		db.Users.Add(user);
		SaveStep("Insert User");

		// 2) Workout
		var workout = new DigitalHealthTracker.Data.Entities.Workout
		{
			Name = "Test Workout"
		};
		db.Workouts.Add(workout);
		SaveStep("Insert Workout");

		// 3) WorkoutProgram (sende Name değil Title)
		var program = new DigitalHealthTracker.Data.Entities.WorkoutProgram
		{
			Title = "Test Program",
			TrainerId = trainer.Id
		};
		db.WorkoutPrograms.Add(program);
		SaveStep("Insert WorkoutProgram");

		// 4) AssignedProgram (TrainerId ZORUNLU!)
		var assigned = new DigitalHealthTracker.Data.Entities.AssignedProgram
		{
			UserId = user.Id,
			TrainerId = trainer.Id,          // ✅ kritik fix
			WorkoutProgramId = program.Id
		};
		db.AssignedPrograms.Add(assigned);
		SaveStep("Insert AssignedProgram");

		// 5) WorkoutLog (FK'lerin hepsi dolu)
		var log = new DigitalHealthTracker.Data.Entities.WorkoutLog
		{
			UserId = user.Id,
			TrainerId = trainer.Id,
			WorkoutProgramId = program.Id,
			WorkoutId = workout.Id,
			AssignedProgramId = assigned.Id,
			DayNo = 1,
			Sets = 3,
			Reps = 10,
			CompletedAt = DateTime.Now
		};

		db.WorkoutLogs.Add(log);
		SaveStep("Insert WorkoutLog");
	}






}
