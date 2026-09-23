using Microsoft.EntityFrameworkCore;
using WEGManagement.Domain.Enums;
using WEGManagement.Domain.Models;

namespace WEGManagement.Infrastructure.Data;

/// <summary>
/// Reproduzierbare Demodaten für die WEG-Verwaltung.
///
/// Die Daten werden über EF Core HasData in Migrationen übernommen.
/// Dadurch werden sie beim Anwenden der Initial-/Folgemigrationen
/// automatisch in die Datenbank geschrieben bzw. aktualisiert.
///
/// Die festen IDs sind außerdem für Integrationstests verwendbar.
/// </summary>
public static class Seed
{
    // ============================================================
    // Feste IDs
    // ============================================================

    public static readonly Guid BuildingId =
        Guid.Parse("10000000-0000-0000-0000-000000000001");

    public static readonly Guid ApartmentBugsId =
        Guid.Parse("20000000-0000-0000-0000-000000000001");

    public static readonly Guid ApartmentLeelaId =
        Guid.Parse("20000000-0000-0000-0000-000000000002");

    public static readonly Guid OwnerBugsId =
        Guid.Parse("30000000-0000-0000-0000-000000000001");

    public static readonly Guid OwnerLeelaId =
        Guid.Parse("30000000-0000-0000-0000-000000000002");

    public static readonly Guid CondoFeeBugsAugustId =
        Guid.Parse("40000000-0000-0000-0000-000000000001");

    public static readonly Guid CondoFeeBugsSeptemberId =
        Guid.Parse("40000000-0000-0000-0000-000000000002");

    public static readonly Guid CondoFeeLeelaSeptemberId =
        Guid.Parse("40000000-0000-0000-0000-000000000003");

    public static readonly Guid PaymentBugsAugustId =
        Guid.Parse("50000000-0000-0000-0000-000000000001");

    public static readonly Guid RepairOrderRoofId =
        Guid.Parse("60000000-0000-0000-0000-000000000001");

    public static readonly Guid RepairOrderElevatorId =
        Guid.Parse("60000000-0000-0000-0000-000000000002");

    public static readonly Guid RepairOrderHeatingId =
        Guid.Parse("60000000-0000-0000-0000-000000000003");

    /// <summary>
    /// Registriert die Demodaten im EF-Core-Modell.
    /// In WegDbContext.OnModelCreating aufrufen.
    /// </summary>
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Building>().HasData(
            new
            {
                Id = BuildingId,
                Address = "742 Evergreen Terrace, Springfield"
            });

        modelBuilder.Entity<Owner>().HasData(
            new
            {
                Id = OwnerBugsId,
                Name = "Bugs Bunny",
                Contact = "bugs.bunny@example.test",
                BankConnection = "DEMO-BANK-0001"
            },
            new
            {
                Id = OwnerLeelaId,
                Name = "Turanga Leela",
                Contact = "leela@example.test",
                BankConnection = "DEMO-BANK-0002"
            });

        modelBuilder.Entity<Apartment>().HasData(
            new
            {
                Id = ApartmentBugsId,
                Address = "742 Evergreen Terrace, Wohnung 1",
                Size = 82.5f,
                Floor = 1,
                Status = ApartmentStatus.Rented,
                NumberOfResidents = 2,
                OwnerId = OwnerBugsId,
                BuildingId = BuildingId
            },
            new
            {
                Id = ApartmentLeelaId,
                Address = "742 Evergreen Terrace, Wohnung 2",
                Size = 96.0f,
                Floor = 2,
                Status = ApartmentStatus.Sold,
                NumberOfResidents = 1,
                OwnerId = OwnerLeelaId,
                BuildingId = BuildingId
            });

        modelBuilder.Entity<CondoFee>().HasData(
            new
            {
                Id = CondoFeeBugsAugustId,
                Amount = 285.00m,
                DueDate = new DateTime(2026, 8, 1),
                PaymentStatus = PaymentStatus.Paid,
                ApartmentId = ApartmentBugsId
            },
            new
            {
                Id = CondoFeeBugsSeptemberId,
                Amount = 285.00m,
                DueDate = new DateTime(2026, 9, 1),
                PaymentStatus = PaymentStatus.Overdue,
                ApartmentId = ApartmentBugsId
            },
            new
            {
                Id = CondoFeeLeelaSeptemberId,
                Amount = 340.00m,
                DueDate = new DateTime(2026, 9, 1),
                PaymentStatus = PaymentStatus.Open,
                ApartmentId = ApartmentLeelaId
            });

        modelBuilder.Entity<Payment>().HasData(
            new
            {
                Id = PaymentBugsAugustId,
                Amount = 285.00m,
                Date = new DateTime(2026, 8, 1),
                PaymentType = PaymentType.Transfer,
                Reference = CondoFeeBugsAugustId.ToString(),
                CondoFeeId = CondoFeeBugsAugustId
            });

        modelBuilder.Entity<RepairOrder>().HasData(
            new
            {
                Id = RepairOrderRoofId,
                Description = "Dachrinne am Nordflügel prüfen und reparieren",
                Priority = RepairOrderPriority.High,
                Status = RepairOrderStatus.Open,
                Cost = 1250.00m,
                Date = new DateTime(2026, 9, 10),
                Craftsman = "Acme Roofing",
                BuildingId = BuildingId
            },
            new
            {
                Id = RepairOrderElevatorId,
                Description = "Aufzug: Türsensor austauschen",
                Priority = RepairOrderPriority.High,
                Status = RepairOrderStatus.InProgress,
                Cost = 680.00m,
                Date = new DateTime(2026, 9, 12),
                Craftsman = "Planet Express Maintenance",
                BuildingId = BuildingId
            },
            new
            {
                Id = RepairOrderHeatingId,
                Description = "Heizungsanlage warten",
                Priority = RepairOrderPriority.Low,
                Status = RepairOrderStatus.Completed,
                Cost = 420.00m,
                Date = new DateTime(2026, 9, 5),
                Craftsman = "MomCorp Services",
                BuildingId = BuildingId
            });
    }
}
