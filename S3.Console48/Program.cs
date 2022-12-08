using FreeRedis;
using Jst.Standard.Cache;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace S3.Console48
{
    internal class Program
    {
        //static Lazy<RedisClient> _cliLazy1 = new Lazy<RedisClient>(() =>
        //{
        //    var ConnectionString = "10.230.61.121:6379,max pool size=500,poolsize=100,min pool size=100,retry=1,protocol=3,password=erpredis.123";
        //    ConnectionStringBuilder[] builders = { (ConnectionStringBuilder)ConnectionString };
        //    var r = new RedisClient(builders); //redis 6.0
        //    r.Serialize = obj => JsonConvert.SerializeObject(obj);
        //    r.Deserialize = (json, type) => JsonConvert.DeserializeObject(json, type);
        //    return r;
        //});
        //static RedisClient cache1 => _cliLazy1.Value;

        static Lazy<ICacheClient<string>> _cliLazy = new Lazy<ICacheClient<string>>(() =>
        {
            Console.WriteLine("--------------------hel----------------");
            var r = CacheService<string>.CreateClient(CacheStoreType.LoaclAndRedis, "Order");
            return r;
        });
        static ICacheClient<string> cache => _cliLazy.Value;

        static Lazy<ICacheClient<string>> _cliLazyCache = new Lazy<ICacheClient<string>>(() =>
        {
            var r = CacheService<string>.CreateClient(CacheStoreType.LoaclAndRedis, "CLG");
            return r;
        });      
        static ICacheClient<string> cache1 => _cliLazyCache.Value;

        static Lazy<ICacheClient<string>> _cliLazyCacheCar = new Lazy<ICacheClient<string>>(() =>
        {
            var r = CacheService<string>.CreateClient(CacheStoreType.OnlyReids, "Car");
            return r;
        });
        static ICacheClient<string> carCache => _cliLazyCacheCar.Value;



        static void Main(string[] args)
        {
            //HashSet<string> filterKeys = new HashSet<string>();
            //filterKeys.Add("Erp:Auto:Order");
            //filterKeys.Add("Erp:Auto:CLG");
            ////Console.SetWindowSize(40, 20);
            //cache1.UseClientSideCaching(new ClientSideCachingOptions
            //{
            //    Capacity = 100000,
            //    KeyFilter = k => KeyFilterStartsWith(filterKeys,k),
            //    CheckExpired = (k, dt) => DateTime.Now.Subtract(dt) > TimeSpan.FromDays(2)
            //});


            while (true)
            {
                var key = "name";
                //Console.WriteLine($"cache，val= " + cache.Get(key));
                Console.WriteLine($"cache，val=" + cache.Get(key));
                //Console.WriteLine($"cache1，val=" + cache1.Get(key));
                //Console.WriteLine($"carCache, val={carCache.Get(key)}");
                Console.WriteLine(Console.ReadLine());
            }
        }

        public static bool KeyFilterStartsWith(HashSet<string> filterKeys, string key)
        {
            foreach (var vk in filterKeys)
            {
                if (key.StartsWith(vk))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
