using System;
using System.Security.Cryptography;
using System.Text;

namespace DigitalHealthTracker.Data.Infrastructure
{
	public static class PasswordHasher
	{
		// Basit + stabil bir format:  salt:hash  (Base64)
		public static string HashPassword(string password)
		{
			if (password == null) password = "";

			byte[] salt = RandomNumberGenerator.GetBytes(16);
			byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
				password: Encoding.UTF8.GetBytes(password),
				salt: salt,
				iterations: 100_000,
				hashAlgorithm: HashAlgorithmName.SHA256,
				outputLength: 32
			);

			return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
		}

		public static bool VerifyPassword(string password, string stored)
		{
			if (string.IsNullOrWhiteSpace(stored)) return false;

			var parts = stored.Split(':');
			if (parts.Length != 2) return false;

			var salt = Convert.FromBase64String(parts[0]);
			var expectedHash = Convert.FromBase64String(parts[1]);

			var actualHash = Rfc2898DeriveBytes.Pbkdf2(
				password: Encoding.UTF8.GetBytes(password ?? ""),
				salt: salt,
				iterations: 100_000,
				hashAlgorithm: HashAlgorithmName.SHA256,
				outputLength: expectedHash.Length
			);

			return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
		}
	}
}
