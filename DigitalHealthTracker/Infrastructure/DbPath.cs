using System;
using System.IO;

namespace DigitalHealthTracker.Data.Infrastructure
{
	public static class DbPath
	{
		public static string GetDbFolder()
		{
			// baseDir: ...\_run\Debug\net10.0\   veya   ...\_run\Debug\net10.0-windows\
			var baseDir = AppContext.BaseDirectory;

			// Bir üst klasöre çık: ...\_run\Debug\
			var debugDir = Path.GetFullPath(Path.Combine(baseDir, ".."));

			// Ortak DB klasörü: ...\_run\Debug\App_Data
			var folder = Path.Combine(debugDir, "App_Data");
			Directory.CreateDirectory(folder);
			return folder;
		}

		public static string GetDbFilePath()
		{
			return Path.Combine(GetDbFolder(), "healthtracker.db");
		}
	}
}
