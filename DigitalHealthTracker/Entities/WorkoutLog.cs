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

		public int TrainerId { get; set; }
		public Trainer Trainer { get; set; } = null!;

		public int WorkoutProgramId { get; set; }
		public WorkoutProgram WorkoutProgram { get; set; } = null!;

		public int WorkoutId { get; set; }
		public Workout Workout { get; set; } = null!;

		public int DayNo { get; set; }
		public int Sets { get; set; }
		public int Reps { get; set; }

		public DateTime CompletedAt { get; set; } = DateTime.Now;
	}
}
