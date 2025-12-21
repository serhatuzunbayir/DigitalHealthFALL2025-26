using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHealthTracker.Desktop
{
	public enum AppRole { Admin, Trainer, User }

	public static class Session
	{
		public static AppRole Role { get; set; }
		public static int? CurrentUserId { get; set; }
		public static int? CurrentTrainerId { get; set; }

		public static void Clear()
		{
			Role = default;
			CurrentUserId = null;
			CurrentTrainerId = null;
		}
	}

}
