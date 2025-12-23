using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DigitalHealthTracker.Data
{
	public static class PasswordHasher
	{
		private const int SaltSize = 16;   // 128 bit
		private const int KeySize = 32;    // 256 bit
		private const int Iterations = 10000;

		public static string HashPassword(string password)
		{
			using var algorithm = new Rfc2898DeriveBytes(password,SaltSize,Iterations,HashAlgorithmName.SHA256);
			var salt = Convert.ToBase64String(algorithm.Salt);
			var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));

			return $"{Iterations}.{salt}.{key}";
		}

		public static bool VerifyPassword(string password, string storedHash)
		{
			var parts = storedHash.Split('.', 3);
			if (parts.Length != 3)
				return false;

			var iterations = int.Parse(parts[0]);
			var salt = Convert.FromBase64String(parts[1]);
			var key = parts[2];

			using var algorithm = new Rfc2898DeriveBytes(
				password,
				salt,
				iterations,
				HashAlgorithmName.SHA256);

			var keyToCheck = Convert.ToBase64String(algorithm.GetBytes(KeySize));

			return keyToCheck == key;
		}
	}
}
