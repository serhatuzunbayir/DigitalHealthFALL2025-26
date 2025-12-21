using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DigitalHealthTracker.Data.Entities;

namespace DigitalHealthTracker.Data
{

	public class AppDbContext : DbContext
	{
		// Constructor
		public AppDbContext()
		{
		}

		// Tabloyu temsil eden DbSet
		public DbSet<User> Users { get; set; }
		public DbSet<Trainer> Trainers { get; set; }
		public DbSet<Workout> Workouts { get; set; }
		public DbSet<WorkoutProgram> WorkoutPrograms { get; set; }
		public DbSet<WorkoutProgramItem> WorkoutProgramItems { get; set; }




		// Veritabanı bağlantı ayarı
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Healthtracker.db");
				optionsBuilder.UseSqlite($"Data Source={dbPath}");
			}
		}

		// İleride model ayarları yapmak istersen burayı kullanacağız
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<WorkoutProgram>()
				.HasMany(p => p.Items)
				.WithOne(i => i.WorkoutProgram)
				.HasForeignKey(i => i.WorkoutProgramId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<WorkoutProgram>()
				.HasOne(p => p.Trainer)
				.WithMany()
				.HasForeignKey(p => p.TrainerId)
				.OnDelete(DeleteBehavior.Restrict);
		}


	}
}
