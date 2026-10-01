namespace AccountManagement.Common.DTOs;

public class AccountDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public string UserRole { get; set; } = string.Empty;

    public static AccountDto FromDomain(Domain.Entities.Account account)
    {
        if (account == null)
            throw new ArgumentNullException(nameof(account));

        return new AccountDto
        {
            Id = account.Id,
            UserId = account.UserId,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            Status = account.Status.ToString(),
            CreatedAt = account.CreatedAt,
            UpdatedAt = account.UpdatedAt,
            UserName = account.User?.Name ?? string.Empty,
            UserEmail = account.User?.Email ?? string.Empty,
            UserRole = account.User?.Role.ToString() ?? string.Empty
        };
    }
}

public class CreateAccountDto
{
    public Guid UserId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public decimal InitialBalance { get; set; }
}

public class UpdateAccountDto
{
    public Guid Id { get; set; }
    public string? AccountNumber { get; set; }
    public string? Status { get; set; }
}

public class AccountSummaryDto
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string Status { get; set; } = string.Empty;
}