namespace TaskManagement.Infrastructure.Managers;

public class RedisManager : IRedisManager
{
    private static ConnectionMultiplexer ConnectionMultiplexer => _connection.Value;
    private static readonly Lazy<ConnectionMultiplexer> _connection;

    private static bool _throwOnException;
    private static string _formattedHosts;
    private static string _password;

    static RedisManager()
    {
        _connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(
            _password?.Length > 0 ?
            _formattedHosts + $",password={_password},connectTimeout=20000,syncTimeout=20000"
            : _formattedHosts + ",connectTimeout=20000,syncTimeout=20000"));
    }

    public RedisManager() { }

    public static void Initialize(string password, bool throwOnExeption = true, params string[] hosts)
    {
        _password = password;
        _formattedHosts = string.Join(",", hosts);
        _throwOnException = throwOnExeption;
    }

    public async Task<bool> SetAsync(string key, object value)
    {
        try
        {
            var db = ConnectionMultiplexer.GetDatabase();
            var serializedValue = JsonConvert.SerializeObject(value);
            return await db.StringSetAsync(key, serializedValue);
        }
        catch (Exception)
        {
            if (_throwOnException)
            {
                throw;
            }
            return false;
        }
    }

    public async Task<bool> SetAsync(string key, object value, TimeSpan expirationTime)
    {
        try
        {
            var db = ConnectionMultiplexer.GetDatabase();
            var serializedValue = JsonConvert.SerializeObject(value);

            return await db.StringSetAsync(key, serializedValue, expirationTime);
        }
        catch (Exception)
        {
            if (_throwOnException)
            {
                throw;
            }
            return false;
        }
    }

    public async Task<bool> UpdateAsync(string key, object value, TimeSpan expirationTime)
    {
        try
        {
            var db = ConnectionMultiplexer.GetDatabase();
            var serializedValue = JsonConvert.SerializeObject(value);

            return await db.StringSetAsync(key, serializedValue, expirationTime);
        }
        catch (Exception ex)
        {
            if (_throwOnException) throw;
            return false;
        }
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        try
        {
            var db = ConnectionMultiplexer.GetDatabase();
            var value = await db.StringGetAsync(key);
            if (IsRedisResultEmpty(value))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(value!);
        }
        catch (Exception)
        {
            if (_throwOnException)
            {
                throw;
            }
            return default(T);
        }
    }

    public async Task<T?> GetAsync<T>(string key, TimeSpan expirationTime)
    {
        try
        {
            var db = ConnectionMultiplexer.GetDatabase();

            await db.KeyExpireAsync(key, expirationTime);
            var value = await db.StringGetAsync(key);


            if (IsRedisResultEmpty(value))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(value!);
        }
        catch (Exception)
        {
            if (_throwOnException)
            {
                throw;
            }
            return default(T);
        }
    }

    public async Task<bool> DeleteAsync(string key)
    {
        try
        {
            var db = ConnectionMultiplexer.GetDatabase();
            return await db.KeyDeleteAsync(key);
        }
        catch (Exception)
        {
            if (_throwOnException)
            {
                throw;
            }
            return false;
        }
    }

    public async Task<bool> DeleteByPatternAsync(string pattern)
    {
        try
        {
            var endpoints = ConnectionMultiplexer.GetEndPoints();
            var db = ConnectionMultiplexer.GetDatabase();

            foreach (var endpoint in endpoints)
            {
                try
                {
                    var server = ConnectionMultiplexer.GetServer(endpoint);
                    var keys = server.KeysAsync(pattern: pattern);
                    await foreach (var key in keys)
                    {
                        return await db.KeyDeleteAsync(key);
                    }
                }
                catch (Exception)
                {
                    if (_throwOnException)
                    {
                        throw;
                    }
                }

            }
            return true;
        }
        catch (Exception)
        {
            if (_throwOnException)
            {
                throw;
            }
            return false;
        }
    }

    private static bool IsRedisResultEmpty(RedisValue redisValue)
    {
        return !redisValue.HasValue || redisValue.IsNullOrEmpty || redisValue == "[]";
    }

    public void Dispose()
    {
    }
}
