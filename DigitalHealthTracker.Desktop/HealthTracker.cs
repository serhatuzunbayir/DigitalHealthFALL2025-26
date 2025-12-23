using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using System;
using System.Linq;
using System.Windows.Forms;
using DigitalHealthTracker.Desktop.RegistrationAndLogin;

namespace DigitalHealthTracker.Desktop
{
	internal static class HealthTracker
	{
		private const string DemoAdminPhone = "5550000000";
		private const string DemoAdminPassword = "admin123";

		private const string DemoUserPhone = "5555555555";
		private const string DemoTrainerPhone = "5444444444";

		[STAThread]
		static void Main()
		{
			try
			{
				EnsureDatabaseAndSeed();

				ApplicationConfiguration.Initialize();
				Application.Run(new LoginForm());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Startup Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private static void EnsureDatabaseAndSeed()
		{
			using var context = new AppDbContext();
			context.Database.EnsureCreated();

			SeedAdminIfNeeded(context);
			SeedDemoUserIfNeeded(context);
			SeedDemoTrainerIfNeeded(context);

			context.SaveChanges();
		}

		private static void SeedAdminIfNeeded(AppDbContext context)
		{
			if (context.Admins.Any())
				return;

			context.Admins.Add(new Admin
			{
				Name = "Admin",
				Phone = DemoAdminPhone,
				PasswordHash = PasswordHasher.HashPassword(DemoAdminPassword)
			});
		}

		private static void SeedDemoUserIfNeeded(AppDbContext context)
		{
			if (context.Users.Any(u => u.Phone == DemoUserPhone))
				return;

			context.Users.Add(new User
			{
				Name = "Ismet",
				Surname = "Tasdemir",
				Phone = DemoUserPhone,
				Email = "test@example.com",
				BirthYear = 1995,
				HeightCm = 180,
				WeightKg = 75,
				MedicalRecord = "No known allergies",
				PasswordHash = PasswordHasher.HashPassword("user123")
			});
		}

		private static void SeedDemoTrainerIfNeeded(AppDbContext context)
		{
			if (context.Trainers.Any(t => t.Phone == DemoTrainerPhone))
				return;

			context.Trainers.Add(new Trainer
			{
				Name = "Ahmet",
				Surname = "Yilmaz",
				Phone = DemoTrainerPhone,
				Email = "hoca@mail.com",
				BirthYear = 1998,
				IsApproved = true,
				PasswordHash = PasswordHasher.HashPassword("trainer123")
			});
		}
	}
}
