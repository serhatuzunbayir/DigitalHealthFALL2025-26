using DigitalHealthTracker.Web.Models; // Az önce oluşturduğumuz model
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DigitalHealthTracker.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UsersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            // Program.cs'te tanımladığımız "HealthApi" ismini kullanıyoruz
            var client = _httpClientFactory.CreateClient("HealthApi");

            // Şemaya göre API'deki endpoint muhtemelen "api/users" veya "api/user" dır.
            // Burayı API projendeki Controller adına göre teyit etmelisin.
            var response = await client.GetAsync("/api/users");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                // JSON ayarları: Büyük küçük harf duyarlılığını kapatıyoruz ki hata olmasın
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var users = JsonSerializer.Deserialize<List<UserViewModel>>(jsonData, options);
                return View(users);
            }

            // Hata olursa boş liste dönsün, patlamasın.
            return View(new List<UserViewModel>());
        }
    }
}