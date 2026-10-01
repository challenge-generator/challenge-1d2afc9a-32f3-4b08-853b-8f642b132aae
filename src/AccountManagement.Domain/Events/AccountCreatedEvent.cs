namespace AccountManagement.Domain.Events;

/// <summary>
/// Domain event raised when a new account is created in the system.
/// Contains all necessary information about the newly created account for event sourcing and notification purposes.
/// </summary>
public class AccountCreatedEvent : AccountDomainEvent
{
    public Guid UserId { get; }
    public string AccountNumber { get; }
    public decimal InitialBalance { get; }
    public string AccountType { get; }
    public string Currency { get; }
    public DateTime CreatedAt { get; }
    public string CreatedBy { get; }

    public AccountCreatedEvent(
        Guid aggregateId,
        Guid userId,
        string accountNumber,
        decimal initialBalance,
        string accountType,
        string currency,
        string createdBy) : base(aggregateId, nameof(AccountCreatedEvent))
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId cannot be empty", nameof(userId));

        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentException("AccountNumber cannot be empty", nameof(accountNumber));

        if (initialBalance < 0)
            throw new ArgumentException("InitialBalance cannot be negative", nameof(initialBalance));

        if (string.IsNullOrWhiteSpace(accountType))
            throw new ArgumentException("AccountType cannot be empty", nameof(accountType));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency cannot be empty", nameof(currency));

        if (string.IsNullOrWhiteSpace(createdBy))
            throw new ArgumentException("CreatedBy cannot be empty", nameof(createdBy));

        UserId = userId;
        AccountNumber = accountNumber;
        InitialBalance = initialBalance;
        AccountType = accountType;
        Currency = currency;
        CreatedAt = DateTime.UtcNow;
        CreatedBy = createdBy;

        AddMetadata("InitialBalanceFormatted", $"{initialBalance:N2} {currency}");
        AddMetadata("UserId", userId.ToString());
        AddMetadata("AccountType", accountType);
    }

    public override string ToString() =>
        $"AccountCreated: AccountNumber={AccountNumber}, UserId={UserId}, InitialBalance={InitialBalance} {Currency}, Type={AccountType}, CreatedBy={CreatedBy}";
}