namespace TaskManagement.Domain.Common.Settings.Authorization;

public class AuthorizationSettings
{
    public string SessionIdPrefix { get; set; } = null!;
    public string SessionIdSalt { get; set; } = null!;
}