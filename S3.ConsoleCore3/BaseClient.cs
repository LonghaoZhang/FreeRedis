using FreeRedis;
using Jst.Standard.Cache;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace S3.ConsoleCore3
{
    public class BaseClient
    {
        public static int count = 1*10000;
        static ConnectionStringBuilder[] connectionStringBuilder = { (ConnectionStringBuilder)"r-bp173rz6i5rvmp2219.redis.rds.aliyuncs.com:6379,password=guanlong:GwLXw9ySn2xjvzC4JwUW,max pool size=500,min pool size=100,retry=1,protocol=3" };
        public static CSRedis.CSRedisClient csredis = new CSRedis.CSRedisClient("r-bp173rz6i5rvmp2219.redis.rds.aliyuncs.com:6379,password=guanlong:GwLXw9ySn2xjvzC4JwUW,max pool size=500,min pool size=100,retry=1,protocol=3");
        public static IDatabase redis = ConnectionMultiplexer.Connect("r-bp173rz6i5rvmp2219.redis.rds.aliyuncs.com:6379,password=guanlong:GwLXw9ySn2xjvzC4JwUW").GetDatabase(0);
        public static IDatabase LazyRedis = new Lazy<IDatabase>(() => {
            return ConnectionMultiplexer.Connect("r-bp173rz6i5rvmp2219.redis.rds.aliyuncs.com:6379,password=guanlong:GwLXw9ySn2xjvzC4JwUW").GetDatabase(0);
        }).Value;
        public static RedisClient lazyFree = new Lazy<RedisClient>(() =>
        {
            var r = new RedisClient(connectionStringBuilder);
            r.Serialize = obj => JsonConvert.SerializeObject(obj);
            r.Deserialize = (json, type) => JsonConvert.DeserializeObject(json, type);
            return r;
        }).Value;
        
        public static RedisClient free = new RedisClient(connectionStringBuilder)
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
