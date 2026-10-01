namespace AccountManagement.Common.Exceptions;

public class AccountNotFoundException : AccountException
{
    public AccountNotFoundException(Guid accountId)
        : base(
            $"La cuenta con ID '{accountId}' no fue encontrada.",
            AccountErrorCode.AccountNotFound.ToAccountErrorString(),
            accountId)
    {
    }

    public AccountNotFoundException(string accountNumber)
        : base(
            $"La cuenta con número '{accountNumber}' no fue encontrada.",
            AccountErrorCode.AccountNotFound.ToAccountErrorString(),
            null)
    {
    }

    public AccountNotFoundException(Guid accountId, Exception innerException)
        : base(
            $"La cuenta con ID '{accountId}' no fue encontrada.",
            AccountErrorCode.AccountNotFound.ToAccountErrorString(),
            accountId,
            innerException)
    {
    }

    public override Dictionary<string, object> ToErrorDetails()
    {
        var details = base.ToErrorDetails();
        details["title"] = "Account Not Found";
        details["status"] = 404;
        details["code"] = AccountErrorCode.AccountNotFound.ToAccountErrorString();
        return details;
    }
}

public static class AccountErrorCodeExtensions
{
    public static string ToAccountErrorString(this AccountErrorCode code)
    {
        return code switch
        {
            AccountErrorCode.Unknown => "UNKNOWN_ERROR",
            AccountErrorCode.AccountNotFound => "ACCOUNT_NOT_FOUND",
            AccountErrorCode.AccountAlreadyExists => "ACCOUNT_ALREADY_EXISTS",
            AccountErrorCode.InvalidAccountNumber => "INVALID_ACCOUNT_NUMBER",
            AccountErrorCode.InsufficientFunds => "INSUFFICIENT_FUNDS",
            AccountErrorCode.AccountClosed => "ACCOUNT_CLOSED",
            AccountErrorCode.AccountLocked => "ACCOUNT_LOCKED",
            AccountErrorCode.InvalidAmount => "INVALID_AMOUNT",
            AccountErrorCode.UserNotFound => "USER_NOT_FOUND",
            AccountErrorCode.ValidationError => "VALIDATION_ERROR",
            AccountErrorCode.ConcurrencyConflict => "CONCURRENCY_CONFLICT",
            AccountErrorCode.ExternalServiceFailure => "EXTERNAL_SERVICE_FAILURE",
            _ => "UNKNOWN_ERROR"
        };
    }
}