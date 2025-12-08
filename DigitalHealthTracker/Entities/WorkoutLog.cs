using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHealthTracker.Data.Entities
{
    public class WorkoutLog
    {
		public int Id { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }

		public DateTime Date { get; set; }
		public string ExerciseName { get; set; }
		public string Notes { get; set; }

		// Program dışı mı?
		public bool IsExtraExercise { get; set; }
	}
}
