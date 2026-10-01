using MediatR;
using AccountManagement.Domain.Entities;
using AccountManagement.Domain.Repositories;
using AccountManagement.Application.Commands;
using AccountManagement.Common.DTOs;
using AccountManagement.Common.Exceptions;
using AccountManagement.Domain.Events;

namespace AccountManagement.Application.Handlers;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUserRepository _userRepository;
    private readonly INotificationService _notificationService;
    private readonly IAccountEventStore _eventStore;
    private readonly ILogger<CreateAccountCommandHandler> _logger;

    public CreateAccountCommandHandler(
        IAccountRepository accountRepository,
        IUserRepository userRepository,
        INotificationService notificationService,
        IAccountEventStore eventStore,
        ILogger<CreateAccountCommandHandler> logger)
    {
        _accountRepository = accountRepository;
        _userRepository = userRepository;
        _notificationService = notificationService;
        _eventStore = eventStore;
        _logger = logger;
    }

    public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando creación de cuenta para usuario {UserId}", request.UserId);

        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            _logger.LogWarning("Usuario no encontrado: {UserId}", request.UserId);
            throw new AccountNotFoundException($"Usuario con ID {request.UserId} no encontrado");
        }

        var accountNumberExists = await _accountRepository.AccountNumberExistsAsync(
            request.AccountNumber, cancellationToken);
        if (accountNumberExists)
        {
            _logger.LogWarning("Número de cuenta ya existe: {AccountNumber}", request.AccountNumber);
            throw new AccountException($"El número de cuenta {request.AccountNumber} ya existe");
        }

        var account = new Account(
            userId: request.UserId,
            accountNumber: request.AccountNumber,
            user: user);

        if (request.InitialBalance > 0)
        {
            account.Deposit(request.InitialBalance);
        }

        await _accountRepository.AddAsync(account, cancellationToken);
        await _accountRepository.SaveChangesAsync(cancellationToken);

        var accountCreatedEvent = new AccountCreatedEvent(
            accountId: account.Id,
            accountNumber: account.AccountNumber,
            userId: account.UserId,
            initialBalance: request.InitialBalance,
            timestamp: DateTime.UtcNow);

        await _eventStore.SaveEventAsync(accountCreatedEvent, cancellationToken);

        await _notificationService.SendAccountCreatedNotificationAsync(
            account.Id,
            account.AccountNumber,
            user.Email,
            cancellationToken);

        _logger.LogInformation("Cuenta creada exitosamente: {AccountId}", account.Id);

        return new AccountDto
        {
            Id = account.Id,
            AccountNumber = account.AccountNumber,
            UserId = account.UserId,
            Balance = account.Balance,
            Status = account.Status,
            CreatedAt = account.CreatedAt,
            UserName = user.Name,
            UserEmail = user.Email
        };
    }
}