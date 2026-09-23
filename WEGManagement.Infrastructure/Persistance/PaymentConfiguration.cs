using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WEGManagement.Domain.Models;

namespace WEGManagement.Infrastructure.Persistance;

public class PaymentConfiguration
    : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Reference)
            .HasMaxLength(200);

        builder.Property(x => x.PaymentType)
            .IsRequired();

        builder.Property(x => x.Date)
            .IsRequired();

        builder.HasOne(x => x.CondoFee)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.CondoFeeId);
    }
}
