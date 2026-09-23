using System.ComponentModel.DataAnnotations.Schema;
using WEGManagement.Domain.Enums;
using WEGManagement.Domain.Events;

namespace WEGManagement.Domain.Models;

public class Apartment
{
    private readonly List<DomainEvent> _domainEvents = new();

    public Guid Id { get; private set; }
    public string Address { get; private set; } = string.Empty;
    public float Size { get; private set; }
    public int Floor { get; private set; }
    public ApartmentStatus Status { get; private set; }
    public int NumberOfResidents { get; private set; }

    public Guid OwnerId { get; private set; }
    public Owner Owner { get; private set; } = null!;

    public Guid BuildingId { get; private set; }
    public Building Building { get; private set; } = null!;

    public ICollection<CondoFee> CondoFees { get; private set; } = new List<CondoFee>();

    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private Apartment() { }

    public Apartment(
        string address,
        float size,
        int floor,
        ApartmentStatus status,
        int numberOfResidents,
        Building building,
        Owner owner)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address is required.", nameof(address));
        if (size <= 0)
            throw new ArgumentOutOfRangeException(nameof(size));
        if (numberOfResidents < 0)
            throw new ArgumentOutOfRangeException(nameof(numberOfResidents));

        ArgumentNullException.ThrowIfNull(building);
        ArgumentNullException.ThrowIfNull(owner);

        Id = Guid.NewGuid();
        Address = address;
        Size = size;
        Floor = floor;
        Status = status;
        NumberOfResidents = numberOfResidents;

        Building = building;
        BuildingId = building.Id;

        Owner = owner;
        OwnerId = owner.Id;
    }

    public void ChangeOwner(Owner newOwner)
    {
        ArgumentNullException.ThrowIfNull(newOwner);

        var previousOwnerId = OwnerId;

        Owner = newOwner;
        OwnerId = newOwner.Id;

        _domainEvents.Add(
            new OwnerChanged(Id, previousOwnerId, newOwner.Id));
    }

    public void ClearDomainEvents()
        => _domainEvents.Clear();
}
