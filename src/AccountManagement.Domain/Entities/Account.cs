using System;
using System.Collections.Generic;
using AccountManagement.Domain.Events;

namespace AccountManagement.Domain.Entities;

public class Account
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string AccountNumber { get; private set; }
    public decimal Balance { get; private set; }
    public AccountStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public User User { get; private set; }

    private readonly List<AccountDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<AccountDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private Account() { }

    public Account(Guid userId, string accountNumber, User user)
    {
        if (userId == Guid.Empty) throw new ArgumentException("User ID cannot be empty", nameof(userId));
        if (string.IsNullOrWhiteSpace(accountNumber)) throw new ArgumentException("Account number cannot be empty", nameof(accountNumber));
        if (user == null) throw new ArgumentNullException(nameof(user));

        Id = Guid.NewGuid();
        UserId = userId;
        AccountNumber = accountNumber;
        Balance = 0m;
        Status = AccountStatus.Active;
        CreatedAt = DateTime.UtcNow;
        User = user;

        _domainEvents.Add(new AccountCreatedEvent(Id, userId, accountNumber, CreatedAt));
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Deposit amount must be positive", nameof(amount));
        if (Status != AccountStatus.Active) throw new InvalidOperationException("Cannot deposit to non-active account");

        Balance += amount;
        UpdatedAt = DateTime.UtcNow;

        _domainEvents.Add(new AccountUpdatedEvent(Id, Balance, UpdatedAt.Value));
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Withdrawal amount must be positive", nameof(amount));
        if (Status != AccountStatus.Active) throw new InvalidOperationException("Cannot withdraw from non-active account");
        if (Balance < amount) throw new InvalidOperationException("Insufficient funds");

        Balance -= amount;
        UpdatedAt = DateTime.UtcNow;

        _domainEvents.Add(new AccountUpdatedEvent(Id, Balance, UpdatedAt.Value));
    }

    public void Close()
    {
        if (Status == AccountStatus.Closed) throw new InvalidOperationException("Account is already closed");

        Status = AccountStatus.Closed;
        UpdatedAt = DateTime.UtcNow;

        _domainEvents.Add(new AccountUpdatedEvent(Id, Balance, UpdatedAt.Value));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}

public enum AccountStatus
{
    Active,
    Closed,
    Suspended
}