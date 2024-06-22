namespace TaskManagement.Infrastructure.Managers;

public class SessionManager<T> : SessionBaseManager
{
    private readonly IRedisManager _redisManager;

    public SessionManager(IRedisManager redisManager)
    {
        _redisManager = redisManager;
    }

    public async Task<T?> GetSessionAsync(string key)
    {
        return await _redisManager.GetAsync<T>(key, ExpirationTime);
    }

    public async Task<bool> SetSessionAsync(string key, T data)
    {
        return await _redisManager.SetAsync(key, data!, ExpirationTime);
    }

    public async Task<bool> DeleteSessionAsync(string prefix, string sessionId)
    {
        var pattern = prefix + sessionId + "*";
        return await _redisManager.DeleteByPatternAsync(pattern);
    }

    public async Task<bool> DeleteSessionInfoAsync(string key)
    {
        return await _redisManager.DeleteAsync(key);
    }

    public async Task<bool> SessionIsActive(string key)
    {
        var value = await _redisManager.GetAsync<T>(key);
        return value != null;
    }

    public async Task<bool> UpdateSessionAsync(string key, T data)
    {
        return await _redisManager.UpdateAsync(key, data!, ExpirationTime);
    }
}

