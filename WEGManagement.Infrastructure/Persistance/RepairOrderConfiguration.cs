using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEGManagement.Domain.Models;

namespace WEGManagement.Infrastructure.Persistance;

public class RepairOrderConfiguration
    : IEntityTypeConfiguration<RepairOrder>
{
    public void Configure(EntityTypeBuilder<RepairOrder> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Cost)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Date)
            .IsRequired();
    }
}