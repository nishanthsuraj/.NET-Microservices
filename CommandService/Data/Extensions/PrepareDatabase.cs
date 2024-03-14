using CommandService.Data.Interfaces;
using CommandService.Models;
using CommandService.SyncDataServices.Grpc;

namespace CommandService.Data.Extensions
{
    public static class PrepareDatabase
    {
        public static void Seed(IApplicationBuilder builder)
        {
            using IServiceScope serviceScope = builder.ApplicationServices.CreateScope();
            IPlatformDataClient? grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
            IEnumerable<Platform> platforms = grpcClient.ReturnAllPlatforms();
            ICommandRepository commandRepository = serviceScope.ServiceProvider.GetService<ICommandRepository>();

            Console.WriteLine("Seeding new platforms...");

            foreach (Platform platform in platforms)
            {
                if (!commandRepository.ExternalPlatformExists(platform.ExternalID))
                {
                    commandRepository.CreatePlatform(platform);
                }
                commandRepository.SaveChanges();
            }
        }
    }
}
