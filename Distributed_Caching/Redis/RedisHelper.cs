using StackExchange.Redis;

namespace Distributed_Caching.Redis
{
    public class RedisHelper : IRedisHelper
    {
        private IDatabase db
        {
            get
            {
                ConfigurationOptions conf = new ConfigurationOptions
                {
                    EndPoints = { "localhost:1453" },
                    User = "yourUsername",
                    Password = "yourPassword"
                };
                var redis = ConnectionMultiplexer.Connect(conf);
                return redis.GetDatabase();
            }
        }
        public bool SetString(string key, string value) => db.StringSet(key, value);

        public string? GetString(string key) => db.KeyExists(key) ? db.StringGet(key).ToString() : null;


        public bool RemoveString(string key) => db.KeyExists(key) ? db.KeyDelete(key) : false;

        public bool SetHashEntry(string hashKey, HashEntry[] entries)
        {
            if (string.IsNullOrWhiteSpace(hashKey) || entries == null || entries.Length == 0)
                return false;

            db.HashSet(hashKey, entries);
            return true;
        }

        public HashEntry[] GetHashEntry(string hashKey)
        {
            if (string.IsNullOrWhiteSpace(hashKey))
                return Array.Empty<HashEntry>();

            return db.KeyExists(hashKey)
                ? db.HashGetAll(hashKey)
                : Array.Empty<HashEntry>();
        }
    }
}
