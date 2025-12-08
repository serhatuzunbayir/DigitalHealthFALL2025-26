using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DigitalHealthTracker.Data.Entities
{
    public class Trainer
    {
		public int Id { get; set; }

		// FR4
		public string Name { get; set; } = string.Empty;
		public string Surname { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;

		public bool IsApproved { get; set; } = false; // Admin onayı

		public int BirthYear { get; set; }

		[NotMapped]
		public int Age
		{
			get
			{
				if (BirthYear <= 0) return 0;
				var year = DateTime.Now.Year;
				var age = year - BirthYear;
				return age < 0 ? 0 : age;
			}
		}
		public ICollection<User> Users { get; set; } = new List<User>();
		public ICollection<WorkoutProgram> Programs { get; set; } = new List<WorkoutProgram>();
	}
}
