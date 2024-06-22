namespace TaskManagement.Domain.Common.Settings.Redis;

public class RedisSettings
{
    public string ConnectionString { get; set; } = null!;
    public int SessionTimeout { get; set; }
    public string Password { get; set; } = null!;
}
