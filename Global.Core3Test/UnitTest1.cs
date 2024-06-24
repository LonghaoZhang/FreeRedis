using FreeRedis;
using NUnit.Framework;
using System.Collections.Concurrent;
using System.IO;

namespace Global.Core3Test
{
    public class Tests
    {
        RedisClient r = new RedisClient("192.168.48.123:6379,max pool size=500,retry=1");
        [SetUp]
        public void Setup()
        {
            //[{z:15.5,j:9,g:20},{d:2.4,h:0.8,g:2},{x:20,d:40,s:4}]
            //{h:0.8,g:2,j:0.7}
            //{h:0.8,g:2,f:0.5}
        }

        [Test]
        public void Test1()
        {
           
          
        }
    }
}