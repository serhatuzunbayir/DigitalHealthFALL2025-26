using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHealthTracker.Desktop
{
    public static class Session
    {
		public static AppRole Role { get; set; } = AppRole.None;

		public static int? AdminId { get; set; }
		public static int? TrainerId { get; set; }
		public static int? UserId { get; set; }

		public static string DisplayName { get; set; } = "";

		public static void Clear()
		{
			Role = AppRole.None;
			AdminId = null;
			TrainerId = null;
			UserId = null;
			DisplayName = "";
		}
	}
}
