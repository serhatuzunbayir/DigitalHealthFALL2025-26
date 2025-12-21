using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHealthTracker.Data.Entities
{
    public class WorkoutLog
    {
		public int Id { get; set; }
		public int UserId { get; set; }
		public User User { get; set; } = null!;
		public int WorkoutId { get; set; }
		public Workout Workout { get; set; } = null!;

		public DateTime LogDate { get; set; } = DateTime.Now;
		public bool Completed { get; set; }
		public string Note { get; set; } = "";
	}
}
