using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PredictiveMaintenance.Domain.Entities;

namespace PredictiveMaintenance.Infrastructure.Persistence.Configurations
{
    public class MachineConfiguration : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> builder)
        {
            // Primary Key
            builder.HasKey(m => m.Id);

            // SerialNumber: obligatorio y máximo 50 caracteres
            builder.Property(m => m.SerialNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            // Model: obligatorio y máximo 100 caracteres
            builder.Property(m => m.Model)
                   .IsRequired()
                   .HasMaxLength(100);

            // InstallationDate: obligatorio
            builder.Property(m => m.InstallationDate)
                   .IsRequired();
        }
    }
}