using System.ComponentModel.DataAnnotations.Schema;
using WEGManagement.Domain.Enums;
using WEGManagement.Domain.Events;

namespace WEGManagement.Domain.Models;

public class CondoFee
{
    private readonly List<DomainEvent> _domainEvents = new();

    public Guid Id { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime DueDate { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }

    public Guid ApartmentId { get; private set; }
    public Apartment Apartment { get; private set; } = null!;

    public ICollection<Payment> Payments { get; private set; } = new List<Payment>();

    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private CondoFee() { }

    public CondoFee(decimal amount, DateTime dueDate, Apartment apartment)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        ArgumentNullException.ThrowIfNull(apartment);

        Id = Guid.NewGuid();
        Amount = amount;
        DueDate = dueDate;
        PaymentStatus = PaymentStatus.Open;

        Apartment = apartment;
        ApartmentId = apartment.Id;
    }

    public void CreateReminder(DateTime? now = null)
    {
        var currentDate = now ?? DateTime.UtcNow;

        if (PaymentStatus == PaymentStatus.Paid)
            return;

        if (currentDate >= DueDate)
        {
            PaymentStatus = PaymentStatus.Overdue;
            _domainEvents.Add(new CondoFeeDue(Id));
        }
    }

    public void MarkAsPaid(Payment payment)
    {
        ArgumentNullException.ThrowIfNull(payment);

        if (PaymentStatus == PaymentStatus.Paid)
            return;

        PaymentStatus = PaymentStatus.Paid;
        Payments.Add(payment);

        _domainEvents.Add(
            new PaymentReceived(Id, payment.Id, payment.Amount));
    }

    public void ClearDomainEvents()
        => _domainEvents.Clear();
}
