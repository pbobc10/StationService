using Microsoft.EntityFrameworkCore;
using StationService.Models;

namespace StationService.Data
{
    public class StationeServiceContext : DbContext
    {
        public StationeServiceContext(DbContextOptions<StationeServiceContext> options):base(options) { }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<DispensingUnit> DispensingUnits { get; set; }
        public DbSet<FuelPipe> FuelPipes { get; set; }
        public DbSet<FuelQuantity> FuelQuantities { get; set; }
        public DbSet<GasMeter> GasMeters { get; set; }
        public DbSet<GasStation> GasStations { get; set; }
        public DbSet<GasStationAttendant> GasStationAttendants { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            
            // Configure one-to-many relationships
            modelBuilder.Entity<Administrator>()
                .HasMany(a => a.Stations)
                .WithOne(s => s.Administrator)
                .HasForeignKey(s => s.AdministratorId);

            modelBuilder.Entity<GasStation>()
                .HasMany(s => s.GasStationAttendants)
                .WithOne(a => a.GasStation)
                .HasForeignKey(a => a.GasStationId);

            modelBuilder.Entity<GasStation>()
                .HasMany(s => s.DispensingUnits)
                .WithOne(d => d.GasStation)
                .HasForeignKey(d => d.GasStationId);

            // Configure one-to-one relationship between GasStation and Supervisor
            modelBuilder.Entity<GasStation>()
                .HasOne(g => g.Supervisor)
                .WithOne(s => s.Station)
                .HasForeignKey<Supervisor>(s => s.GasStationId); // ForeignKey for Supervisor

            modelBuilder.Entity<DispensingUnit>()
                .HasMany(d => d.Pipes)
                .WithOne(p => p.DispensingUnit)
                .HasForeignKey(p => p.DispensingUnitId);

            // Configure one-to-one relationships
            modelBuilder.Entity<FuelPipe>()
                .HasOne(p => p.Meter)
                .WithOne(m => m.FuelPipe)
                .HasForeignKey<GasMeter>(m => m.FuelPipeId);

            modelBuilder.Entity<Assignment>()
                .HasMany(a => a.FuelQuantities)
                .WithOne(fq => fq.Assignment)
                .HasForeignKey(fq => fq.AssignmentId);
            
        }
    }
}
