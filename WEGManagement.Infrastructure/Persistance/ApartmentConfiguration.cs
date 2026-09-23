using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEGManagement.Domain.Models;

namespace WEGManagement.Infrastructure.Persistance;

public class ApartmentConfiguration
    : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Size)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.NumberOfResidents)
            .IsRequired();

        builder.HasOne(x => x.Owner)
            .WithMany(x => x.Apartments)
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Building)
            .WithMany(x => x.Apartments)
            .HasForeignKey(x => x.BuildingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.CondoFees)
            .WithOne(x => x.Apartment)
            .HasForeignKey(x => x.ApartmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}