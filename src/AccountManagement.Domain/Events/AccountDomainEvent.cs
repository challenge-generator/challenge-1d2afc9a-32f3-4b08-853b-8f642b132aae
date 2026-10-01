namespace AccountManagement.Domain.Events;

/// <summary>
/// Base class for all domain events in the account management system.
/// Implements the event sourcing pattern for immutable event storage.
/// </summary>
public abstract class AccountDomainEvent
{
    public Guid Id { get; }
    public DateTime OccurredAt { get; }
    public string EventType { get; }
    public Guid AggregateId { get; }
    public int Version { get; protected set; }
    public Dictionary<string, object> Metadata { get; }

    protected AccountDomainEvent(Guid aggregateId, string eventType)
    {
        if (aggregateId == Guid.Empty)
            throw new ArgumentException("AggregateId cannot be empty", nameof(aggregateId));

        Id = Guid.NewGuid();
        OccurredAt = DateTime.UtcNow;
        EventType = eventType ?? throw new ArgumentNullException(nameof(eventType));
        AggregateId = aggregateId;
        Version = 1;
        Metadata = new Dictionary<string, object>();
    }

    public void IncrementVersion() => Version++;

    public void AddMetadata(string key, object value)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Metadata key cannot be empty", nameof(key));

        Metadata[key] = value ?? throw new ArgumentNullException(nameof(value));
    }

    public bool TryGetMetadata<T>(string key, out T? value)
    {
        if (Metadata.TryGetValue(key, out var rawValue) && rawValue is T typedValue)
        {
            value = typedValue;
            return true;
        }

        value = default;
        return false;
    }

    public override string ToString() => $"{EventType} (AggregateId: {AggregateId}, Version: {Version}, OccurredAt: {OccurredAt:yyyy-MM-dd HH:mm:ss})";
}