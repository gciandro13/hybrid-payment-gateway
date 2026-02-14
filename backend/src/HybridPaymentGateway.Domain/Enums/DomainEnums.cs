namespace HybridPaymentGateway.Domain.Enums;

public enum PaymentStatus
{
    Pending = 0,
    Processing = 1,
    Completed = 2,
    Failed = 3,
    Cancelled = 4
}

public enum PaymentType
{
    BankTransfer = 0,
    BitcoinTransfer = 1,
    Hybrid = 2
}

public enum TransactionType
{
    ISO20022 = 0,
    Bitcoin = 1
}

public enum TransactionStatus
{
    Pending = 0,
    Broadcasting = 1,
    Confirmed = 2,
    Failed = 3
}

public enum AccountType
{
    BankAccount = 0,
    BitcoinWallet = 1
}
