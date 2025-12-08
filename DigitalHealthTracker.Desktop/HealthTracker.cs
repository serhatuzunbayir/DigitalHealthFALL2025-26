using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace DigitalHealthTracker.Desktop
{
	internal static class HealthTracker
	{
		[STAThread]
		static void Main()
		{

			try
			{
				using (var context = new AppDbContext())
				{
					context.Database.EnsureCreated();

					if (!context.Users.Any())
					{
						context.Users.Add(new User
						{
							Name = "ismet",
							Surname = "Tasdemir",
							Phone = "5555555555",
							Email = "test@example.com",
							BirthYear =1995,
							HeightCm = 180,
							WeightKg = 75,
							MedicalRecord = "No known allergies"
						});

						context.SaveChanges();
					}

					if (!context.Trainers.Any())
					{
						context.Trainers.Add(new Trainer
						{
							Name = "Ahmet",
							Surname = "Yilmaz",
							Phone = "5444444444",
							Email = "hoca@mail.com",
							BirthYear = 1998
						});

						context.SaveChanges();

					}
				}
				ApplicationConfiguration.Initialize();
				Application.Run(new MainForm());
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					ex.ToString(),
					"Baþlangýç Hatasý",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}

		}
	}
}

