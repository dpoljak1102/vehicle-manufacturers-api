using Microsoft.EntityFrameworkCore;
using VehicleManufacturers.DAL.Entities;

namespace VehicleManufacturers.DAL
{
    public class VehicleManufacturersContext : DbContext
    {
        public VehicleManufacturersContext(DbContextOptions<VehicleManufacturersContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Check if optionsBuilder is already configured
            if (!optionsBuilder.IsConfigured)
            {
                // Set up SQL Server connection string
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=VehicleManufacturersDB;Integrated Security=True",
                    b => b.MigrationsAssembly("VehicleManufacturers.WebApi"));

                // Configuration for query tracking behavior
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }

        // DbSet for VehicleMakeEntity table
        public DbSet<VehicleMakeEntity> VehicleMakes { get; set; }

        // DbSet for VehicleModelEntity table
        public DbSet<VehicleModelEntity> VehicleModels { get; set; }
    }
}