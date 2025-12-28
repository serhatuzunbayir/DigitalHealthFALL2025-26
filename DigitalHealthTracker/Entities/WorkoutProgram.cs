using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHealthTracker.Data.Entities
{
    public class WorkoutProgram
    {
		public int Id { get; set; }
		public string Title { get; set; } = "";
		public int TrainerId { get; set; }
		public Trainer Trainer { get; set; } = null!;
		public List<WorkoutProgramItem> Items { get; set; } = new List<WorkoutProgramItem>();
	}
}
