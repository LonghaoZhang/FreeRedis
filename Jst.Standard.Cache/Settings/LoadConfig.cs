using FreeRedis;

namespace Jst.Standard.Cache.Settings
{
    internal class LoadConfig
    {
        const string ConnectionKeyStr = "ddd";

        public static ConnectionStringBuilder Bulider()
        {
#if DEBUG
            var ConnectionString = "192.168.48.123:6379,max pool size=500,retry=1";
#else
            var ConnectionString = "192.168.48.123:6379,poolsize=100,min pool size=100";
            //var ConnectionString = "r-bp1mjy5544toippf8lpd.redis.rds.aliyuncs.com:6379,password=guanlong:GwLXw9ySn2xjvzC4JwUW";
            //var ConnectionString= System.Configuration.ConfigurationManager.AppSettings[ConnectionKeyStr];
#endif
            return  (ConnectionStringBuilder)ConnectionString;
        }
    }
}
