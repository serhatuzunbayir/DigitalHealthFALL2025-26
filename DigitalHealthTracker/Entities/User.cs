using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DigitalHealthTracker.Data.Entities
{
    public class User
    {
		public int Id { get; set; }

		// FR1
		public string Name { get; set; } = string.Empty;
		public string Surname { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;

		// FR2
		public int BirthYear { get; set; }
		
		[NotMapped]
		public int Age
		{
			get
			{
				if (BirthYear <= 0) return 0;
				var year = DateTime.Now.Year;
				var age = year - BirthYear;
				return age < 0 ? 0 : age;
			}
		}
		public double HeightCm { get; set; } = 0.0; // boy
		public double WeightKg { get; set; } = 0.0; // kilo
		public string MedicalRecord { get; set; } = string.Empty;

		// FR3 - BMI ve hedef kilo farkı

		[NotMapped]
		public double Bmi {
			get
			{
				if (HeightCm <= 0 || WeightKg <= 0)
				{
					return 0;
				}
					
				var heightM = HeightCm / 100.0;
				var bmi = WeightKg / (heightM * heightM);
				return Math.Round(bmi, 2);  // 2 basamaklı sonuç ver
			}
		}


		[NotMapped]
		public double TargetWeightKg 
		{
			get
			{
				if (HeightCm <= 0)
					return 0;

				const double targetBmi = 22.0; // 18.5–24.9 arasında ideal değer 22
				var heightM = HeightCm / 100.0;
				var targetWeight = targetBmi * heightM * heightM;
				return Math.Round(targetWeight, 1); // 1 basamaklı sonuç ver
			}
		}


		[NotMapped]
		public double WeightDiffKg
		{
			get
			{
				if (WeightKg <= 0)
				{
					return 0;
				}

				var diff = TargetWeightKg - WeightKg;
				return Math.Round(diff, 1);
			}
		}

		[NotMapped]
		public string BmiCategory
		{
			get
			{
				var bmi = Bmi;

				if (bmi == 0) return "Unknown";
				if (bmi < 18.5) return "Underweight";
				if (bmi < 25.0) return "Normal";
				if (bmi < 30.0) return "Overweight";
				return "Obese";
			}
		}

		// Navigation
		/*public int? TrainerId { get; set; }
		public Trainer Trainer { get; set; }

		public ICollection<WorkoutProgram> Programs { get; set; }
		public ICollection<WorkoutLog> WorkoutLogs { get; set; }*/
	}
}
