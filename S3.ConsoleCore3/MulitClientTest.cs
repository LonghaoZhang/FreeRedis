using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;

namespace S3.ConsoleCore3
{
    public class MulitClientTest : BaseClient
    {

        //[Benchmark]
        //public void LazyExchangeRedisGetSet()
        //{
        //    for (var i = 0; i < count; i++)
        //    {
        //        var val = LazyRedis.StringGet(i.ToString());
        //        if (val.IsNullOrEmpty)
        //        {
        //            LazyRedis.StringSet(i.ToString(), JsonConvert.SerializeObject(GetLgDBData(i)));
        //        }
        //    }
        //}

        //[Benchmark]
        //public void LazyFreeRedisGetSet()
        //{
        //    for (var i = 0; i < count; i++)
        //    {
        //        var val = lazyFree.Get(i.ToString());
        //        if (val == null)
        //        {
        //            lazyFree.Set(i.ToString(), GetLgDBData(i));
        //        }
        //    }
        //}

        //[Benchmark]
        //public void LazyCacheGetSet()
        //{
        //    for (var i = 0; i < count; i++)
        //    {
        //        var val = lazyCache.Get(i.ToString(), _ => GetLgDBData(i));
        //    }
        //}
        [Benchmark]
        public void ExchangeRedisGetSet()
        {
            for (var i = 0; i < count; i++)
            {
                var val = redis.StringGet(i.ToString());
                if (val.IsNullOrEmpty)
                {
                    redis.StringSet(i.ToString(), JsonConvert.SerializeObject(GetLgDBData(i)));
                }
            }
        }
        [Benchmark]
        public void FreeRedisGetSet()
        {
            for (var i = 0; i < count; i++)
            {
                var val = free.Get(i.ToString());
                if (val == null)
                {
                    free.Set(i.ToString(), GetLgDBData(i));
                }
            }
        }
        [Benchmark]
        public void CSRedisGetSet()
        {
            for (var i = 0; i < count; i++)
            {
                var val = csredis.Get(i.ToString());
                if (val == null)
                {
                    csredis.Set(i.ToString(), GetLgDBData(i));
                }
            }
        }
        [Benchmark]
        public void CacheGetSet()
        {
            for (var i = 0; i < count; i++)
            {
                var val = cache.GetString(i.ToString());
            }
        }


    }
}
