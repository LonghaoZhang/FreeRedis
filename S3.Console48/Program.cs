using Jst.Standard.Cache;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace S3.Console48
{
    internal class Program
    {
        public static ICacheClient<string> cache = CacheService<string>.CreateClient(CacheStoreType.OnlyReids, "CLG");
        static void Main(string[] args)
        {
            var key = "name";
            Console.WriteLine(cache.Get(key, _ => { return "gl"; }));
            Console.WriteLine(cache.Get(key, _ => { return "gl"; }));
            Console.WriteLine(cache.Get(key, _ => { return "gl"; }));
            Console.WriteLine(cache.Get(key, _ => { return "gl"; }));
        }       
    }
}
