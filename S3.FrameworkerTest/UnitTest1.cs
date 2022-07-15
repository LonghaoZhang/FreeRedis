using FreeRedis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace S3.FrameworkerTest
{
    [TestClass]
    public class UnitTest1
    {
        RedisClient cli = new RedisClient("192.168.48.123:6379,max pool size=500,retry=1");
        [TestMethod]
        public void TestMethod1()
        {           
            var name= cli.Get("name");
            Assert.AreEqual(name,"gl");
        }
        [TestMethod]
        public void ClinetAndSide()
        {
            cli.UseClientSideCaching(new ClientSideCachingOptions
            {
                //Client cache capacity
                Capacity = 3,
                //Filtering rules, which specify which keys can be cached locally
                KeyFilter = key => key.StartsWith("gl"),
                //Check long-term unused cache
                CheckExpired = (key, dt) => DateTime.Now.Subtract(dt) > TimeSpan.FromSeconds(2000)
            });
            var k = "gl:name";
            cli.Set(k,"123");
            cli.Get(k);
            cli.Get(k);
            cli.Get(k);
            Assert.IsTrue(true);

        }
    }
}
