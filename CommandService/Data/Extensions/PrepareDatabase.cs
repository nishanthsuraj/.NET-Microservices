using Microsoft.EntityFrameworkCore;

namespace CommandService.Data.Extensions
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
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }


            Console.WriteLine("Seeding new platforms...");

        }
    }
}
