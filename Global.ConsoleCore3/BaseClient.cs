using FreeRedis;
using Jst.Standard.Cache;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace Global.ConsoleCore3
{
    public class BaseClient
    {
        public static int count = 1;
        public static IDatabase redis = ConnectionMultiplexer.Connect("192.168.48.123:6379").GetDatabase(0);
        public static IDatabase LazyRedis = new Lazy<IDatabase>(() => { 
          return ConnectionMultiplexer.Connect("192.168.48.123:6379").GetDatabase(0);
        }).Value; 
        public static RedisClient lazyFree = new Lazy<RedisClient>(() =>
        {
            var r = new RedisClient("192.168.48.123:6379");
            r.Serialize = obj => JsonConvert.SerializeObject(obj);
            r.Deserialize = (json, type) => JsonConvert.DeserializeObject(json, type);
            return r;
        }).Value;
        public static RedisClient free = new RedisClient("192.168.48.123:6379,poolsize=100,min pool size=100")
        {
            Deserialize = (json, type) => JsonConvert.DeserializeObject(json, type),
            Serialize = obj => JsonConvert.SerializeObject(obj)
        };
        public static ICacheClient<Lg> cache = CacheService<Lg>.CreateClient(CacheStoreType.OnlyReids, "CLG");
        public static ICacheClient<Lg> lazyCache = new Lazy<ICacheClient<Lg>>(() =>
        {
           return  CacheService<Lg>.CreateClient(CacheStoreType.OnlyReids, "CLG");
        }).Value;
        public static ICacheClient<Order> cacheCs = CacheService<Order>.CreateClient(CacheStoreType.LoaclAndRedis, "Order");

        public static Order GetDBData(int id)
        {           
            return new Order() { ID = id };
        }
        public static Lg GetLgDBData(int id)
        {
            return new Lg() { ID = id };
        }
        public static string GetDBStringData(int id)
        {
            return "{order.id=" + id + "}";
        }
    }
}
