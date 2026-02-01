using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Domain.Entities;
using PredictiveMaintenance.Infrastructure.Persistence.Configurations;

namespace PredictiveMaintenance.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<SensorData> SensorData { get; set; }
        public DbSet<MaintenanceAlert> MaintenanceAlerts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Machine
            modelBuilder.Entity<Machine>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Machine>()
                .HasIndex(m => m.SerialNumber)
                .IsUnique();

            // Configuración de SensorData
            modelBuilder.Entity<SensorData>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<SensorData>()
                .HasOne<Machine>()
                .WithMany()
                .HasForeignKey(s => s.MachineId);

            // Configuración de MaintenanceAlert
            modelBuilder.Entity<MaintenanceAlert>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<MaintenanceAlert>()
                .HasOne<Machine>()
                .WithMany()
                .HasForeignKey(a => a.MachineId);

            // Aplicando la configuración de Machine
            modelBuilder.ApplyConfiguration(new MachineConfiguration());
        }
    }
}