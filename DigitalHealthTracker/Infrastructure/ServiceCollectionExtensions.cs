using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalHealthTracker.Data.Infrastructure
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDigitalHealthTrackerDb(this IServiceCollection services)
		{
			var dbPath = DbPath.GetDbFilePath();
			var cs = $"Data Source={dbPath}";
			services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(cs));
			return services;
		}
	}
}
