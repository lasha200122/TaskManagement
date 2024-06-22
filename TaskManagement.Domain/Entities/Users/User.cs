namespace TaskManagement.Domain.Entities.Users;

public class User : Entity
{
    public string FullName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public string PasswordSalt { get; private set; } = null!;

    private List<Assignment> _assignments = new();
    public IReadOnlyCollection<Assignment> Assignments => _assignments;

    public User() {}

    public User(
        Guid id,
        string fullName,
        string email,
        string passwordHash,
        string passwordSalt) : base(id, DateTimeOffset.Now) 
    {
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }

    private void Validate(User user) 
    {
        if (string.IsNullOrEmpty(user.FullName)) throw new Exception("Invalid fullname");

        if (string.IsNullOrEmpty(user.Email)) throw new Exception("Invalid email");

        if (string.IsNullOrEmpty(user.PasswordHash)) throw new Exception("Invalid password hash");

        if (string.IsNullOrEmpty(user.PasswordSalt)) throw new Exception("Invalid password salt");
    }
}
