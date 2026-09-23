using System.ComponentModel.DataAnnotations.Schema;
using WEGManagement.Domain.Events;

namespace WEGManagement.Domain.Models;

public class Building
{
    private readonly List<DomainEvent> _domainEvents = new();

    public Guid Id { get; private set; }
    public string Address { get; private set; } = string.Empty;

    public ICollection<Apartment> Apartments { get; private set; } = new List<Apartment>();
    public ICollection<RepairOrder> RepairOrders { get; private set; } = new List<RepairOrder>();

    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private Building() { }

    public Building(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address is required.", nameof(address));

        Id = Guid.NewGuid();
        Address = address;
    }

    public void AddDomainEvent(DomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents()
        => _domainEvents.Clear();
}
