using System;
using System.IO;

namespace DigitalHealthTracker.Data
{
	public static class DbPaths
	{
		public static string GetDbFilePath()
		{
			var folder = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
				"DigitalHealthTracker"
			);

			Directory.CreateDirectory(folder);
			return Path.Combine(folder, "Healthtracker.db");
		}
	}
}
