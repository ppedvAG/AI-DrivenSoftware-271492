using EventSourcing.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcing.Data;

public class InMemoryEventStore<T> where T : class, IMaterializable<T>, new()
{
    // 1. Events InMemory speichern
    private readonly Dictionary<Guid, SortedList<DateTime, DomainEvent>> _eventStore = [];

    public void Append(DomainEvent @event)
    {
        if (!_eventStore.ContainsKey(@event.StreamId))
        {
            _eventStore[@event.StreamId] = [];
        }
        @event.CreatedAtUtc = DateTime.UtcNow;
        _eventStore[@event.StreamId].Add(@event.CreatedAtUtc, @event);

        // 3. Projektion aufbauen
        var entityId = @event.StreamId;
        _entityProjections[entityId] = GetEntity(entityId)!;
    }

    // 2. Entitäten aus Events materialisieren
    public T? GetEntity(Guid id)
    {
        if (!_eventStore.ContainsKey(id))
        {
            return null;
        }

        return _eventStore[id]
            .Aggregate(new T(), (result, kvPair) => result.Apply(kvPair.Value));
    }

    // 3. (Asynchrone) Projektion 
    // Hinweis: strong consistancy vs. eventual consistancy
    private readonly Dictionary<Guid, T> _entityProjections = [];

    public T? GetEntityProjection(Guid id)
    {
        return _entityProjections.GetValueOrDefault(id);
    }
}
