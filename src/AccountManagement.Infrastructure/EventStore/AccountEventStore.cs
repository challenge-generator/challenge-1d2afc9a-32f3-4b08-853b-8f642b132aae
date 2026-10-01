using AccountManagement.Domain.Entities;
using AccountManagement.Domain.Events;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AccountManagement.Infrastructure.EventStore;

public class AccountEventStore
{
    private readonly AccountDbContext _context;
    private readonly ILogger<AccountEventStore> _logger;

    public AccountEventStore(AccountDbContext context, ILogger<AccountEventStore> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task SaveEventAsync(AccountDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        if (domainEvent == null)
        {
            throw new ArgumentNullException(nameof(domainEvent));
        }

        var storedEvent = new StoredEvent
        {
            Id = Guid.NewGuid(),
            AggregateId = domainEvent.AccountId,
            EventType = domainEvent.GetType().Name,
            EventData = JsonSerializer.Serialize(domainEvent),
            OccurredOn = domainEvent.OccurredOn,
            Version = domainEvent.Version
        };

        await _context.StoredEvents.AddAsync(storedEvent, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Evento {EventType} guardado para aggregate {AggregateId} con versión {Version}",
            storedEvent.EventType, storedEvent.AggregateId, storedEvent.Version);
    }

    public async Task SaveEventsAsync(IEnumerable<AccountDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        if (domainEvents == null)
        {
            throw new ArgumentNullException(nameof(domainEvents));
        }

        var eventsList = domainEvents.ToList();
        if (!eventsList.Any())
        {
            _logger.LogWarning("No hay eventos para guardar");
            return;
        }

        foreach (var domainEvent in eventsList)
        {
            var storedEvent = new StoredEvent
            {
                Id = Guid.NewGuid(),
                AggregateId = domainEvent.AccountId,
                EventType = domainEvent.GetType().Name,
                EventData = JsonSerializer.Serialize(domainEvent),
                OccurredOn = domainEvent.OccurredOn,
                Version = domainEvent.Version
            };

            await _context.StoredEvents.AddAsync(storedEvent, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Se guardaron {Count} eventos para el aggregate", eventsList.Count);
    }

    public async Task<IReadOnlyList<StoredEvent>> GetEventsForAggregateAsync(Guid aggregateId, CancellationToken cancellationToken = default)
    {
        if (aggregateId == Guid.Empty)
        {
            _logger.LogWarning("Se intentó obtener eventos para un aggregate con ID vacío");
            return Array.Empty<StoredEvent>();
        }

        var events = await _context.StoredEvents
            .Where(e => e.AggregateId == aggregateId)
            .OrderBy(e => e.OccurredOn)
            .ThenBy(e => e.Version)
            .ToListAsync(cancellationToken);

        _logger.LogDebug("Se recuperaron {Count} eventos para el aggregate {AggregateId}", events.Count, aggregateId);

        return events;
    }

    public async Task<int> GetEventCountForAggregateAsync(Guid aggregateId, CancellationToken cancellationToken = default)
    {
        if (aggregateId == Guid.Empty)
        {
            return 0;
        }

        var count = await _context.StoredEvents
            .CountAsync(e => e.AggregateId == aggregateId, cancellationToken);

        return count;
    }

    public async Task<IReadOnlyList<StoredEvent>> GetEventsByTypeAsync(string eventType, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(eventType))
        {
            _logger.LogWarning("Se intentó obtener eventos con tipo vacío o nulo");
            return Array.Empty<StoredEvent>();
        }

        var events = await _context.StoredEvents
            .Where(e => e.EventType == eventType)
            .OrderByDescending(e => e.OccurredOn)
            .ToListAsync(cancellationToken);

        _logger.LogDebug("Se recuperaron {Count} eventos de tipo {EventType}", events.Count, eventType);

        return events;
    }

    public async Task<IReadOnlyList<StoredEvent>> GetEventsInDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        if (startDate > endDate)
        {
            _logger.LogWarning("La fecha de inicio {StartDate} es posterior a la fecha de fin {EndDate}");
            return Array.Empty<StoredEvent>();
        }

        var events = await _context.StoredEvents
            .Where(e => e.OccurredOn >= startDate && e.OccurredOn <= endDate)
            .OrderBy(e => e.OccurredOn)
            .ToListAsync(cancellationToken);

        _logger.LogDebug("Se recuperaron {Count} eventos entre {StartDate} y {EndDate}", 
            events.Count, startDate, endDate);

        return events;
    }
}

public class StoredEvent
{
    public Guid Id { get; set; }
    public Guid AggregateId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string EventData { get; set; } = string.Empty;
    public DateTime OccurredOn { get; set; }
    public int Version { get; set; }
}