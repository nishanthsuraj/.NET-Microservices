using PlatformService.Data.Interfaces;
using PlatformService.Models;

namespace PlatformService.Data.Implementations
{
    internal class PlatformRepository : IPlatformRepository
    {
        #region Private Readonly Fields
        private readonly AppDbContext _context;
        #endregion

        #region Constructor
        public PlatformRepository(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region IPlatformRepository Implementation
        public void CreatePlatform(Platform platform)
        {
            ArgumentNullException.ThrowIfNull(platform);

            _context.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            if (id < 1)
                throw new ArgumentException(nameof(id));

            return _context.Platforms.SingleOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
        #endregion
    }
}
