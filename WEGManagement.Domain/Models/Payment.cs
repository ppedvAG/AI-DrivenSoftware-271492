using WEGManagement.Domain.Enums;

namespace WEGManagement.Domain.Models;

public class Payment
{
    public Guid Id { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public PaymentType PaymentType { get; private set; }
    public string Reference { get; private set; } = string.Empty;

    public Guid CondoFeeId { get; private set; }
    public CondoFee CondoFee { get; private set; } = null!;

    private Payment() { }

    public Payment(
        decimal amount,
        DateTime date,
        PaymentType paymentType,
        string reference,
        Guid condoFeeId)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        Id = Guid.NewGuid();
        Amount = amount;
        Date = date;
        PaymentType = paymentType;
        Reference = reference;
        CondoFeeId = condoFeeId;
    }
}
