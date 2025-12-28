using System;
using System.Collections.Generic;
using System.Text;


namespace DigitalHealthTracker.Data.Entities
{
	public class AssignedProgram
    {
		public int Id { get; set; }
		public int UserId { get; set; }
		public User User { get; set; } = null!;
		public int TrainerId { get; set; }
		public Trainer Trainer { get; set; } = null!;
		public int WorkoutProgramId { get; set; }
		public WorkoutProgram WorkoutProgram { get; set; } = null!;

		public AssignmentStatus Status { get; set; } = AssignmentStatus.Pending;
		public DateTime AssignedAt { get; set; } = DateTime.Now;
		public DateTime? ApprovedAt { get; set; }
	}
}
