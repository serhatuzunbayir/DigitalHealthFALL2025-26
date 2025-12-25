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
		public AppDbContext() { }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		// Tabloyu temsil eden DbSet
		public DbSet<User> Users { get; set; }
		public DbSet<Trainer> Trainers { get; set; }
		public DbSet<Workout> Workouts { get; set; }
		public DbSet<WorkoutProgram> WorkoutPrograms { get; set; }
		public DbSet<WorkoutProgramItem> WorkoutProgramItems { get; set; }
		public DbSet<AssignedProgram> AssignedPrograms { get; set; }
		public DbSet<WorkoutLog> WorkoutLogs { get; set; }
		public DbSet<Admin> Admins { get; set; }



		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (optionsBuilder.IsConfigured) return;

			var dbPath = DbPaths.GetDbFilePath();
			optionsBuilder.UseSqlite($"Data Source={dbPath}");
		}




		//Relations(Keys - FKeys- Relation Counts)
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			//Relation on WorkoutProgram-WorkoutProgramItem
			modelBuilder.Entity<WorkoutProgram>()
				.HasMany(p => p.Items)
				.WithOne(i => i.WorkoutProgram)
				.HasForeignKey(i => i.WorkoutProgramId)
				.OnDelete(DeleteBehavior.Cascade);
			// Relation on WorkoutProgram-Trainer
			modelBuilder.Entity<WorkoutProgram>()
				.HasOne(p => p.Trainer)
				.WithMany()
				.HasForeignKey(p => p.TrainerId)
				.OnDelete(DeleteBehavior.Restrict);

			// ***Relations on AssignedProgram***
			modelBuilder.Entity<AssignedProgram>()
				.HasOne(a => a.Trainer)
				.WithMany()
				.HasForeignKey(a => a.TrainerId)
				.OnDelete(DeleteBehavior.Restrict);
			//-User
			modelBuilder.Entity<AssignedProgram>()
				.HasOne(a => a.User)
				.WithMany()
				.HasForeignKey(a => a.UserId)
				.OnDelete(DeleteBehavior.Restrict);
			//-WorkoutProgram
			modelBuilder.Entity<AssignedProgram>()
				.HasOne(a => a.WorkoutProgram)
				.WithMany()
				.HasForeignKey(a => a.WorkoutProgramId)
				.OnDelete(DeleteBehavior.Restrict);
			//Relations on WorkoutLog
			modelBuilder.Entity<WorkoutLog>()
				.HasOne(l => l.User)
				.WithMany()
				.HasForeignKey(l => l.UserId)
				.OnDelete(DeleteBehavior.Restrict);
			//-Trainer
			modelBuilder.Entity<WorkoutLog>()
				.HasOne(l => l.Trainer)
				.WithMany()
				.HasForeignKey(l => l.TrainerId)
				.OnDelete(DeleteBehavior.Restrict);
			//-WorkoutProgram
			modelBuilder.Entity<WorkoutLog>()
				.HasOne(l => l.WorkoutProgram)
				.WithMany()
				.HasForeignKey(l => l.WorkoutProgramId)
				.OnDelete(DeleteBehavior.Restrict);
			//-Workout
			modelBuilder.Entity<WorkoutLog>()
				.HasOne(l => l.Workout)
				.WithMany()
				.HasForeignKey(l => l.WorkoutId)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<WorkoutLog>()
				.HasOne(l => l.AssignedProgram)
				.WithMany()
				.HasForeignKey(l => l.AssignedProgramId)
				.OnDelete(DeleteBehavior.Cascade);

		}


	}
}
