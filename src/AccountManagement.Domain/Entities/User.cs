using System;
using System.Collections.Generic;

namespace AccountManagement.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private User() { }

    public User(string name, string email, UserRole role)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty", nameof(name));
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be empty", nameof(email));
        if (!email.Contains("@")) throw new ArgumentException("Invalid email format", nameof(email));

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail)) throw new ArgumentException("Email cannot be empty", nameof(newEmail));
        if (!newEmail.Contains("@")) throw new ArgumentException("Invalid email format", nameof(newEmail));

        Email = newEmail;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateRole(UserRole newRole)
    {
        if (Role == newRole) return;

        Role = newRole;
        UpdatedAt = DateTime.UtcNow;
    }
}

public enum UserRole
{
    Customer,
    Employee,
    Admin
}