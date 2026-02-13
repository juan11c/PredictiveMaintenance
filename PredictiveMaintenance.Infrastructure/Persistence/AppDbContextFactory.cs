using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PredictiveMaintenance.Infrastructure.Persistence;

namespace PredictiveMaintenance.Infrastructure.Persistence;
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Aquí pones tu cadena de conexión real a SQL Server
        optionsBuilder.UseSqlServer(
            "Server=localhost\\SQLEXPRESS;Database=PredictiveMaintenanceDb;Trusted_Connection=True;TrustServerCertificate=True");

        return new AppDbContext(optionsBuilder.Options);
    }
}