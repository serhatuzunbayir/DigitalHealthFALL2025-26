using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using DigitalHealthTracker.Data.Infrastructure;
using DigitalHealthTracker.Desktop.RegistrationAndLogin;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DigitalHealthTracker.Desktop
{
	internal static class HealthTracker
	{
		//API Display Address: https://localhost:7193/Swagger

		[STAThread]
		static void Main()
		{
			try
			{
				//MessageBox.Show("DESKTOP DB PATH = " + DbPath.GetDbFilePath(),"DB PATH",MessageBoxButtons.OK,MessageBoxIcon.Information);


				ApplicationConfiguration.Initialize();
				Application.Run(new LoginForm());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Startup Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		
	}
}


