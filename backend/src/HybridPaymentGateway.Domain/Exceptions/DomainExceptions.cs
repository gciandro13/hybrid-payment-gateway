namespace HybridPaymentGateway.Domain.Exceptions;

/// <summary>
/// Base exception for domain-specific errors
/// </summary>
public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }

    protected DomainException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Exception thrown when a payment operation is invalid
/// </summary>
public class InvalidPaymentException : DomainException
{
    public InvalidPaymentException(string message) : base(message)
    {
    }
}

/// <summary>
/// Exception thrown when an account is not found
/// </summary>
public class AccountNotFoundException : DomainException
{
    public Guid AccountId { get; }

    public AccountNotFoundException(Guid accountId) 
        : base($"Account with ID '{accountId}' was not found")
    {
        AccountId = accountId;
    }
}

/// <summary>
/// Exception thrown when a payment is not found
/// </summary>
public class PaymentNotFoundException : DomainException
{
    public Guid PaymentId { get; }

    public PaymentNotFoundException(Guid paymentId) 
        : base($"Payment with ID '{paymentId}' was not found")
    {
        PaymentId = paymentId;
    }
}

/// <summary>
/// Exception thrown when insufficient funds are available
/// </summary>
public class InsufficientFundsException : DomainException
{
    public decimal RequiredAmount { get; }
    public decimal AvailableAmount { get; }

    public InsufficientFundsException(decimal requiredAmount, decimal availableAmount)
        : base($"Insufficient funds. Required: {requiredAmount}, Available: {availableAmount}")
    {
        RequiredAmount = requiredAmount;
        AvailableAmount = availableAmount;
    }
}

/// <summary>
/// Exception thrown when a Bitcoin transaction fails
/// </summary>
public class BitcoinTransactionException : DomainException
{
    public string? TransactionHash { get; }

    public BitcoinTransactionException(string message, string? transactionHash = null) 
        : base(message)
    {
        TransactionHash = transactionHash;
    }
}
