using StackExchange.Redis;

namespace Distributed_Caching.Redis
{
    public interface IRedisHelper
    {

        bool SetString(string key, string value);
        string? GetString(string key);
        bool RemoveString(string key);
        bool SetHashEntry(string hashKey, HashEntry[] entries);
        HashEntry[] GetHashEntry(string hashKey);





    }
}
