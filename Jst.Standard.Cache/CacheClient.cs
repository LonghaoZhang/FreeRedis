using FreeRedis;
using Jst.Standard.Cache.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Jst.Standard.Cache
{
    public partial class CacheClient<T>
    {
        static Lazy<RedisClient> _cliLazy = new Lazy<RedisClient>(() =>
        {
            var r= new RedisClient(LoadConfig.Bulider());
            r.Serialize = obj => JsonConvert.SerializeObject(obj);
            r.Deserialize = (json, type) => JsonConvert.DeserializeObject(json, type);
            return r;
        });
        internal static RedisClient redisClient => _cliLazy.Value;
        public string ObjectCachePrefixKey { get; set; }
    }
}
