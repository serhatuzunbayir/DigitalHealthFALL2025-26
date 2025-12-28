using System.Net.Http.Json;
using DigitalHealthTracker.Web.Filters;
using DigitalHealthTracker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHealthTracker.Web.Controllers;

public class RoleController : Controller
{
    // =========================
    // ADMIN
    // =========================
    [RequireRole("Admin")]
    [HttpGet("/Admin")]
    public async Task<IActionResult> Admin()
    {
        var client = HttpContext.RequestServices
            .GetRequiredService<IHttpClientFactory>()
            .CreateClient("api");

        var list = await client.GetFromJsonAsync<List<TrainerPendingVm>>("/api/Trainers/pending")
                   ?? new List<TrainerPendingVm>();

        return View(list);
    }

    [RequireRole("Admin")]
    [HttpPost("/Admin/Approve")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ApproveTrainer(int id)
    {
        var client = HttpContext.RequestServices
            .GetRequiredService<IHttpClientFactory>()
            .CreateClient("api");

        var resp = await client.PutAsync($"/api/Trainers/{id}/approve", null);

        TempData["Msg"] = resp.IsSuccessStatusCode
            ? "Trainer approved."
            : $"Approve failed ({(int)resp.StatusCode}).";

        return Redirect("/Admin");
    }

    // =========================
    // TRAINER
    // =========================
    [RequireRole("Trainer")]
    // [HttpGet("/Trainer")] <-- Artık redirect ediyoruz
    public IActionResult Trainer()
    {
        return RedirectToAction("Index", "Trainer");
    }

    // =========================
    // USER KISMI SİLİNDİ 🗑️
    // =========================
    // Artık User işlemleri "UserController.cs" içinde yapılıyor.
    // Buradaki eski kodları sildik ki sistem karışmasın.
}