using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace S3.ConsoleCore3
{
    public class DelDicTest
    {
        static DelDicTest()
        {
            Console.WriteLine("--------------------------------------------------------------------------");
            ConcurrentDictionary<string, object> dic = new ConcurrentDictionary<string, object>();
            for (var i = 0; i < 100 * 10000; i++)
            {
                dic.TryAdd("k" + i, i);
            }
            BigData = dic;
            HashSet<string> tt = new HashSet<string>();
            for (var i = 0; i < 100; i++)
            {
                tt.Add("k" + i);
            }
            LittleData = tt;
        }
        static ConcurrentDictionary<string, object> BigData { get; set; }
        static HashSet<string> LittleData { get; set; }

        [Benchmark]
        public void DeleteBig()
        {
            BigData.TryRemove("iixsf", out var old);
        }
        [Benchmark]
        public void DeleteLittle()
        {
            foreach (var vk in LittleData)
            {
                "iixsf".StartsWith(vk);
            }
        }

    }
}
