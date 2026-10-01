using Microsoft.AspNetCore.Mvc;
using MediatR;
using AccountManagement.Application.Commands;
using AccountManagement.Application.Queries;
using AccountManagement.Common.DTOs;
using AccountManagement.Common.Exceptions;
using System.Security.Claims;

namespace AccountManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IMediator mediator, ILogger<AccountController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAccount(
        [FromBody] CreateAccountCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Recibida solicitud para crear cuenta: {AccountNumber}", 
                command.AccountNumber);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Detail = "Token de autenticación inválido o ausente"
                });
            }

            var result = await _mediator.Send(command, cancellationToken);

            _logger.LogInformation("Cuenta creada exitosamente: {AccountId}", result.Id);
            return CreatedAtAction(nameof(GetAccountById), new { id = result.Id }, result);
        }
        catch (AccountNotFoundException ex)
        {
            _logger.LogWarning(ex, "Recurso no encontrado: {Message}", ex.Message);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = ex.Message
            });
        }
        catch (AccountException ex)
        {
            _logger.LogWarning(ex, "Error de negocio: {Message}", ex.Message);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error interno al crear cuenta");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "Error interno del servidor"
            });
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAccountById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Consultando cuenta: {AccountId}", id);

            var query = new GetAccountByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
            {
                _logger.LogWarning("Cuenta no encontrada: {AccountId}", id);
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Not Found",
                    Detail = $"Cuenta con ID {id} no encontrada"
                });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al consultar cuenta: {AccountId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "Error interno del servidor"
            });
        }
    }

    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<AccountDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAccountsByUserId(
        Guid userId, 
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Consultando cuentas para usuario: {UserId}", userId);

            var query = new GetAccountsByUserIdQuery(userId);
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al consultar cuentas de usuario: {UserId}", userId);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "Error interno del servidor"
            });
        }
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAccount(
        Guid id,
        [FromBody] UpdateAccountCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Actualizando cuenta: {AccountId}", id);

            if (id != command.AccountId)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "El ID del path no coincide con el ID del cuerpo de la solicitud"
                });
            }

            var result = await _mediator.Send(command, cancellationToken);

            _logger.LogInformation("Cuenta actualizada exitosamente: {AccountId}", id);
            return Ok(result);
        }
        catch (AccountNotFoundException ex)
        {
            _logger.LogWarning(ex, "Cuenta no encontrada para actualizar: {Message}", ex.Message);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = ex.Message
            });
        }
        catch (AccountException ex)
        {
            _logger.LogWarning(ex, "Error de negocio al actualizar cuenta: {Message}", ex.Message);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error interno al actualizar cuenta: {AccountId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "Error interno del servidor"
            });
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAccount(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Eliminando cuenta: {AccountId}", id);

            var command = new DeleteAccountCommand(id);
            await _mediator.Send(command, cancellationToken);

            _logger.LogInformation("Cuenta eliminada exitosamente: {AccountId}", id);
            return NoContent();
        }
        catch (AccountNotFoundException ex)
        {
            _logger.LogWarning(ex, "Cuenta no encontrada para eliminar: {Message}", ex.Message);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = ex.Message
            });
        }
        catch (AccountException ex)
        {
            _logger.LogWarning(ex, "Error de negocio al eliminar cuenta: {Message}", ex.Message);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error interno al eliminar cuenta: {AccountId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "Error interno del servidor"
            });
        }
    }

    [HttpPost("{id:guid}/deposit")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Deposit(
        Guid id,
        [FromBody] DepositCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Depósito en cuenta: {AccountId}, monto: {Amount}", id, command.Amount);

            if (id != command.AccountId)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "El ID del path no coincide con el ID del cuerpo de la solicitud"
                });
            }

            var result = await _mediator.Send(command, cancellationToken);

            _logger.LogInformation("Depósito exitoso en cuenta: {AccountId}", id);
            return Ok(result);
        }
        catch (AccountNotFoundException ex)
        {
            _logger.LogWarning(ex, "Cuenta no encontrada para depósito: {Message}", ex.Message);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = ex.Message
            });
        }
        catch (AccountException ex)
        {
            _logger.LogWarning(ex, "Error de negocio en depósito: {Message}", ex.Message);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error interno en depósito: {AccountId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "Error interno del servidor"
            });
        }
    }

    [HttpPost("{id:guid}/withdraw")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Withdraw(
        Guid id,
        [FromBody] WithdrawCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Retiro de cuenta: {AccountId}, monto: {Amount}", id, command.Amount);

            if (id != command.AccountId)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "El ID del path no coincide con el ID del cuerpo de la solicitud"
                });
            }

            var result = await _mediator.Send(command, cancellationToken);

            _logger.LogInformation("Retiro exitoso de cuenta: {AccountId}", id);
            return Ok(result);
        }
        catch (AccountNotFoundException ex)
        {
            _logger.LogWarning(ex, "Cuenta no encontrada para retiro: {Message}", ex.Message);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = ex.Message
            });
        }
        catch (AccountException ex)
        {
            _logger.LogWarning(ex, "Error de negocio en retiro: {Message}", ex.Message);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error interno en retiro: {AccountId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "Error interno del servidor"
            });
        }
    }
}