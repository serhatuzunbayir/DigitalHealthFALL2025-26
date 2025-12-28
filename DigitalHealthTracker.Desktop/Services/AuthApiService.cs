using System;
using System.Net;
using System.Net.Http.Json;
using DigitalHealthTracker.Contracts.Auth;


namespace DigitalHealthTracker.Desktop.Services
{
	public class AuthApiService
	{
		// if API is offline, returns false due to handled exception
		public async Task<bool> PingAsync()
		{
			try
			{
				using var client = ApiClient.Create();
				var resp = await client.GetAsync("/api/Health");
				return resp.IsSuccessStatusCode;
			}
			catch
			{
				return false;
			}
		}

		public async Task<LoginResponseDto> LoginAsync(LoginRequestDto req)
		{
			using var client = ApiClient.Create();
			var resp = await client.PostAsJsonAsync("/api/Auth/login", req);

			if (resp.StatusCode == HttpStatusCode.Unauthorized)
				throw new Exception("Invalid credentials.");

			if (resp.StatusCode == HttpStatusCode.Forbidden)
				throw new Exception("Trainer account is not approved yet.");

			if (!resp.IsSuccessStatusCode)
			{
				var body = await resp.Content.ReadAsStringAsync();
				throw new Exception($"Login failed: {(int)resp.StatusCode} {resp.ReasonPhrase}\n{body}");
			}

			var data = await resp.Content.ReadFromJsonAsync<LoginResponseDto>();
			return data!;
		}

		public async Task RegisterUserAsync(RegisterUserRequestDto req)
		{
			using var client = ApiClient.Create();
			var resp = await client.PostAsJsonAsync("/api/Auth/register-user", req);

			if (resp.StatusCode == HttpStatusCode.Conflict)
				throw new Exception("This phone number is already registered.");

			if (!resp.IsSuccessStatusCode)
			{
				var body = await resp.Content.ReadAsStringAsync();
				throw new Exception($"User registration failed: {(int)resp.StatusCode} {resp.ReasonPhrase}\n{body}");
			}
		}

		public async Task RegisterTrainerAsync(RegisterTrainerRequestDto req)
		{
			using var client = ApiClient.Create();
			var resp = await client.PostAsJsonAsync("/api/Auth/register-trainer", req);

			if (resp.StatusCode == HttpStatusCode.Conflict)
				throw new Exception("This phone number is already registered.");

			if (!resp.IsSuccessStatusCode)
			{
				var body = await resp.Content.ReadAsStringAsync();
				throw new Exception($"Trainer registration failed: {(int)resp.StatusCode} {resp.ReasonPhrase}\n{body}");
			}
		}
	}
}
