using HybridPaymentGateway.Domain.Enums;
using HybridPaymentGateway.Domain.ValueObjects;

namespace HybridPaymentGateway.Domain.Entities;

/// <summary>
/// Represents a financial account (bank or bitcoin wallet)
/// </summary>
public class Account
{
    public Guid Id { get; private set; }
    public AccountType Type { get; private set; }
    public string AccountHolder { get; private set; }
    
    // Bank account properties
    public IBAN? Iban { get; private set; }
    public string? Bic { get; private set; }
    public string? BankName { get; private set; }
    
    // Bitcoin wallet properties
    public BitcoinAddress? WalletAddress { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public bool IsActive { get; private set; }

    private Account() { } // EF Core

    public static Account CreateBankAccount(
        string accountHolder,
        IBAN iban,
        string? bic = null,
        string? bankName = null)
    {
        if (string.IsNullOrWhiteSpace(accountHolder))
            throw new ArgumentException("Account holder cannot be empty", nameof(accountHolder));

        return new Account
        {
            Id = Guid.NewGuid(),
            Type = AccountType.BankAccount,
            AccountHolder = accountHolder,
            Iban = iban ?? throw new ArgumentNullException(nameof(iban)),
            Bic = bic,
            BankName = bankName,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    public static Account CreateBitcoinWallet(
        string accountHolder,
        BitcoinAddress walletAddress)
    {
        if (string.IsNullOrWhiteSpace(accountHolder))
            throw new ArgumentException("Account holder cannot be empty", nameof(accountHolder));

        return new Account
        {
            Id = Guid.NewGuid(),
            Type = AccountType.BitcoinWallet,
            AccountHolder = accountHolder,
            WalletAddress = walletAddress ?? throw new ArgumentNullException(nameof(walletAddress)),
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public string GetAccountIdentifier()
    {
        return Type switch
        {
            AccountType.BankAccount => Iban?.Value ?? "Unknown",
            AccountType.BitcoinWallet => WalletAddress?.Value ?? "Unknown",
            _ => "Unknown"
        };
    }
}
