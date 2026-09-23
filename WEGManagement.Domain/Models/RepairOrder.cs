using System.ComponentModel.DataAnnotations.Schema;
using WEGManagement.Domain.Enums;
using WEGManagement.Domain.Events;

namespace WEGManagement.Domain.Models;

public class RepairOrder
{
    private readonly List<DomainEvent> _domainEvents = new();

    public Guid Id { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public RepairOrderPriority Priority { get; private set; }
    public RepairOrderStatus Status { get; private set; }
    public decimal Cost { get; private set; }
    public DateTime Date { get; private set; }
    public string Craftsman { get; private set; } = string.Empty;

    public Guid BuildingId { get; private set; }
    public Building Building { get; private set; } = null!;

    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private RepairOrder() { }

    public RepairOrder(
        string description,
        RepairOrderPriority priority,
        decimal cost,
        DateTime date,
        string craftsman,
        Building building)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required.", nameof(description));
        if (cost < 0)
            throw new ArgumentOutOfRangeException(nameof(cost));

        ArgumentNullException.ThrowIfNull(building);

        Id = Guid.NewGuid();
        Description = description;
        Priority = priority;
        Status = RepairOrderStatus.Open;
        Cost = cost;
        Date = date;
        Craftsman = craftsman;

        Building = building;
        BuildingId = building.Id;
    }

    public void Start()
    {
        if (Status == RepairOrderStatus.Completed)
            throw new InvalidOperationException(
                "A completed repair order cannot be started.");

        Status = RepairOrderStatus.InProgress;
    }

    public void Complete()
    {
        if (Status == RepairOrderStatus.Completed)
            return;

        Status = RepairOrderStatus.Completed;
        _domainEvents.Add(new RepairOrderCompleted(Id));
    }

    public void ClearDomainEvents()
        => _domainEvents.Clear();
}