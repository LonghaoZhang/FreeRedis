using FreeRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedsiAnalyze
{
    public static class ConfigAnalyze
    {
        public static void RunConfigAnalyze(this RedisClient cli)
        {
            var result = cli.ConfigGet("*");
            foreach (var kv in result)
            {
                kv.Key.Write("=");
                kv.Value.WriteLineGreen();
            }
        }
    }
}
