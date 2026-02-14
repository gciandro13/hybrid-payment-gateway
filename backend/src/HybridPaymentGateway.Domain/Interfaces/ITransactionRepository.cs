using HybridPaymentGateway.Domain.Entities;
using HybridPaymentGateway.Domain.Enums;

namespace HybridPaymentGateway.Domain.Interfaces;

/// <summary>
/// Repository interface for Transaction entity
/// </summary>
public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Transaction?> GetByHashAsync(string hash, CancellationToken cancellationToken = default);
    Task<IEnumerable<Transaction>> GetByPaymentIdAsync(Guid paymentId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Transaction>> GetByTypeAsync(TransactionType type, CancellationToken cancellationToken = default);
    Task<IEnumerable<Transaction>> GetPendingAsync(CancellationToken cancellationToken = default);
    Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
    Task UpdateAsync(Transaction transaction, CancellationToken cancellationToken = default);
}
