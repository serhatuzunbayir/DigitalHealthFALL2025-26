using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHealthTracker.Data.Entities
{
    public class WorkoutProgram
    {

		public int Id { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }

		public int TrainerId { get; set; }
		public Trainer Trainer { get; set; }

		public bool IsApprovedByUser { get; set; }
		public bool IsCompleted { get; set; }
	}
}
