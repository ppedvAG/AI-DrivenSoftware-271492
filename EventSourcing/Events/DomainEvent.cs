using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcing.Events;

public abstract class DomainEvent
{
    // Events treten immer zu einem Zeitpunkt auf
    public DateTime CreatedAtUtc { get; set; }

    // Abfolge von Events als Stream eindeutig identifizieren
    public abstract Guid StreamId { get; }
}

// Wir verwenden keine "regulaere" Student class
public class StudentRegisteredEvent : DomainEvent
{
    public required Guid StudentId { get; init; }

    public required string FullName { get; init; }

    public required string Email { get; init; }

    public required DateTime DateOfBirth { get; init; }

    public override Guid StreamId => StudentId;
}

public class StudentEmailUpdatedEvent : DomainEvent
{
    public required Guid StudentId { get; init; }

    public required string Email { get; init; }

    public override Guid StreamId => StudentId;
}

public class StudentEnrolledEvent : DomainEvent
{
    public required Guid StudentId { get; init; }

    public required string CourseName { get; init; }

    public override Guid StreamId => StudentId;
}

public class StudentDisenrolledEvent : DomainEvent
{
    public required Guid StudentId { get; init; }

    public required string CourseName { get; init; }

    public override Guid StreamId => StudentId;
}