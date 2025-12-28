using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using DigitalHealthTracker.Web.Filters;
using DigitalHealthTracker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHealthTracker.Web.Controllers;

[RequireRole("User")]
[Route("User")] // 👈 BU SATIR ÇOK ÖNEMLİ: Tüm linklerin başı "/User" olacak
public class UserController : Controller
{
    // ==========================================
    // 1. DASHBOARD (INDEX)
    // ==========================================
    // Hem "/User" hem "/User/Index" bu sayfayı açar
    [HttpGet("")]
    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId is null) return RedirectToAction("Login", "Account");

        var client = HttpContext.RequestServices
            .GetRequiredService<IHttpClientFactory>()
            .CreateClient("api");

        // 1. Aktif Programı Çek
        var activeList = await client.GetFromJsonAsync<List<ActiveListRowDto>>(
            $"/api/AssignedPrograms/user/{userId}/active-list"
        ) ?? new List<ActiveListRowDto>();

        // 2. Tüm Atamaları Çek
        var rows = await client.GetFromJsonAsync<List<UserAssignedProgramRowVm>>(
            $"/api/AssignedPrograms/user/{userId}"
        ) ?? new List<UserAssignedProgramRowVm>();

        // 3. Sağlık Bilgilerini Çek
        HealthVm? health = null;
        var u = await client.GetFromJsonAsync<ApiUserDto>($"/api/Users/{userId}");

        if (u is not null)
        {
            var heightCm = u.HeightCm ?? 0;
            var weightKg = u.WeightKg ?? 0;

            double bmi = 0;
            if (heightCm > 0 && weightKg > 0)
            {
                var m = heightCm / 100.0;
                bmi = weightKg / (m * m);
            }

            string category = bmi <= 0 ? "-" :
                              bmi < 18.5 ? "Underweight" :
                              bmi < 25.0 ? "Normal" :
                              bmi < 30.0 ? "Overweight" : "Obese";

            double targetWeight = 0;
            double diff = 0;
            if (heightCm > 0)
            {
                var m = heightCm / 100.0;
                targetWeight = 22.0 * (m * m);
                diff = weightKg - targetWeight;
            }

            health = new HealthVm
            {
                HeightCm = heightCm,
                WeightKg = weightKg,
                Bmi = bmi,
                Category = category,
                TargetWeightKg = targetWeight,
                WeightDiffKg = diff
            };
        }

        var vm = new UserDashboardVm
        {
            Health = health,
            AssignedPrograms = rows,
            ActiveAssignments = activeList.Select(x => new UserActiveAssignmentVm
            {
                Id = x.Id,
                WorkoutProgramId = x.WorkoutProgramId,
                ProgramTitle = x.ProgramTitle ?? "",
                TrainerName = x.TrainerName ?? ""
            }).ToList()
        };

        return View(vm);
    }

    // ==========================================
    // 2. PROFILE
    // ==========================================
    [HttpGet("Profile")] // -> /User/Profile
    public async Task<IActionResult> Profile()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId is null) return RedirectToAction("Login", "Account");

        var client = HttpContext.RequestServices.GetRequiredService<IHttpClientFactory>().CreateClient("api");
        var apiUser = await client.GetFromJsonAsync<ApiUserDto>($"/api/Users/{userId.Value}");

        if (apiUser is null) return View(new UserProfileVm { Error = "User data could not be loaded." });

        var vm = new UserProfileVm
        {
            Id = apiUser.Id,
            Name = apiUser.Name ?? "",
            Surname = apiUser.Surname ?? "",
            Phone = apiUser.Phone ?? "",
            Email = apiUser.Email,
            HeightCm = apiUser.HeightCm ?? 0,
            WeightKg = apiUser.WeightKg ?? 0,
            BirthYear = apiUser.BirthYear ?? 0,
            MedicalRecord = apiUser.MedicalRecord
        };
        return View(vm);
    }

    [HttpPost("Profile")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Profile(UserProfileVm vm)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId is null) return RedirectToAction("Login", "Account");

        if (!ModelState.IsValid) return View(vm);

        var client = HttpContext.RequestServices.GetRequiredService<IHttpClientFactory>().CreateClient("api");
        var existing = await client.GetFromJsonAsync<ApiUserDto>($"/api/Users/{userId.Value}");

        if (existing is null) { vm.Error = "User not found"; return View(vm); }

        existing.Name = vm.Name;
        existing.Surname = vm.Surname;
        existing.Phone = vm.Phone;
        existing.Email = vm.Email;
        existing.MedicalRecord = vm.MedicalRecord;
        existing.HeightCm = vm.HeightCm;
        existing.WeightKg = vm.WeightKg;
        existing.BirthYear = vm.BirthYear;

        var resp = await client.PutAsJsonAsync($"/api/Users/{userId.Value}", existing);
        if (!resp.IsSuccessStatusCode) { vm.Error = "Update failed"; return View(vm); }

        TempData["Success"] = "Profile updated.";
        return RedirectToAction("Profile");
    }

    // ==========================================
    // 3. ACTIONS (Approve / Complete)
    // ==========================================

    // BUTONLARIN ÇALIŞMASI İÇİN BU ROUTE'LAR ŞART
    [HttpPost("Approve")] // -> /User/Approve
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ApproveAssignedProgram(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var client = HttpContext.RequestServices.GetRequiredService<IHttpClientFactory>().CreateClient("api");

        var req = new HttpRequestMessage(HttpMethod.Put, $"/api/AssignedPrograms/{id}/approve?userId={userId}")
        {
            Content = new StringContent("{}", Encoding.UTF8, "application/json")
        };

        var resp = await client.SendAsync(req);

        if (resp.IsSuccessStatusCode)
            TempData["Success"] = "Program Approved!";
        else
            TempData["UserMsg"] = "Approval Failed.";

        return RedirectToAction("Index");
    }

    [HttpPost("Complete")] // -> /User/Complete
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CompleteWithLogs(int id)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        var client = HttpContext.RequestServices.GetRequiredService<IHttpClientFactory>().CreateClient("api");

        var req = new HttpRequestMessage(HttpMethod.Put, $"/api/AssignedPrograms/{id}/complete-with-logs?userId={userId}")
        {
            Content = new StringContent("{}", Encoding.UTF8, "application/json")
        };

        var resp = await client.SendAsync(req);

        if (resp.IsSuccessStatusCode)
            TempData["Success"] = "Workout Completed!";
        else
            TempData["UserMsg"] = "Completion Failed.";

        return RedirectToAction("Index");
    }

    // ==========================================
    // YARDIMCI SINIFLAR
    // ==========================================
    private class ApiUserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public double? HeightCm { get; set; }
        public double? WeightKg { get; set; }
        public int? BirthYear { get; set; }
        public string? MedicalRecord { get; set; }
    }

    private sealed class ActiveListRowDto
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("workoutProgramId")] public int WorkoutProgramId { get; set; }
        [JsonPropertyName("programTitle")] public string? ProgramTitle { get; set; }
        [JsonPropertyName("trainerName")] public string? TrainerName { get; set; }
    }
}