using EventSourcing.Events;

namespace EventSourcing.Data;

public interface IMaterializable<T>
{
    T Apply(DomainEvent evt);
}
