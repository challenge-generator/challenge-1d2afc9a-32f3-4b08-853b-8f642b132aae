using Xunit;
using Moq;
using MediatR;
using AccountManagement.Application.Commands;
using AccountManagement.Application.Handlers;
using AccountManagement.Domain.Entities;
using AccountManagement.Domain.Repositories;
using AccountManagement.Common.Exceptions;

namespace AccountManagement.UnitTests.Handlers;

public class CreateAccountCommandHandlerTests
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CreateAccountCommandHandler _handler;

    public CreateAccountCommandHandlerTests()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _mediatorMock = new Mock<IMediator>();
        _handler = new CreateAccountCommandHandler(
            _accountRepositoryMock.Object,
            _mediatorMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_CreatesAccountSuccessfully()
    {
        var command = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-2024-001",
            InitialDeposit = 1000.00m
        };

        var user = new User("Test User", "test@example.com", UserRole.Client);
        
        _accountRepositoryMock
            .Setup(x => x.AccountNumberExistsAsync(command.AccountNumber, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _accountRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _mediatorMock
            .Setup(x => x.Publish(It.IsAny<AccountCreatedEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(command.AccountNumber, result.AccountNumber);
        Assert.Equal(command.InitialDeposit, result.Balance);
        _accountRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()), Times.Once);
        _mediatorMock.Verify(x => x.Publish(It.IsAny<AccountCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_DuplicateAccountNumber_ThrowsAccountException()
    {
        var command = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-2024-DUPLICATE",
            InitialDeposit = 500.00m
        };

        _accountRepositoryMock
            .Setup(x => x.AccountNumberExistsAsync(command.AccountNumber, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        await Assert.ThrowsAsync<AccountException>(() => _handler.Handle(command, CancellationToken.None));
        
        _accountRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory]
    [InlineData(-100.00)]
    [InlineData(-0.01)]
    [InlineData(-1000.50)]
    public async Task Handle_NegativeInitialDeposit_ThrowsArgumentException(decimal invalidAmount)
    {
        var command = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-2024-002",
            InitialDeposit = invalidAmount
        };

        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ValidCommand_PublishesAccountCreatedEvent()
    {
        var command = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-2024-EVENT",
            InitialDeposit = 2500.00m
        };

        _accountRepositoryMock
            .Setup(x => x.AccountNumberExistsAsync(command.AccountNumber, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _accountRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        _mediatorMock.Verify(
            x => x.Publish(
                It.Is<AccountCreatedEvent>(e => e.AccountNumber == command.AccountNumber),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_EmptyAccountNumber_ThrowsArgumentException()
    {
        var command = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = string.Empty,
            InitialDeposit = 100.00m
        };

        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_EmptyUserId_ThrowsArgumentException()
    {
        var command = new CreateAccountCommand
        {
            UserId = Guid.Empty,
            AccountNumber = "ACC-2024-003",
            InitialDeposit = 100.00m
        };

        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Theory]
    [InlineData(0.00)]
    [InlineData(0.01)]
    [InlineData(999999.99)]
    public async Task Handle_ValidAmounts_DoesNotThrow(decimal validAmount)
    {
        var command = new CreateAccountCommand
        {
            UserId = Guid.NewGuid(),
            AccountNumber = "ACC-2024-VALID",
            InitialDeposit = validAmount
        };

        _accountRepositoryMock
            .Setup(x => x.AccountNumberExistsAsync(command.AccountNumber, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _accountRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _mediatorMock
            .Setup(x => x.Publish(It.IsAny<AccountCreatedEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);
        Assert.NotNull(result);
    }
}