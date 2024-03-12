using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data.Extensions
{
    public static class PrepareDatabase
    {
        public static void Seed(IApplicationBuilder builder, bool isProd)
        {
            using IServiceScope serviceScope = builder.ApplicationServices.CreateScope();
            AppDbContext? context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            if (isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context?.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            if (context != null && !context.Platforms.Any())
            {
                context.Platforms.AddRange(
                    new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" });

                context.SaveChanges();
            }
            else
            {
                // Do nothing, We already have data.
            }
        }
    }
}
