using FreeRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedsiAnalyze
{
    public static class SlowLogAnalyze
    {
        public static void RunSlowLogAnalyze(this RedisClient cli)
        {
            dynamic result = cli.SlowLog("get");
            $"slow log all count={result.Length} ".WriteLineYellow();
            for (var index = 0; index < result.Length; index++)
            {
                string id = string.Empty;
                long timespan = 0;
                long times = 0;
                string cmd = string.Empty;
                string client = string.Empty;
                string clientName = string.Empty;
                var val = result[index];
                for (var i = 0; i < val.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            id = Convert.ToString(val[i]);
                            break;
                        case 1:
                            timespan = Convert.ToInt64(val[i]);
                            break;
                        case 2:
                            times = Convert.ToInt64(val[i]);
                            break;
                        case 3:
                            cmd = string.Join(" ", val[i]);
                            break;
                        case 4:
                            client = Convert.ToString(val[i]);
                            break;
                        case 5:
                            clientName = Convert.ToString(val[i]);
                            break;
                    }
                }
                index.ToString().WriteRed("  ");
                timespan.TimeStrHelper().WriteRed("  ");
                times.TimeGetUSHelper().WriteRed("  ");
                cmd.WriteRed("  ");
                client.WriteRed("  ");
                clientName.WriteLineRed();
            }
        }
    }
}
