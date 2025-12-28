using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHealthTracker.Data.Entities
{
    public class WorkoutProgramItem
    {
		public int Id { get; set; }
		public int WorkoutProgramId { get; set; }
		public WorkoutProgram WorkoutProgram { get; set; } = null!;

		public int WorkoutId { get; set; }
		public Workout Workout { get; set; } = null!;

		public int DayNo { get; set; }      // always assigns to 1- not used
		public int Sets { get; set; }
		public int Reps { get; set; }
	}
}
