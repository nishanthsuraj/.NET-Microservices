using PlatformService.Models;

namespace PlatformService.Data.Interfaces
{
    public interface IPlatformRepository
    {
        void CreatePlatform(Platform platform);

        IEnumerable<Platform> GetAllPlatforms();

        Platform GetPlatformById(int id);

        bool SaveChanges();
    }
}
