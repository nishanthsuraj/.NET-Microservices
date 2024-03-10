using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    internal class AppDbContext : DbContext
    {
        #region Public Properties
        public DbSet<Platform> Platforms { get; set; }
        #endregion

        #region Construction
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions)
            : base(contextOptions)
        {

        }
        #endregion
    }
}
