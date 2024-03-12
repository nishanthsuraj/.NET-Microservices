
using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Data.Extensions;
using PlatformService.Data.Implementations;
using PlatformService.Data.Interfaces;
using PlatformService.SyncDataServices.Http;

namespace PlatformService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            #region Developer Added Configurations - 1
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(80);
            });

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemory"));
            builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();

            // AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Gives HttpClient object to HttpCommandDataClient ctor. 
            builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
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

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            #region Developer Added Configurations - 2
            PrepareDatabase.Seed(app);
            Console.WriteLine($"--> CommandService Endpoint {app.Configuration["CommandService"]}");
            #endregion

            app.MapControllers();

            app.Run();
        }
    }
}
