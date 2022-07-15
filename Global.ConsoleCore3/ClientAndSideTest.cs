using System;
using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;

namespace Global.ConsoleCore3
{
    public class ClientAndSideTest:BaseClient
    {
        [Benchmark]
        public void OnlyRedis()
        {
            for (var i = 0; i < count; i++)
            {
                var val = cache.Get(i.ToString(), _ => GetLgDBData(i));
            }
        }
        [Benchmark]
        public void ClientAndSide()
        {
            for (var i = 0; i < count; i++)
            {
                var val = cacheCs.Get(i.ToString(), _ => GetDBData(i));
            }
        }
    }
}
