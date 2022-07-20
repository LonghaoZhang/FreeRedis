using BenchmarkDotNet.Attributes;
using Jst.Standard.Cache;
using System;
using System.Collections.Generic;
using System.Text;

namespace Global.ConsoleCore3
{
    public class TestRedisCache
    {
        static ICacheClient<Order> OrderCache = new Lazy<ICacheClient<Order>>(() => {         
           return CacheService<Order>.CreateClient(CacheStoreType.LoaclAndRedis, "Order");
        }).Value;       
        [Benchmark]
        public void LoaclAndRedis()
        {
           OrderCache.Get("1", _ => GetDBData(1));           
        }
         //[Benchmark]
        public void LoaclAndRedisCreateClient()
        {
            var client = CacheService<Order>.CreateClient(CacheStoreType.LoaclAndRedis, "Order"); 
            client.Get("1", _ => GetDBData(1));
        }
        //[Benchmark]
        public void OnlyReids()
        {
            ICacheClient<Lg> LgCache = CacheService<Lg>.CreateClient(CacheStoreType.OnlyReids, "Lg");
            LgCache.Get("2", _ => GetLgDBData(2));
        }
        //[Benchmark]
        public void OnlyLocal()
        {
            ICacheClient<string> UserNameCache = CacheService<string>.CreateClient(CacheStoreType.OnlyLocal, "UserName");
            UserNameCache.Get("3", _ => GetDBStringData(3));
        }
        static string GetDBStringData(int id)
        {
            return "{user.id=" + id + "}";
        }
        Order GetDBData(int id)
        {
            return new Order() { ID = id };
        }
        static Lg GetLgDBData(int id)
        {
            return new Lg() { ID = id };
        }
    }
}
