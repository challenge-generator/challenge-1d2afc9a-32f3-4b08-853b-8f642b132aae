using AccountManagement.Application.DTOs;
using AccountManagement.Common.Exceptions;
using AccountManagement.Domain.Entities;
using AccountManagement.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace AccountManagement.Application.Commands;

public record UpdateAccountCommand(
    Guid AccountId,
    string NewAccountNumber,
    string NewStatus
) : IRequest<AccountDto>;

public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
{
    public UpdateAccountCommandValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty()
            .WithMessage("El identificador de cuenta es requerido");

        RuleFor(x => x.NewAccountNumber)
            .NotEmpty()
            .WithMessage("El número de cuenta es requerido")
            .Matches(@"^[0-9]{10,20}$")
            .WithMessage("El número de cuenta debe contener solo dígitos y tener entre 10 y 20 caracteres");

        RuleFor(x => x.NewStatus)
            .NotEmpty()
            .WithMessage("El estado de cuenta es requerido")
            .Must(BeValidStatus)
            .WithMessage("El estado debe ser: Activa, Inactiva o Cerrada");
    }

    private bool BeValidStatus(string status)
    {
        var validStatuses = new[] { "Activa", "Inactiva", "Cerrada" };
        return validStatuses.Contains(status);
    }
}

public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, AccountDto>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<UpdateAccountCommandHandler> _logger;

    public UpdateAccountCommandHandler(
        IAccountRepository accountRepository,
        ILogger<UpdateAccountCommandHandler> logger)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<AccountDto> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando actualización de cuenta {AccountId}", request.AccountId);

        var account = await _accountRepository.GetByIdAsync(request.AccountId, cancellationToken);

        if (account == null)
        {
            _logger.LogWarning("Cuenta {AccountId} no encontrada", request.AccountId);
            throw new AccountNotFoundException($"La cuenta con ID {request.AccountId} no fue encontrada");
        }

        if (account.Status == AccountStatus.Cerrada)
        {
            _logger.LogWarning("No se puede modificar la cuenta {AccountId} porque está cerrada", request.AccountId);
            throw new AccountException("No se puede modificar una cuenta que está cerrada");
        }

        var accountNumberExists = await _accountRepository.AccountNumberExistsAsync(
            request.NewAccountNumber, 
            cancellationToken);

        if (accountNumberExists && request.NewAccountNumber != account.AccountNumber)
        {
            _logger.LogWarning("El número de cuenta {AccountNumber} ya está en uso", request.NewAccountNumber);
            throw new AccountException($"El número de cuenta {request.NewAccountNumber} ya está en uso");
        }

        if (Enum.TryParse<AccountStatus>(request.NewStatus, out var newStatus))
        {
            if (newStatus == AccountStatus.Cerrada)
            {
                account.Close();
            }
        }

        await _accountRepository.UpdateAsync(account, cancellationToken);

        _logger.LogInformation("Cuenta {AccountId} actualizada exitosamente", request.AccountId);

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