using BenchmarkDotNet.Attributes;

namespace S3.ConsoleCore3
{
    public class ClientAndSideTest : BaseClient
    {
        [Benchmark]
        public void OnlyRedis()
        {
            for (var i = 0; i < count; i++)
            {
                var val = cacheC.Get(i.ToString(), _ => GetDBData(i));
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
