using FreeRedis;
using Newtonsoft.Json;
using System;

namespace S3.Console48
{
    internal class Program
    {
        static Lazy<RedisClient> _cliLazy1 = new Lazy<RedisClient>(() =>
        {
            var ConnectionString = "10.201.45.217:6379,max pool size=500,poolsize=100,min pool size=100,retry=1,protocol=3,password=erpredis.123";
            ConnectionStringBuilder[] builders = { (ConnectionStringBuilder)ConnectionString };
            var r = new RedisClient(builders); //redis 6.0
            r.Serialize = obj => JsonConvert.SerializeObject(obj);
            r.Deserialize = (json, type) => JsonConvert.DeserializeObject(json, type);
            return r;
        });
        static RedisClient cache1 => _cliLazy1.Value;

        //static Lazy<ICacheClient<string>> _cliLazyCache = new Lazy<ICacheClient<string>>(() =>
        //{
        //    var r = CacheService<string>.CreateClient(CacheStoreType.LoaclAndRedis, "CLG");
        //    return r;
        //});
        //static ICacheClient<string> cache1 => _cliLazyCache.Value;

        //static Lazy<ICacheClient<string>> _cliLazy = new Lazy<ICacheClient<string>>(() =>
        //{
        //    var r = CacheService<string>.CreateClient(CacheStoreType.LoaclAndRedis, "CLG");
        //    return r;
        //});
        //static ICacheClient<string> cache => _cliLazy.Value;

        static void Main(string[] args)
        {
            Console.SetWindowSize(40, 20);
            cache1.UseClientSideCaching(new ClientSideCachingOptions
            {
                Capacity = 100000,
                KeyFilter = k => k.StartsWith("Erp:Auto:CLG"),
                CheckExpired = (k, dt) => DateTime.Now.Subtract(dt) > TimeSpan.FromDays(2)
            });
            //var key = "name";
            while (true)
            {
                
                // Console.WriteLine(_cliLazyCache.IsValueCreated.ToString()+"---"+ _cliLazy.IsValueCreated);
               // Console.WriteLine($"cache-clientid={cache.ClientId()}，val= " + cache.Get(key));
                Console.WriteLine($"free，val="+ cache1.Get("Erp:Auto:CLG:name"));               
                Console.WriteLine(Console.ReadLine());
            }
        }
    }
}
