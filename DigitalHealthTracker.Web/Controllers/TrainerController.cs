using System.Net.Http.Json;
using DigitalHealthTracker.Web.Filters;
using DigitalHealthTracker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHealthTracker.Web.Controllers;

[RequireRole("Trainer")]
public class TrainerController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public TrainerController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // =============================================
    // 1. DASHBOARD (INDEX)
    // =============================================
    public async Task<IActionResult> Index()
    {
        var trainerId = HttpContext.Session.GetInt32("UserId");
        if (trainerId is null) return RedirectToAction("Login", "Account");

        var client = _httpClientFactory.CreateClient("api");

        // Kullanıcı Listesini Getir
        var list = await client.GetFromJsonAsync<List<UserLookupVm>>(
            $"/api/Users/assigned-to-trainer/{trainerId}"
        ) ?? new List<UserLookupVm>();

        return View(list);
    }

    // =============================================
    // 2. ASSIGN SAYFASI (GET)
    // =============================================
    [HttpGet]
    public async Task<IActionResult> Assign()
    {
        var trainerId = HttpContext.Session.GetInt32("UserId");
        if (trainerId is null) return RedirectToAction("Login", "Account");

        // Sayfayı dolduracak verileri çekiyoruz
        var vm = await LoadAssignVm(trainerId.Value);

        return View(vm);
    }

    // =============================================
    // 3. ASSIGN İŞLEMİ (POST)
    // =============================================
    // =============================================
    // 3. ASSIGN İŞLEMİ (POST)
    // =============================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Assign(AssignProgramVm model)
    {
        var trainerId = HttpContext.Session.GetInt32("UserId");
        if (trainerId is null) return RedirectToAction("Login", "Account");

        var client = _httpClientFactory.CreateClient("api");

        // 🛠️ DÜZELTME BURADA: payload içine 'trainerId' EKLEDİK
        var payload = new
        {
            userId = model.UserId,
            workoutProgramId = model.WorkoutProgramId,
            trainerId = trainerId.Value // <-- EKSİK OLAN BUYDU!
        };

        var response = await client.PostAsJsonAsync("/api/AssignedPrograms", payload);

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Program assigned successfully!";
            return RedirectToAction("Index");
        }

        // Hata olursa sayfayı tekrar doldurup gösteriyoruz
        var vm = await LoadAssignVm(trainerId.Value);

        // Hatanın detayını görmek için response body'yi okuyalım
        var errorBody = await response.Content.ReadAsStringAsync();
        vm.Error = $"Assignment failed. HTTP {(int)response.StatusCode}. Details: {errorBody}";

        return View(vm);
    }

    // =============================================
    // YARDIMCI METOT: Dropdownları Doldurur
    // =============================================
    // =============================================
    // YARDIMCI METOT: Dropdownları Doldurur
    // =============================================
    private async Task<AssignProgramVm> LoadAssignVm(int trainerId)
    {
        var client = _httpClientFactory.CreateClient("api");

        // 🔴 ESKİSİ: Sadece bu eğitmene zimmetli olanları getiriyordu
        // var usersTask = client.GetFromJsonAsync<List<UserLookupVm>>($"/api/Users/assigned-to-trainer/{trainerId}");

        // 🟢 YENİSİ: Sistemdeki TÜM "User" rolündekileri getirecek
        // (Not: Aşağıdaki 2. adımda API tarafına bu özelliği ekleyeceğiz)
        var usersTask = client.GetFromJsonAsync<List<UserLookupVm>>("/api/Users");

        // 2. Programları çek
        var programsTask = client.GetFromJsonAsync<List<ProgramPickVm>>(
            $"/api/WorkoutPrograms/trainer/{trainerId}");

        await Task.WhenAll(usersTask, programsTask);

        var users = await usersTask ?? new List<UserLookupVm>();
        var programs = await programsTask ?? new List<ProgramPickVm>();

        return new AssignProgramVm
        {
            Users = users,
            Programs = programs
        };
    }
}