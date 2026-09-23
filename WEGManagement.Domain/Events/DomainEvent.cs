namespace WEGManagement.Domain.Events;

public abstract record DomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}

public record CondoFeeDue(Guid CondoFeeId) : DomainEvent
{
}

public record PaymentReceived(Guid CondoFeeId, Guid PaymentId, decimal Amount) : DomainEvent
{
}

public record RepairOrderCompleted(Guid RepairOrderId) : DomainEvent
{
}

public record OwnerChanged(Guid ApartmentId, Guid? PreviousOwnerId, Guid NewOwnerId) : DomainEvent
{
}
