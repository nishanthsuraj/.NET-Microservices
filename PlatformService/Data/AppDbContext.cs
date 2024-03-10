using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    internal class AppDbContext : DbContext
    {
        #region Protected Properties
        public DbSet<Platform> platforms { get; set; }
        #endregion

        #region Construction
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions)
            : base(contextOptions)
        {

        }
        #endregion
    }
}
