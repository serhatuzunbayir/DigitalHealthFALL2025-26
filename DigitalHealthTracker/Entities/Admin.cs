using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHealthTracker.Data.Entities
{
    public class Admin
    {
		public int Id { get; set; }
		public string Name { get; set; } = "Admin";
		public string Phone { get; set; } = "";
		public string PasswordHash { get; set; } = "";
	}
}
