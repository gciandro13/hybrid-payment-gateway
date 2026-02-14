using HybridPaymentGateway.Domain.Enums;
using HybridPaymentGateway.Domain.ValueObjects;

namespace HybridPaymentGateway.Domain.Entities;

/// <summary>
/// Represents a payment entity in the system
/// </summary>
public class Payment
{
    public Guid Id { get; private set; }
    public string PaymentReference { get; private set; }
    public PaymentType Type { get; private set; }
    public Money Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public Account DebtorAccount { get; private set; }
    public Account CreditorAccount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ProcessedAt { get; private set; }
    public string? Description { get; private set; }

    // Navigation properties
    public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

    private Payment() { } // EF Core

    public Payment(
        string paymentReference,
        PaymentType type,
        Money amount,
        Account debtorAccount,
        Account creditorAccount,
        string? description = null)
    {
        if (string.IsNullOrWhiteSpace(paymentReference))
            throw new ArgumentException("Payment reference cannot be empty", nameof(paymentReference));

        if (amount.Amount <= 0)
            throw new ArgumentException("Payment amount must be greater than zero", nameof(amount));

        Id = Guid.NewGuid();
        PaymentReference = paymentReference;
        Type = type;
        Amount = amount;
        DebtorAccount = debtorAccount ?? throw new ArgumentNullException(nameof(debtorAccount));
        CreditorAccount = creditorAccount ?? throw new ArgumentNullException(nameof(creditorAccount));
        Description = description;
        Status = PaymentStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsProcessing()
    {
        if (Status != PaymentStatus.Pending)
            throw new InvalidOperationException($"Cannot process payment in {Status} status");

        Status = PaymentStatus.Processing;
    }

    public void MarkAsCompleted()
    {
        if (Status != PaymentStatus.Processing)
            throw new InvalidOperationException($"Cannot complete payment in {Status} status");

        Status = PaymentStatus.Completed;
        ProcessedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed(string reason)
    {
        Status = PaymentStatus.Failed;
        ProcessedAt = DateTime.UtcNow;
        // TODO: Add failure reason tracking
    }

    public void AddTransaction(Transaction transaction)
    {
        if (transaction == null)
            throw new ArgumentNullException(nameof(transaction));

        Transactions.Add(transaction);
    }
}
