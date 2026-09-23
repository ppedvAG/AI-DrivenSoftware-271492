using Microsoft.EntityFrameworkCore;
using WEGManagement.Domain.Events;
using WEGManagement.Domain.Models;
using WEGManagement.Infrastructure.Data;

namespace WEGManagement.Infrastructure.Persistance;

public class WegDbContext : DbContext
{
    public WegDbContext(DbContextOptions<WegDbContext> options)
        : base(options)
    {
    }

    public DbSet<Building> Buildings => Set<Building>();
    public DbSet<Apartment> Apartments => Set<Apartment>();
    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<CondoFee> CondoFees => Set<CondoFee>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<RepairOrder> RepairOrders => Set<RepairOrder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WegDbContext).Assembly);

        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.Ignore<CondoFeeDue>();
        modelBuilder.Ignore<PaymentReceived>();
        modelBuilder.Ignore<RepairOrderCompleted>();
        modelBuilder.Ignore<OwnerChanged>();

        Seed.Configure(modelBuilder);
    }
}
