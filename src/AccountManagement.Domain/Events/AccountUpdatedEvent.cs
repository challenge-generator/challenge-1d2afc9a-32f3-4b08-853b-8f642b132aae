namespace AccountManagement.Domain.Events;

/// <summary>
/// Domain event raised when an account is updated in the system.
/// Captures the state changes made to an existing account for event sourcing and audit purposes.
/// </summary>
public class AccountUpdatedEvent : AccountDomainEvent
{
    public string? PreviousAccountNumber { get; }
    public string? NewAccountNumber { get; }
    public decimal? PreviousBalance { get; }
    public decimal? NewBalance { get; }
    public string? PreviousStatus { get; }
    public string? NewStatus { get; }
    public DateTime UpdatedAt { get; }
    public string UpdatedBy { get; }
    public string ChangeReason { get; }
    public List<string> ChangedFields { get; }

    public AccountUpdatedEvent(
        Guid aggregateId,
        string updatedBy,
        string changeReason) : base(aggregateId, nameof(AccountUpdatedEvent))
    {
        if (string.IsNullOrWhiteSpace(updatedBy))
            throw new ArgumentException("UpdatedBy cannot be empty", nameof(updatedBy));

        if (string.IsNullOrWhiteSpace(changeReason))
            throw new ArgumentException("ChangeReason cannot be empty", nameof(changeReason));

        UpdatedBy = updatedBy;
        ChangeReason = changeReason;
        UpdatedAt = DateTime.UtcNow;
        ChangedFields = new List<string>();

        AddMetadata("ChangeReason", changeReason);
        AddMetadata("UpdatedBy", updatedBy);
    }

    public void RecordAccountNumberChange(string previousNumber, string newNumber)
    {
        if (string.IsNullOrWhiteSpace(previousNumber))
            throw new ArgumentException("PreviousAccountNumber cannot be empty", nameof(previousNumber));

        if (string.IsNullOrWhiteSpace(newNumber))
            throw new ArgumentException("NewAccountNumber cannot be empty", nameof(newNumber));

        PreviousAccountNumber = previousNumber;
        NewAccountNumber = newNumber;
        ChangedFields.Add("AccountNumber");
        AddMetadata("PreviousAccountNumber", previousNumber);
        AddMetadata("NewAccountNumber", newNumber);
    }

    public void RecordBalanceChange(decimal previousBalance, decimal newBalance)
    {
        if (previousBalance < 0)
            throw new ArgumentException("PreviousBalance cannot be negative", nameof(previousBalance));

        if (newBalance < 0)
            throw new ArgumentException("NewBalance cannot be negative", nameof(newBalance));

        PreviousBalance = previousBalance;
        NewBalance = newBalance;
        ChangedFields.Add("Balance");
        AddMetadata("PreviousBalance", previousBalance);
        AddMetadata("NewBalance", newBalance);
        AddMetadata("BalanceDifference", newBalance - previousBalance);
    }

    public void RecordStatusChange(string previousStatus, string newStatus)
    {
        if (string.IsNullOrWhiteSpace(previousStatus))
            throw new ArgumentException("PreviousStatus cannot be empty", nameof(previousStatus));

        if (string.IsNullOrWhiteSpace(newStatus))
            throw new ArgumentException("NewStatus cannot be empty", nameof(newStatus));

        PreviousStatus = previousStatus;
        NewStatus = newStatus;
        ChangedFields.Add("Status");
        AddMetadata("PreviousStatus", previousStatus);
        AddMetadata("NewStatus", newStatus);
    }

    public bool HasChanges() => ChangedFields.Count > 0;

    public override string ToString() =>
        $"AccountUpdated: AggregateId={AggregateId}, ChangedFields=[{string.Join(", ", ChangedFields)}], UpdatedBy={UpdatedBy}, Reason={ChangeReason}";
}