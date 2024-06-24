using FreeRedis;
using Jst.Standard.Cache.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Jst.Standard.Cache
{
    public partial class CacheClient<T>
    {
        public CacheClient(string _objectCachePrefixKey)
        {
            ObjectCachePrefixKey = _objectCachePrefixKey;
        }
        public string ObjectCachePrefixKey { get; set; }
    }

    public class BaseRedis
    {
        internal static Lazy<RedisClient> _cliLazy = new Lazy<RedisClient>(() =>
        {
            var r = new RedisClient(LoadConfig.Bulider()); //redis 6.0
            r.Serialize = obj => JsonConvert.SerializeObject(obj);
            r.Deserialize = (json, type) => JsonConvert.DeserializeObject(json, type);
            return r;
        });
       
        public static RedisClient Client => _cliLazy.Value;
        public static bool LoadedClientSideCaching { get; set; }
    }
    
}
