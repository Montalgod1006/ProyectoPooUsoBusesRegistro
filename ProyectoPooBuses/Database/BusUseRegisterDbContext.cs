using Microsoft.EntityFrameworkCore;
using ProyectoPooBuses.Entities;

namespace ProyectoPooBuses.Database
{
    public class BusUseRegisterDbContext : DbContext
    {
        public BusUseRegisterDbContext (DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<BusRegisterEntity> Buses { get; set; }
        public DbSet<RouteRegisterEntity> Routes { get; set; }
        public DbSet<TotalDailyStatisticsEntity> Dailies { get; set; }
        
    }
}