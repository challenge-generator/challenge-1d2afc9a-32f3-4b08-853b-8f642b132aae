using AccountManagement.Domain.Entities;
using AccountManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AccountDbContext _context;
    private readonly ILogger<AccountRepository> _logger;

    public AccountRepository(AccountDbContext context, ILogger<AccountRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Account> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            _logger.LogWarning("Se intentó obtener una cuenta con ID vacío");
            return null!;
        }

        var account = await _context.Accounts
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (account == null)
        {
            _logger.LogInformation("Cuenta con ID {AccountId} no encontrada", id);
        }
        else
        {
            _logger.LogDebug("Cuenta {AccountId} recuperada exitosamente", id);
        }

        return account!;
    }

    public async Task<Account> GetByAccountNumberAsync(string accountNumber, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
        {
            _logger.LogWarning("Se intentó obtener una cuenta con número de cuenta vacío o nulo");
            return null!;
        }

        var account = await _context.Accounts
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber, cancellationToken);

        if (account == null)
        {
            _logger.LogInformation("Cuenta con número {AccountNumber} no encontrada", accountNumber);
        }
        else
        {
            _logger.LogDebug("Cuenta {AccountNumber} recuperada exitosamente", accountNumber);
        }

        return account!;
    }

    public async Task<IReadOnlyList<Account>> GetAccountsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
        {
            _logger.LogWarning("Se intentó obtener cuentas para usuario con ID vacío");
            return Array.Empty<Account>();
        }

        var accounts = await _context.Accounts
            .Include(a => a.User)
            .Where(a => a.UserId == userId)
            .ToListAsync(cancellationToken);

        _logger.LogDebug("Se recuperaron {Count} cuentas para el usuario {UserId}", accounts.Count, userId);

        return accounts;
    }

    public async Task AddAsync(Account account, CancellationToken cancellationToken = default)
    {
        if (account == null)
        {
            throw new ArgumentNullException(nameof(account));
        }

        await _context.Accounts.AddAsync(account, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Cuenta {AccountId} con número {AccountNumber} creada exitosamente", 
            account.Id, account.AccountNumber);
    }

    public async Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
    {
        if (account == null)
        {
            throw new ArgumentNullException(nameof(account));
        }

        var existingAccount = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == account.Id, cancellationToken);

        if (existingAccount == null)
        {
            _logger.LogError("No se puede actualizar la cuenta {AccountId}: no existe", account.Id);
            throw new InvalidOperationException($"La cuenta con ID {account.Id} no existe");
        }

        _context.Entry(existingAccount).CurrentValues.SetValues(account);
        await _context.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Cuenta {AccountId} actualizada exitosamente", account.Id);
    }

    public async Task<bool> AccountNumberExistsAsync(string accountNumber, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
        {
            return false;
        }

        var exists = await _context.Accounts
            .AnyAsync(a => a.AccountNumber == accountNumber, cancellationToken);

        _logger.LogDebug("Verificación de número de cuenta {AccountNumber}: {Exists}", accountNumber, exists);

        return exists;
    }

    public async Task<bool> UserHasAccountsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        if (userId == Guid.Empty)
        {
            return false;
        }

        var hasAccounts = await _context.Accounts
            .AnyAsync(a => a.UserId == userId, cancellationToken);

        _logger.LogDebug("Usuario {UserId} tiene cuentas: {HasAccounts}", userId, hasAccounts);

        return hasAccounts;
    }
}