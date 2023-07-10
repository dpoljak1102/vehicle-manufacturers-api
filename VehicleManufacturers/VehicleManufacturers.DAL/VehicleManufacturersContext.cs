using Microsoft.EntityFrameworkCore;
using VehicleManufacturers.DAL.Entities;

namespace VehicleManufacturers.DAL
{
    public class VehicleManufacturersContext : DbContext
    {
        public VehicleManufacturersContext(DbContextOptions<VehicleManufacturersContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=VehicleManufacturersDB;Integrated Security=True",
                    b => b.MigrationsAssembly("VehicleManufacturers.WebApi"));

                // Configuration for query tracking behavior
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }

        public DbSet<VehicleMakeEntity> VehicleMakes { get; set; }
        public DbSet<VehicleModelEntity> VehicleModels { get; set; }
    }
}