using DigitalHealthTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DigitalHealthTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        // Dependency Injection ile istemci fabrikasýný alýyoruz
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            // Program.cs'te tanýmladýðýn "HealthApi" ayarýný kullan
            var client = _httpClientFactory.CreateClient("HealthApi");

            // Þemaya göre API muhtemelen "/api/users" adresinden veriyi veriyordur.
            // Eðer API'de controller adý farklýysa burayý güncelle.
            var response = await client.GetAsync("/api/users");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // user_name veya User_Name fark etmez
                };

                // Gelen JSON verisini User listesine çevir
                var users = JsonSerializer.Deserialize<List<UserViewModel>>(jsonData, options);

                return View(users);
            }

            // Baðlantý hatasý olursa boþ liste ile sayfayý aç, patlamasýn
            return View(new List<UserViewModel>());
        }
    }
}