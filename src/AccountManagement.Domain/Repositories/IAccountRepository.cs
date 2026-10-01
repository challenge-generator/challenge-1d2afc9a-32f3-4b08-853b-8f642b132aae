using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AccountManagement.Domain.Entities;

namespace AccountManagement.Domain.Repositories;

public interface IAccountRepository
{
    Task<Account> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Account> GetByAccountNumberAsync(string accountNumber, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Account>> GetAccountsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(Account account, CancellationToken cancellationToken = default);
    Task UpdateAsync(Account account, CancellationToken cancellationToken = default);
    Task<bool> AccountNumberExistsAsync(string accountNumber, CancellationToken cancellationToken = default);
    Task<bool> UserHasAccountsAsync(Guid userId, CancellationToken cancellationToken = default);
}