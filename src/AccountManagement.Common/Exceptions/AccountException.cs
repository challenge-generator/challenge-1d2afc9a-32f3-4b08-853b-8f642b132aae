namespace AccountManagement.Common.Exceptions;

public abstract class AccountException : Exception
{
    public Guid? AccountId { get; }
    public string Code { get; }
    public DateTime OccurredAt { get; }

    protected AccountException(string message, string code, Guid? accountId = null)
        : base(message)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        AccountId = accountId;
        OccurredAt = DateTime.UtcNow;
    }

    protected AccountException(string message, string code, Guid? accountId, Exception innerException)
        : base(message, innerException)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        AccountId = accountId;
        OccurredAt = DateTime.UtcNow;
    }

    public virtual Dictionary<string, object> ToErrorDetails()
    {
        var details = new Dictionary<string, object>
        {
            { "type", "https://tools.ietf.org/html/rfc7231#section-6.6.1" },
            { "title", "Account Error" },
            { "status", 500 },
            { "code", Code },
            { "message", Message },
            { "occurredAt", OccurredAt }
        };

        if (AccountId.HasValue)
        {
            details["accountId"] = AccountId.Value;
        }

        return details;
    }
}

public enum AccountErrorCode
{
    Unknown = 0,
    AccountNotFound = 1001,
    AccountAlreadyExists = 1002,
    InvalidAccountNumber = 1003,
    InsufficientFunds = 1004,
    AccountClosed = 1005,
    AccountLocked = 1006,
    InvalidAmount = 1007,
    UserNotFound = 1008,
    ValidationError = 1009,
    ConcurrencyConflict = 1010,
    ExternalServiceFailure = 1011
}