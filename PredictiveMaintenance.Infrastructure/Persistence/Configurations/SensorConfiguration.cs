using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PredictiveMaintenance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PredictiveMaintenance.Infrastructure.Persistence.Configurations
{
    public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            // Primary Key
            builder.HasKey(m => m.Id);

            builder.Property(m => m.MachineId)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(m => m.Type)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(m => m.Unit)
                .IsRequired();
        }
    }
}
