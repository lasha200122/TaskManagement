namespace TaskManagement.Infrastructure.Managers;

public class SessionBaseManager
{
    public static TimeSpan ExpirationTime { get; set; }

    public static async Task<bool> DeleteAsync(string pattern)
    {
        var redis = new RedisManager();
        return await redis.DeleteByPatternAsync(pattern);
    }
}
