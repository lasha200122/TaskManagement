namespace TaskManagement.Application.Common.Interfaces.Managers;

public interface IRedisManager
{
    Task<bool> SetAsync(string key, object value);
    Task<bool> SetAsync(string key, object value, TimeSpan expirationTime);
    Task<bool> DeleteByPatternAsync(string pattern);
    Task<bool> DeleteAsync(string key);
    Task<T?> GetAsync<T>(string key, TimeSpan expirationTime);
    Task<T?> GetAsync<T>(string key);
    Task<bool> UpdateAsync(string key, object value, TimeSpan expirationTime);
}