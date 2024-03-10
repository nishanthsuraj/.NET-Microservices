
using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Data.Extensions;
using PlatformService.Data.Implementations;
using PlatformService.Data.Interfaces;

namespace PlatformService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            #region Developer Added Configurations - 1
            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemory"));
            builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();

            // AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            #region Developer Added Configurations - 2
            PrepareDatabase.Seed(app);
            #endregion

            app.MapControllers();

            app.Run();
        }
    }
}
