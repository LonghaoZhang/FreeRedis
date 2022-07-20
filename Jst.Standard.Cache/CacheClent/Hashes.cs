using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jst.Standard.Cache
{
    public partial class CacheClient<T>: BaseRedis,ICacheClient<T>
    {
        public Dictionary<string, T> HGET(string key, Func<string, Dictionary<string, T>> data, TimeSpan timeout)
        {
            key = $"{ObjectCachePrefixKey}:{key}";
            string lockKey = $"redis_hset_key_{key}";
            lock (lockKey)
            {
                var t = Client.HGetAll<T>(key);
                if (t == null)
                {
                    var tData = data.Invoke(key);
                    if (tData == null)
                    {
                        Client.HSet(key, tData);
                        return tData;
                    }
                }
                return t;
            }
        }
        public T HGet(string key, string filed, Func<string, T> data)
        {
            key = $"{ObjectCachePrefixKey}:{key}";
            string lockKey = $"redis_hset_key_{key}_{filed}";
            lock (lockKey)
            {
                var t = Client.HGet<T>(key, filed);
                if (t == null)
                {
                    T tData = data.Invoke(key);
                    if (tData == null)
                    {
                        Client.HSet(key, filed, tData);
                        return tData;
                    }
                }
                return t;
            }
        }
        public long HDel(string key, params string[] fields)
        {
            key = $"{ObjectCachePrefixKey}:{key}";
            return Client.HDel(key, fields);
        }
    }
}
