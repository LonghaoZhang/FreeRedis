using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using FreeRedis;
using Jst.Standard.Cache;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Global.ConsoleCore3
{
    public class Program
    {
        static IDatabase redis = ConnectionMultiplexer.Connect("192.168.48.123:6379").GetDatabase(0);
        static RedisClient redisClient = new Lazy<RedisClient>(() =>
        {
            var r = new RedisClient("r-bp1mjy5544toippf8lpd.redis.rds.aliyuncs.com:6379,password=guanlong:GwLXw9ySn2xjvzC4JwUW");
            //var r = new RedisClient("192.168.48.123:6379);
            r.Serialize = obj => JsonConvert.SerializeObject(obj);
            r.Deserialize = (json, type) => JsonConvert.DeserializeObject(json, type);         
            return r;
        }).Value;
        static int count = 10002;
        public static ICacheClient<Order> OrderCache = new Lazy<ICacheClient<Order>>(() =>
        {
            return CacheService<Order>.CreateClient(CacheStoreType.LoaclAndRedis, "Order");
        }).Value;
        public static ICacheClient<Lg> LgCache = new Lazy<ICacheClient<Lg>>(() =>
        {
            return CacheService<Lg>.CreateClient(CacheStoreType.LoaclAndRedis, "CLG");
        }).Value;
        public static ICacheClient<string> UserNameCache = new Lazy<ICacheClient<string>>(() =>
        {
            return CacheService<string>.CreateClient(CacheStoreType.OnlyReids, "UserName");
        }).Value;
        public static ICacheClient<string> UserMobleCache = new Lazy<ICacheClient<string>>(() =>
        {
            return CacheService<string>.CreateClient(CacheStoreType.OnlyLocal, "UserMobile");
        }).Value;
        static void Main(string[] args)
        {
            //Console.WriteLine(OrderCache.Get("gl:name"));
            //Console.WriteLine(OrderCache.Get("gl:name", _ => GetDBData<Order>("gl:name")));
            //Console.WriteLine(UserNameCache.Get("gl:name"));
            //Console.WriteLine(UserNameCache.Get("gl:name"));
            //Console.WriteLine(UserNameCache.Get("gl:name"));
            //Console.WriteLine(UserNameCache.Del("gl:name"));
#if RELEASE
            while (true)
            {
                //TestData();
                //TestData1();
                //Console.WriteLine("--0-cas-1--");
                //TestData2();
                //TestData3();
                //Console.WriteLine("--2--dic--3--");
                //TestData4();
                //TestData5();
                //Console.WriteLine("--4--redis--5---");
                //TestData6();
                //TestData7();
                //Console.WriteLine("--6--local--7---");
                //TestData8();
                //TestData9();
                //Console.WriteLine("--8--cas--9---");
                TestData10();
                //TestData11();
                //Console.WriteLine("--10--free--11---");
                //TestData12();
                //Console.WriteLine("--12--staticExchange----");
                Console.WriteLine("------------------------------------");
            }
#elif DEBUG
            TestDelData();
#endif
        }
        static void TestData12()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                var val = redis.StringGet(i.ToString());
                if (val.IsNullOrEmpty)
                {
                    redis.StringSet(i.ToString(), JsonConvert.SerializeObject(GetLgDBData(i)));
                }
                if (i % 10000 == 0)
                {
                    Console.WriteLine($"val={val},ms={stopwatch.ElapsedMilliseconds}");
                    stopwatch.Restart();
                }
            }
        }
        static void TestData10()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                var val = redisClient.Get(i.ToString());
                if (val == null)
                {
                    redisClient.Set(i.ToString(), GetLgDBData(i));
                }
                if (i % 10000 == 0)
                {
                    Console.WriteLine($"val={val},ms={stopwatch.ElapsedMilliseconds}");
                    stopwatch.Restart();
                }
            }
        }
        static void TestData11()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                var val = redisClient.Get(i.ToString());
                if (val == null)
                {
                    redisClient.Set(i.ToString(), GetLgDBData(i));
                }
                if (i % 10000 == 0)
                {
                    Console.WriteLine($"val={val},ms={stopwatch.ElapsedMilliseconds}");
                    stopwatch.Restart();
                }
            }
        }
        static void TestData8()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                var val = LgCache.Get(i.ToString(), _ => GetLgDBData(i));
                if (i % 10000 == 0)
                {
                    Console.WriteLine($"val={val},ms={stopwatch.ElapsedMilliseconds}");
                    stopwatch.Restart();
                }
            }
        }
        static void TestData9()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                var val = LgCache.Get(i.ToString(), _ => GetLgDBData(i));
                if (i % 10000 == 0)
                {
                    Console.WriteLine($"val={val},ms={stopwatch.ElapsedMilliseconds}");
                    stopwatch.Restart();
                }
            }
        }
        static void TestData6()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                var val = UserMobleCache.Get(i.ToString(), _ => GetDBStringData(i));
                if (i % 10000 == 0)
                {
                    Console.WriteLine($"val={val},ms={stopwatch.ElapsedMilliseconds}");
                    stopwatch.Restart();
                }
            }
        }
        static void TestData7()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                var val = UserMobleCache.Get(i.ToString(), _ => GetDBStringData(i));
                if (i % 10000 == 0)
                {
                    Console.WriteLine($"val={val},ms={stopwatch.ElapsedMilliseconds}");
                    stopwatch.Restart();
                }
            }
        }
        static void TestData4()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                var val = UserNameCache.Get(i.ToString(), _ => GetDBStringData(i));
                if (i % 10000 == 0)
                {
                    Console.WriteLine($"val={val},ms={stopwatch.ElapsedMilliseconds}");
                    stopwatch.Restart();
                }
            }
        }
        static void TestData5()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                var val = UserNameCache.Get(i.ToString(), _ => GetDBStringData(i));
                if (i % 10000 == 0)
                {
                    Console.WriteLine($"val={val},ms={stopwatch.ElapsedMilliseconds}");
                    stopwatch.Restart();
                }
            }
        }
        static Dictionary<string, object> dic = new Dictionary<string, object>();
        static void TestData2()
        {
            dic.Clear();
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                var val = GetDBData(i);
                dic.Add(i.ToString(), JsonConvert.SerializeObject(val));
                if (i % 10000 == 0)
                {
                    Console.WriteLine($"val={val.ID},ms={stopwatch.ElapsedMilliseconds}");
                    stopwatch.Restart();
                }
            }
        }
        static void TestData3()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {

                var o = dic[i.ToString()].ToString();
                var val = JsonConvert.DeserializeObject<Order>(o);
                if (i % 10000 == 0)
                {
                    Console.WriteLine($"val={val.ID},ms={stopwatch.ElapsedMilliseconds}");
                    stopwatch.Restart();
                }
            }
        }
        static void TestData()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                var val = OrderCache.Get(i.ToString(), _ => GetDBData(i));
                if (i % 10000 == 0)
                {
                    Console.WriteLine($"val={val.ID},ms={stopwatch.ElapsedMilliseconds}");
                    stopwatch.Restart();
                }
            }
        }
        static void TestData1()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
            {
                var val = OrderCache.Get(i.ToString(), _ => GetDBData(i));
                if (i % 10000 == 0)
                {
                    Console.WriteLine($"val={val.ID},ms={stopwatch.ElapsedMilliseconds}");
                    stopwatch.Restart();
                }
            }
        }
        static void TestDelData()
        {
            for (var i = 0; i < 10000 * 10000; i++)
            {
                OrderCache.Del(i.ToString());
                LgCache.Del(i.ToString());
                UserNameCache.Del(i.ToString());
                UserMobleCache.Del(i.ToString());
                redisClient.Del(i.ToString());
            }
        }

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
