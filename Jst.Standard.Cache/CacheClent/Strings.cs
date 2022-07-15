using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jst.Standard.Cache
{
    public partial class CacheClient<T>: ICacheClient<T>
    {
        public T Get(string key)
        {
            return redisClient.Get<T>(key);
        }
        public T Get(string key, Func<string, T> data, TimeSpan timeout = default)
        {
            key = $"{ObjectCachePrefixKey}:{key}";
            var lockKey = $"redis_get_set_key_{key}";
            timeout = default == timeout ? CacheService<T>.CacheSettings.RedisKeyTimeOut : timeout;
            lock (lockKey)
            {
                var t = redisClient.Get<T>(key);
                if (t == null)
                {
                    T tData = data.Invoke(key);
                    if (tData != null)
                    {
                        redisClient.Set<T>(key, tData, timeout);
                        return tData;
                    }
                }
                return t;
            }
        }
        public long Del(params string[] keys)
        {
            for (var i = 0; i < keys.Length; i++)
            {
                keys[i] = $"{ObjectCachePrefixKey}:{keys[i]}";
            }
            return redisClient.Del(keys);
        }
    }
}
