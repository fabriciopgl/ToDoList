using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Application.Users.Domain;

[Table("Users")]
public class User
{
    public int Id { get; init; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; private set; }

    private User(int id, string name, string email, string passwordHash, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = createdAt;
    }

    public static User Create(string name, string email, string passwordHash) =>
        new(0, name, email, passwordHash, DateTime.UtcNow);

    public void Update(string name, string email)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name;

        if (!string.IsNullOrWhiteSpace(email))
            Email = email;

        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
        UpdatedAt = DateTime.UtcNow;
    }
}
