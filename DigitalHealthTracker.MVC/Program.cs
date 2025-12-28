var builder = WebApplication.CreateBuilder(args);

// --- EKSÝK OLAN KISIM BURASI ---
// 1. MVC Servisini ekle
builder.Services.AddControllersWithViews();

// 2. HttpClient Servisini ekle (Bunu unutmuþsun veya silinmiþ)
builder.Services.AddHttpClient("HealthApi", client =>
{
    // API adresin (Bunu appsettings.json'dan da çekebilirsin, þimdilik elle yazalým garanti olsun)
    client.BaseAddress = new Uri("https://localhost:7193/");
});
// -------------------------------

var app = builder.Build();

// ... kodun geri kalaný ayný ...
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
