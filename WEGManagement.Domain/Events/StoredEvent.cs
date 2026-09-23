namespace WEGManagement.Domain.Events;

public class StoredEvent
{
    public long EventId { get; set; }

    public Guid AggregateId { get; set; }

    public string AggregateType { get; set; } = null!;

    public string EventType { get; set; } = null!;

    public string Data { get; set; } = null!;

    public DateTime OccurredOn { get; set; }
}