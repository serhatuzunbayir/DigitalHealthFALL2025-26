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


		// Veritabanı bağlantı ayarı
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				// SQLite dosyamızın adı: digitalhealth.db
				optionsBuilder.UseSqlite("Data Source=Healthtracker.db");
			}
		}

		// İleride model ayarları yapmak istersen burayı kullanacağız
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Örnek: Gereksiz, ama gösterelim:
			// modelBuilder.Entity<User>().Property(u => u.Name).IsRequired();
		}
	}
}
