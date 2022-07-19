using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jst.Standard.Cache
{
    public partial class LocalCacheClient<T> : ICacheClient<T>
    {
        public T Get(string key)
        {
            key = $"{ObjectCachePrefixKey}:{key}";
            return this.LGet(key);
        }
        public string GetString(string key)
        {
            key = $"{ObjectCachePrefixKey}:{key}";
            return this.LGet(key)?.ToString();
        }
        public T Get(string key, Func<string, T> data, TimeSpan timeout)
        {
            key = $"{ObjectCachePrefixKey}:{key}";
            string lockKey = $"redis_hset_key_{key}";
            lock (lockKey)
            {
                var t = this.LGet(key);
                if (t == null)
                {
                    var tData = data.Invoke(key);
                    if (tData == null)
                    {
                        this.LSet_AbsoluteExpire(key, tData, timeout);
                        return tData;
                    }
                }
                return t;
            }
        }
        public long Del(params string[] keys)
        {
            long success = 0;
            foreach (var k in keys) {
                var key = $"{ObjectCachePrefixKey}:{k}";
                this.Remove(key);
                success++;
            }
            return success;
        }
        public T HGet(string key, string filed, Func<string, T> data)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, T> HGET(string key, Func<string, Dictionary<string, T>> data, TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public long HDel(string key, params string[] fields)
        {
            throw new NotImplementedException();
        }
        public long ClientId() 
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, T> hdata = new Dictionary<string, T>();
        
    }
}
