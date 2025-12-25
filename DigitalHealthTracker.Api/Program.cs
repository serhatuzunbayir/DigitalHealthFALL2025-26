using DigitalHealthTracker.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
	var dbPath = DigitalHealthTracker.Data.DbPaths.GetDbFilePath();
	Debug.WriteLine("DB PATH = " + dbPath);
	Console.WriteLine("DB PATH = " + dbPath);

	options.UseSqlite($"Data Source={dbPath}");
});

builder.Services.AddControllers()
	.AddJsonOptions(o =>
	{
		o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
		o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
	});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// MIGRATION YOK: DB’de WorkoutLogs tablosuna AssignedProgramId kolonu yoksa ekle
await EnsureAssignedProgramIdColumnAsync(app.Services);

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();
app.Run();

static async Task EnsureAssignedProgramIdColumnAsync(IServiceProvider services)
{
	using var scope = services.CreateScope();
	var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

	// WorkoutLogs tablosu var mý?
	var conn = ctx.Database.GetDbConnection();
	await conn.OpenAsync();

	await using var cmd = conn.CreateCommand();
	cmd.CommandText = "PRAGMA table_info('WorkoutLogs');";

	bool hasAnyColumn = false;
	bool hasAssignedProgramId = false;

	await using (var reader = await cmd.ExecuteReaderAsync())
	{
		while (await reader.ReadAsync())
		{
			hasAnyColumn = true;
			var colName = reader.GetString(1); // name column
			if (colName == "AssignedProgramId")
			{
				hasAssignedProgramId = true;
				break;
			}
		}
	}

	// WorkoutLogs tablosu yoksa burada bir þey yapmýyoruz (zaten endpoint çalýþýyorsa vardýr)
	if (!hasAnyColumn) return;

	if (!hasAssignedProgramId)
	{
		await ctx.Database.ExecuteSqlRawAsync(
			"ALTER TABLE WorkoutLogs ADD COLUMN AssignedProgramId INTEGER NULL;"
		);
	}
}
