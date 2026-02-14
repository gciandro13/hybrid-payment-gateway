using HybridPaymentGateway.Domain.Enums;
using HybridPaymentGateway.Domain.ValueObjects;

namespace HybridPaymentGateway.Domain.Entities;

/// <summary>
/// Represents a transaction (ISO 20022 or Bitcoin)
/// </summary>
public class Transaction
{
    public Guid Id { get; private set; }
    public Guid PaymentId { get; private set; }
    public string TransactionHash { get; private set; }
    public TransactionType Type { get; private set; }
    public Money Amount { get; private set; }
    public TransactionStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ConfirmedAt { get; private set; }
    
    // Bitcoin specific
    public BitcoinAddress? FromAddress { get; private set; }
    public BitcoinAddress? ToAddress { get; private set; }
    public int? Confirmations { get; private set; }
    
    // ISO 20022 specific
    public string? InstructionId { get; private set; }
    public string? EndToEndId { get; private set; }

    // Navigation property
    public Payment Payment { get; private set; } = null!;

    private Transaction() { } // EF Core

    public static Transaction CreateBitcoinTransaction(
        Guid paymentId,
        Money amount,
        BitcoinAddress fromAddress,
        BitcoinAddress toAddress)
    {
        return new Transaction
        {
            Id = Guid.NewGuid(),
            PaymentId = paymentId,
            TransactionHash = string.Empty, // Will be set when broadcasted
            Type = TransactionType.Bitcoin,
            Amount = amount,
            Status = TransactionStatus.Pending,
            FromAddress = fromAddress,
            ToAddress = toAddress,
            Confirmations = 0,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Transaction CreateIso20022Transaction(
        Guid paymentId,
        Money amount,
        string instructionId,
        string endToEndId)
    {
        if (string.IsNullOrWhiteSpace(instructionId))
            throw new ArgumentException("Instruction ID cannot be empty", nameof(instructionId));

        if (string.IsNullOrWhiteSpace(endToEndId))
            throw new ArgumentException("End-to-End ID cannot be empty", nameof(endToEndId));

        return new Transaction
        {
            Id = Guid.NewGuid(),
            PaymentId = paymentId,
            TransactionHash = $"ISO_{instructionId}",
            Type = TransactionType.ISO20022,
            Amount = amount,
            Status = TransactionStatus.Pending,
            InstructionId = instructionId,
            EndToEndId = endToEndId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void SetTransactionHash(string hash)
    {
        if (string.IsNullOrWhiteSpace(hash))
            throw new ArgumentException("Transaction hash cannot be empty", nameof(hash));

        TransactionHash = hash;
    }

    public void MarkAsConfirmed()
    {
        Status = TransactionStatus.Confirmed;
        ConfirmedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed()
    {
        Status = TransactionStatus.Failed;
    }

    public void UpdateConfirmations(int confirmations)
    {
        if (Type != TransactionType.Bitcoin)
            throw new InvalidOperationException("Confirmations are only applicable to Bitcoin transactions");

        Confirmations = confirmations;
        
        if (confirmations >= 6 && Status != TransactionStatus.Confirmed)
        {
            MarkAsConfirmed();
        }
    }
}
