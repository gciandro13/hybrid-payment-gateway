using HybridPaymentGateway.Domain.Entities;
using HybridPaymentGateway.Domain.Enums;

namespace HybridPaymentGateway.Domain.Interfaces;

/// <summary>
/// Repository interface for Account entity
/// </summary>
public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Account?> GetByIbanAsync(string iban, CancellationToken cancellationToken = default);
    Task<Account?> GetByBitcoinAddressAsync(string address, CancellationToken cancellationToken = default);
    Task<IEnumerable<Account>> GetByTypeAsync(AccountType type, CancellationToken cancellationToken = default);
    Task<IEnumerable<Account>> GetActiveAccountsAsync(CancellationToken cancellationToken = default);
    Task<Account> AddAsync(Account account, CancellationToken cancellationToken = default);
    Task UpdateAsync(Account account, CancellationToken cancellationToken = default);
}
