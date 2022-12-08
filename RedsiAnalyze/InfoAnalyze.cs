using FreeRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedsiAnalyze
{
    public static class InfoAnalyze
    {
        public static void RunInfoAnalyze(this RedisClient cli)
        {
            var result = cli.Info();
            result.WriteLineGreen();
        }
    }
}
