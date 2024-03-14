
using CommandService.AsyncDataServices;
using CommandService.Data;
using CommandService.Data.Extensions;
using CommandService.Data.Implementations;
using CommandService.Data.Interfaces;
using CommandService.EventProcessing;
using CommandService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

namespace CommandService
{
    public class Program
    {
        #region Private Constants
        private const string CommandsConnectionString = "Commands";
        #endregion

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Developer Added Configurations - 1
            //// Add services to the container.
            if (!builder.Environment.IsDevelopment())
            {
                builder.WebHost.ConfigureKestrel(options =>
                {
                    options.ListenAnyIP(80);
                });
            }

            Console.WriteLine("--> Using InMemory Db");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemory"));

            builder.Services.AddScoped<ICommandRepository, CommandRepository>();
            builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
            builder.Services.AddHostedService<MessageBusSubscriber>();

            // gRPC
            builder.Services.AddScoped<IPlatformDataClient, PlatformDataClient>();

            // AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            #region Developer Added Configurations - 2
            //PrepareDatabase.Seed(app, app.Environment.IsProduction());
            #endregion

            app.MapControllers();

            app.Run();
        }
    }
}
