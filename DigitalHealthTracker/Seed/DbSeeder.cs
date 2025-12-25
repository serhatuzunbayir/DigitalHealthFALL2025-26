using DigitalHealthTracker.Data.Entities;
using DigitalHealthTracker.Data.Infrastructure;

namespace DigitalHealthTracker.Data.Seed
{
	public static class DbSeeder
	{
		public static void Seed(AppDbContext db)
		{
			// ADMIN
			if (!db.Admins.Any(a => a.Phone == "5550000000"))
			{
				db.Admins.Add(new Admin
				{
					Name = "Admin",
					Phone = "5550000000",
					PasswordHash = PasswordHasher.HashPassword("admin")
				});
			}

			// DEMO USER
			if (!db.Users.Any(u => u.Phone == "5555555555"))
			{
				db.Users.Add(new User
				{
					Name = "Ayhan",
					Surname = "Tasdemir",
					Phone = "5555555555",
					Email = "test@example.com",
					BirthYear = 1995,
					HeightCm = 180,
					WeightKg = 75,
					MedicalRecord = "No known allergies",
					PasswordHash = PasswordHasher.HashPassword("ayhan")
				});
			}

			// DEMO TRAINER
			if (!db.Trainers.Any(t => t.Phone == "1111111111"))
			{
				db.Trainers.Add(new Trainer
				{
					Name = "kağan",
					Surname = "özçelik",
					Phone = "1111111111",
					Email = "hoca@mail.com",
					BirthYear = 1998,
					IsApproved = true,
					PasswordHash = PasswordHasher.HashPassword("kagan")
				});
			}

			db.SaveChanges();
		}
	}
}
