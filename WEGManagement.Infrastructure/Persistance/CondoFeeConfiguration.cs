using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEGManagement.Domain.Models;

namespace WEGManagement.Infrastructure.Persistance;

public class CondoFeeConfiguration
    : IEntityTypeConfiguration<CondoFee>
{
    public void Configure(EntityTypeBuilder<CondoFee> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.PaymentStatus)
            .IsRequired();

        builder.Property(x => x.DueDate)
            .IsRequired();

        builder.HasOne(x => x.Apartment)
            .WithMany(x => x.CondoFees)
            .HasForeignKey(x => x.ApartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Payments)
            .WithOne(x => x.CondoFee)
            .HasForeignKey(x => x.CondoFeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
