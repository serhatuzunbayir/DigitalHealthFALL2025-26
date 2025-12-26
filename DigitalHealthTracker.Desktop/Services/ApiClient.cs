
using System;
using System.Net.Http;

namespace DigitalHealthTracker.Desktop.Services
{
	public static class ApiClient
	{
		private static readonly string BaseUrl = "https://localhost:7193"; //https://localhost:7193/Swagger

		public static HttpClient Create()
		{
			var client = new HttpClient();
			client.BaseAddress = new Uri(BaseUrl);
			return client;
		}
	}
}

