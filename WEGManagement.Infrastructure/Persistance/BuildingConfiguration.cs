using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEGManagement.Domain.Models;

namespace WEGManagement.Infrastructure.Persistance;

public class BuildingConfiguration
    : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(300);

        builder.HasMany(x => x.Apartments)
            .WithOne(x => x.Building)
            .HasForeignKey(x => x.BuildingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.RepairOrders)
            .WithOne(x => x.Building)
            .HasForeignKey(x => x.BuildingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
