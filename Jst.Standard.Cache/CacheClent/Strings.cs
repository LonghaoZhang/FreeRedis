using FreeRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jst.Standard.Cache
{
    public partial class CacheClient<T>: BaseRedis,ICacheClient<T>
    {
        public long ClientId()
        {
            return Client.ClientId();
        }
        public T Get(string key)
        {           
            key = $"{ObjectCachePrefixKey}:{key}";
            return Client.Get<T>(key);
        }
        public string GetString(string key)
        {
            return Client.Get(key);
        }
        public T Get(string key, Func<string, T> data, TimeSpan timeout = default)
        {
            key = $"{ObjectCachePrefixKey}:{key}";
            var lockKey = $"redis_get_set_key_{key}";
            timeout = default == timeout ? CacheService<T>.CacheSettings.RedisKeyTimeOut : timeout;
            lock (lockKey)
            {
                var t = Client.Get<T>(key);
                if (t == null)
                {
                    T tData = data.Invoke(key);
                    if (tData != null)
                    {
                        Client.Set<T>(key, tData, timeout);
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
            return Client.Del(keys);
        }
    }
}
