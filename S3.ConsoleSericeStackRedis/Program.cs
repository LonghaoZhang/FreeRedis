using ServiceStack.Redis;
using System;

namespace S3.ConsoleSericeStackRedis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            //host:1?Client=nunit&Password=erpredis.123&Db=0&Ssl=true&ConnectTimeout=2&SendTimeout=3&ReceiveTimeout=4&IdleTimeOutSecs=5
            // var ConnectionString = "10.201.45.217:6379,max pool size=500,poolsize=100,min pool size=100,retry=1,protocol=3,password=erpredis.123"; 
            //var ConnectionString = "r-bp173rz6i5rvmp2219.redis.rds.aliyuncs.com:6379,password=guanlong:GwLXw9ySn2xjvzC4JwUW,max pool size=500,min pool size=100,retry=1,protocol=3";

            //var connString = $"10.201.45.217:6379?Client=nunit&Password=erpredis.123&Db=0&ConnectTimeout=2&SendTimeout=3&ReceiveTimeout=4&IdleTimeOutSecs=5";
            //var connString = $"10.230.38.224:6379?Client=nunit&Password=erpredis.123&Db=0&ConnectTimeout=2&SendTimeout=3&ReceiveTimeout=4&IdleTimeOutSecs=5";
            //var connString = $"192.168.48.123:6379?Client=nunit&Db=0&ConnectTimeout=5&SendTimeout=3&ReceiveTimeout=4&IdleTimeOutSecs=5";
            var connString = $"r-bp1hz18g01nhv1smm2pd.redis.rds.aliyuncs.com:6379?Client=nunit&Password=localerp:wYurjNjpgeNvgT5Nsgl&Db=0&ConnectTimeout=5&SendTimeout=3&ReceiveTimeout=4&IdleTimeOutSecs=5";
            while (true)
            {
                try
                {
                    var where = Console.ReadLine();
                    if (where == "1")
                    {
                        RedisClient client = new RedisClient("192.168.48.123", 6379);
                        client.Set("name", "gl");
                        var a = client.Get<string>("name");
                        Console.WriteLine(a);
                    }
                    else if (where == "2")
                    {
                        RedisClient client = new RedisClient("10.230.38.224", 6379,"erpredis.123",0);
                        //client.Set("name", "gl");
                        var a = client.Get<string>("name");
                        Console.WriteLine(a);
                        GC.Collect();
                    }
                    else if (where == "3")
                    {
                        RedisClient client = new RedisClient("10.201.45.217", 6379, "erpredis.123", 0);
                        //client.Set("name", "gl");
                        var a = client.Get<string>("name");
                        Console.WriteLine(a);
                    }
                    else if (where == "4")
                    {
                        RedisClient client = new RedisClient("r-bp173rz6i5rvmp2219.redis.rds.aliyuncs.com", 6379, "guanlong:GwLXw9ySn2xjvzC4JwUW", 0);
                        //client.Set("name", "gl");
                        var a = client.Get<string>("name");
                        Console.WriteLine(a);
                    }
                    else if (where == "5")
                    {
                        RedisClient client = new RedisClient("r-bp1hz18g01nhv1smm2pd.redis.rds.aliyuncs.com", 6379, "localerp:wYurjNjpgeNvgT5Nsgl", 0);
                        //client.Set("name", "gl");
                        var a = client.Get<string>("name");
                        Console.WriteLine(a);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"异常：{ex.Message}");
                }
            }
        }

        public class p
        {
            public string name { get; set; } = "gl";
        }


    }
}
