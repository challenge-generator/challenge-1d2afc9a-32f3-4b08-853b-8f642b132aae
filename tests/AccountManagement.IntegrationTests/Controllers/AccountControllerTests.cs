using Xunit;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AccountManagement.Api.Controllers;
using AccountManagement.Application.Commands;
using AccountManagement.Application.Queries;
using AccountManagement.Common.DTOs;
using AccountManagement.Common.Exceptions;
using AccountManagement.Domain.Entities;

namespace AccountManagement.IntegrationTests.Controllers;

public class AccountControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<ILogger<AccountController>> _loggerMock;
    private readonly AccountController _controller;

    public AccountControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<AccountController>>();
        _controller = new AccountController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task CreateAccount_ValidRequest_ReturnsCreatedResult()
    {
        var request = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-TEST-001",
            InitialDeposit = 1000.00m
        };

        var expectedDto = new AccountDto
        {
            Id = Guid.NewGuid(),
            AccountNumber = request.AccountNumber,
            Balance = request.InitialDeposit,
            Status = AccountStatus.Active
        };

        _mediatorMock
            .Setup(x => x.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedDto);

        var result = await _controller.CreateAccount(request, CancellationToken.None);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.NotNull(createdResult);
        var returnValue = Assert.IsType<AccountDto>(createdResult.Value);
        Assert.Equal(expectedDto.AccountNumber, returnValue.AccountNumber);
    }

    [Fact]
    public async Task CreateAccount_RepositoryThrowsException_ReturnsBadRequest()
    {
        var request = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-DUPLICATE",
            InitialDeposit = 500.00m
        };

        _mediatorMock
            .Setup(x => x.Send(request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new AccountException("Account number already exists"));

        var result = await _controller.CreateAccount(request, CancellationToken.None);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult);
    }

    [Fact]
    public async Task GetAccountById_ExistingId_ReturnsOkResult()
    {
        var accountId = Guid.NewGuid();
        var query = new GetAccountByIdQuery { Id = accountId };
        
        var expectedDto = new AccountDto
        {
            Id = accountId,
            AccountNumber = "ACC-GET-001",
            Balance = 2500.00m,
            Status = AccountStatus.Active
        };

        _mediatorMock
            .Setup(x => x.Send(query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedDto);

        var result = await _controller.GetAccountById(accountId, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult);
        var returnValue = Assert.IsType<AccountDto>(okResult.Value);
        Assert.Equal(accountId, returnValue.Id);
    }

    [Fact]
    public async Task GetAccountById_NonExistingId_ReturnsNotFound()
    {
        var accountId = Guid.NewGuid();
        var query = new GetAccountByIdQuery { Id = accountId };

        _mediatorMock
            .Setup(x => x.Send(query, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new AccountNotFoundException(accountId));

        var result = await _controller.GetAccountById(accountId, CancellationToken.None);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateAccount_ValidRequest_ReturnsOkResult()
    {
        var accountId = Guid.NewGuid();
        var request = new UpdateAccountCommand
        {
            Id = accountId,
            NewBalance = 5000.00m
        };

        var expectedDto = new AccountDto
        {
            Id = accountId,
            AccountNumber = "ACC-UPDATE-001",
            Balance = request.NewBalance,
            Status = AccountStatus.Active
        };

        _mediatorMock
            .Setup(x => x.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedDto);

        var result = await _controller.UpdateAccount(accountId, request, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult);
        var returnValue = Assert.IsType<AccountDto>(okResult.Value);
        Assert.Equal(expectedDto.Balance, returnValue.Balance);
    }

    [Fact]
    public async Task GetAccountById_InvalidId_ReturnsBadRequest()
    {
        var invalidId = Guid.Empty;

        var result = await _controller.GetAccountById(invalidId, CancellationToken.None);

        Assert.IsType<BadRequestResult>(result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public async Task CreateAccount_InvalidInitialDeposit_ReturnsBadRequest(decimal invalidAmount)
    {
        var request = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-INVALID",
            InitialDeposit = invalidAmount
        };

        _mediatorMock
            .Setup(x => x.Send(request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ArgumentException("Initial deposit must be positive"));

        var result = await _controller.CreateAccount(request, CancellationToken.None);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteAccount_ExistingId_ReturnsNoContent()
    {
        var accountId = Guid.NewGuid();
        var request = new UpdateAccountCommand
        {
            Id = accountId,
            NewBalance = 0
        };

        _mediatorMock
            .Setup(x => x.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AccountDto { Id = accountId, Status = AccountStatus.Closed });

        var result = await _controller.CloseAccount(accountId, CancellationToken.None);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task CreateAccount_MediatorThrowsUnhandledException_Returns500()
    {
        var request = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-ERROR",
            InitialDeposit = 100.00m
        };

        _mediatorMock
            .Setup(x => x.Send(request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database connection failed"));

        var result = await _controller.CreateAccount(request, CancellationToken.None);

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objectResult.StatusCode);
    }
}