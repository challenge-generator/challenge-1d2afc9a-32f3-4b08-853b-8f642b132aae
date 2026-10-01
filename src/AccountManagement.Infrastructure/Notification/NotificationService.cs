using AccountManagement.Domain.Events;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System.Net.Http.Json;
using System.Text.Json;

namespace AccountManagement.Infrastructure.Notification;

public class NotificationService : INotificationService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<NotificationService> _logger;
    private readonly ResiliencePipeline<HttpResponseMessage> _resiliencePipeline;
    private readonly NotificationSettings _settings;

    public NotificationService(
        IHttpClientFactory httpClientFactory,
        ILogger<NotificationService> logger,
        NotificationSettings settings)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));

        _resiliencePipeline = new ResiliencePipelineBuilder<HttpResponseMessage>()
            .AddRetry(new RetryStrategyOptions<HttpResponseMessage>
            {
                MaxRetryAttempts = _settings.MaxRetryAttempts,
                Delay = TimeSpan.FromMilliseconds(_settings.RetryDelayMs),
                BackoffType = DelayBackoffType.Exponential,
                ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                    .Handle<HttpRequestException>()
                    .Handle<TaskCanceledException>()
                    .HandleResult(r => !r.IsSuccessStatusCode && (int)r.StatusCode >= 500)
            })
            .AddCircuitBreaker(new CircuitBreakerStrategyOptions<HttpResponseMessage>
            {
                FailureRatio = _settings.CircuitBreakerFailureRatio,
                SamplingDuration = TimeSpan.FromSeconds(_settings.CircuitBreakerSamplingDurationSeconds),
                MinimumThroughput = _settings.CircuitBreakerMinimumThroughput,
                BreakDuration = TimeSpan.FromSeconds(_settings.CircuitBreakerBreakDurationSeconds),
                ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                    .Handle<HttpRequestException>()
                    .Handle<TaskCanceledException>()
                    .HandleResult(r => !r.IsSuccessStatusCode)
            })
            .Build();
    }

    public async Task NotifyAccountCreatedAsync(AccountCreatedEvent accountCreatedEvent, CancellationToken cancellationToken = default)
    {
        if (accountCreatedEvent == null)
        {
            throw new ArgumentNullException(nameof(accountCreatedEvent));
        }

        var notification = new AccountNotification
        {
            EventType = "AccountCreated",
            AccountId = accountCreatedEvent.AccountId,
            AccountNumber = accountCreatedEvent.AccountNumber,
            UserId = accountCreatedEvent.UserId,
            Timestamp = accountCreatedEvent.OccurredOn,
            Payload = JsonSerializer.Serialize(accountCreatedEvent)
        };

        await SendNotificationAsync(notification, cancellationToken);
    }

    public async Task NotifyAccountUpdatedAsync(AccountUpdatedEvent accountUpdatedEvent, CancellationToken cancellationToken = default)
    {
        if (accountUpdatedEvent == null)
        {
            throw new ArgumentNullException(nameof(accountUpdatedEvent));
        }

        var notification = new AccountNotification
        {
            EventType = "AccountUpdated",
            AccountId = accountUpdatedEvent.AccountId,
            AccountNumber = accountUpdatedEvent.AccountNumber,
            UserId = accountUpdatedEvent.UserId,
            Timestamp = accountUpdatedEvent.OccurredOn,
            Payload = JsonSerializer.Serialize(accountUpdatedEvent)
        };

        await SendNotificationAsync(notification, cancellationToken);
    }

    private async Task SendNotificationAsync(AccountNotification notification, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(_settings.NotificationEndpoint))
        {
            _logger.LogWarning("Endpoint de notificación no configurado. Omitiendo envío.");
            return;
        }

        try
        {
            var client = _httpClientFactory.CreateClient("NotificationClient");
            
            var response = await _resiliencePipeline.ExecuteAsync(
                async ct => await client.PostAsJsonAsync(_settings.NotificationEndpoint, notification, ct),
                cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Notificación {EventType} enviada exitosamente para cuenta {AccountId}",
                    notification.EventType, notification.AccountId);
            }
            else
            {
                _logger.LogWarning("Envío de notificación {EventType} falló con status {StatusCode}",
                    notification.EventType, response.StatusCode);
            }
        }
        catch (BrokenCircuitException ex)
        {
            _logger.LogError(ex, "Circuit breaker abierto. No se pudo enviar la notificación {EventType}",
                notification.EventType);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al enviar notificación {EventType} para cuenta {AccountId}",
                notification.EventType, notification.AccountId);
            throw;
        }
    }
}

public interface INotificationService
{
    Task NotifyAccountCreatedAsync(AccountCreatedEvent accountCreatedEvent, CancellationToken cancellationToken = default);
    Task NotifyAccountUpdatedAsync(AccountUpdatedEvent accountUpdatedEvent, CancellationToken cancellationToken = default);
}

public class AccountNotification
{
    public string EventType { get; set; } = string.Empty;
    public Guid AccountId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public DateTime Timestamp { get; set; }
    public string Payload { get; set; } = string.Empty;
}

public class NotificationSettings
{
    public string NotificationEndpoint { get; set; } = string.Empty;
    public int MaxRetryAttempts { get; set; } = 3;
    public int RetryDelayMs { get; set; } = 1000;
    public double CircuitBreakerFailureRatio { get; set; } = 0.5;
    public int CircuitBreakerSamplingDurationSeconds { get; set; } = 30;
    public int CircuitBreakerMinimumThroughput { get; set; } = 5;
    public int CircuitBreakerBreakDurationSeconds { get; set; } = 60;
}