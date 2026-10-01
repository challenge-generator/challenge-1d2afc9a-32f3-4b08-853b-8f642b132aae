using AccountManagement.Application.DTOs;
using AccountManagement.Common.Exceptions;
using AccountManagement.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace AccountManagement.Application.Queries;

public record GetAccountByIdQuery(Guid AccountId) : IRequest<AccountDto>;

public class GetAccountByIdQueryValidator : AbstractValidator<GetAccountByIdQuery>
{
    public GetAccountByIdQueryValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty()
            .WithMessage("El identificador de cuenta es requerido");
    }
}

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDto>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<GetAccountByIdQueryHandler> _logger;

    public GetAccountByIdQueryHandler(
        IAccountRepository accountRepository,
        ILogger<GetAccountByIdQueryHandler> logger)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<AccountDto> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Consultando cuenta {AccountId}", request.AccountId);

        var account = await _accountRepository.GetByIdAsync(request.AccountId, cancellationToken);

        if (account == null)
        {
            _logger.LogWarning("Cuenta {AccountId} no encontrada", request.AccountId);
            throw new AccountNotFoundException($"La cuenta con ID {request.AccountId} no fue encontrada");
        }

        _logger.LogInformation("Cuenta {AccountId} encontrada exitosamente", request.AccountId);

        return MapToDto(account);
    }

    private static AccountDto MapToDto(AccountManagement.Domain.Entities.Account account)
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