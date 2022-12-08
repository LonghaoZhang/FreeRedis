using FreeRedis;

namespace Jst.Standard.Cache.Settings
{
    internal class LoadConfig
    {
        const string ConnectionKeyStr = "ddd";

        public static ConnectionStringBuilder[] Bulider()
        {

            //var ConnectionString = "192.168.48.123:6379,max pool size=500,poolsize=100,min pool size=100,retry=1,protocol=3";
           var ConnectionString = "10.201.45.217:6379,max pool size=500,poolsize=100,min pool size=100,retry=1,protocol=3,password=erpredis.123"; 
           // var ConnectionString = "r-bp173rz6i5rvmp2219.redis.rds.aliyuncs.com:6379,password=guanlong:GwLXw9ySn2xjvzC4JwUW,max pool size=500,min pool size=100,retry=1,protocol=3";
            //var ConnectionString= System.Configuration.ConfigurationManager.AppSettings[ConnectionKeyStr];
            ConnectionStringBuilder[] builders =  { (ConnectionStringBuilder)ConnectionString };
            return builders;
        }
    }
}
