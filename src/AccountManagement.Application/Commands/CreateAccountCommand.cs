using AccountManagement.Application.DTOs;
using AccountManagement.Common.Exceptions;
using AccountManagement.Domain.Entities;
using AccountManagement.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace AccountManagement.Application.Commands;

public record CreateAccountCommand(
    Guid UserId,
    string AccountNumber,
    string AccountType,
    decimal InitialBalance
) : IRequest<AccountDto>;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("El identificador de usuario es requerido");

        RuleFor(x => x.AccountNumber)
            .NotEmpty()
            .WithMessage("El número de cuenta es requerido")
            .Matches(@"^[0-9]{10,20}$")
            .WithMessage("El número de cuenta debe contener solo dígitos y tener entre 10 y 20 caracteres");

        RuleFor(x => x.AccountType)
            .NotEmpty()
            .WithMessage("El tipo de cuenta es requerido")
            .Must(BeValidAccountType)
            .WithMessage("El tipo de cuenta debe ser: CuentaCorriente, CuentaAhorros o CuentaInversion");

        RuleFor(x => x.InitialBalance)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El saldo inicial no puede ser negativo")
            .When(x => x.InitialBalance > 0)
            .LessThanOrEqualTo(1000000m)
            .WithMessage("El saldo inicial no puede exceder 1,000,000");
    }

    private bool BeValidAccountType(string accountType)
    {
        var validTypes = new[] { "CuentaCorriente", "CuentaAhorros", "CuentaInversion" };
        return validTypes.Contains(accountType);
    }
}

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<CreateAccountCommandHandler> _logger;

    public CreateAccountCommandHandler(
        IAccountRepository accountRepository,
        ILogger<CreateAccountCommandHandler> logger)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando creación de cuenta para usuario {UserId}", request.UserId);

        var accountNumberExists = await _accountRepository.AccountNumberExistsAsync(
            request.AccountNumber, 
            cancellationToken);

        if (accountNumberExists)
        {
            _logger.LogWarning("El número de cuenta {AccountNumber} ya existe", request.AccountNumber);
            throw new AccountException($"El número de cuenta {request.AccountNumber} ya está en uso");
        }

        var account = new Account(
            request.UserId,
            request.AccountNumber,
            null);

        if (request.InitialBalance > 0)
        {
            account.Deposit(request.InitialBalance);
        }

        await _accountRepository.AddAsync(account, cancellationToken);

        _logger.LogInformation("Cuenta {AccountId} creada exitosamente para usuario {UserId}", 
            account.Id, 
            request.UserId);

        return MapToDto(account);
    }

    private static AccountDto MapToDto(Account account)
    {
        return new AccountDto
        {
            Id = account.Id,
            UserId = account.UserId,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            Status = account.Status.ToString(),
            CreatedAt = account.CreatedAt,
            UpdatedAt = account.UpdatedAt
        };
    }
}