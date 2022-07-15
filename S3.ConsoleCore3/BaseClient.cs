using FreeRedis;
using Jst.Standard.Cache;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace S3.ConsoleCore3
{
    public class BaseClient
    {
        public static int count = 1;
        public static CSRedis.CSRedisClient csredis = new CSRedis.CSRedisClient("192.168.48.123:6379,database=2,poolsize=100");
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
        public static ICacheClient<string> cache = CacheService<string>.CreateClient(CacheStoreType.OnlyReids, "UserName");
        public static ICacheClient<string> lazyCache = new Lazy<ICacheClient<string>>(() =>
        {
            return CacheService<string>.CreateClient(CacheStoreType.OnlyReids, "UserName");
        }).Value;
        public static ICacheClient<Order> cacheCs = CacheService<Order>.CreateClient(CacheStoreType.LoaclAndRedis, "OrderCs");
        public static ICacheClient<Order> cacheC = CacheService<Order>.CreateClient(CacheStoreType.OnlyReids, "OrderC");

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
    public class Order
    {
        public int ID { get; set; }
    }
    public class Lg
    {
        public int ID { get; set; }
    }
}
