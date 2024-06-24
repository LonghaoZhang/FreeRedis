using FreeRedis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedsiAnalyze
{
    public static class KeyDistributionAnalyze
    {
        public static void RunKeyDistributionAnalyze(this RedisClient cli)
        {
            Console.WriteLine("match pattern：default *");
            var match = defaultString();
            Console.WriteLine("deep：default 5");
            var deep = defaultInt(5);           
            Console.WriteLine("show group count gt val：default 100");
            var fiterCount = defaultInt();
            $"now datetime : {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}".WriteLineYellow();
            Stopwatch sw = Stopwatch.StartNew();
            foreach (var rt in cli.Scan(match, 1000, null))
            {
                foreach (var m in rt)
                {
                    var statisticsKey = ((string)m).GetPrefix(deep);
                    statisticsKey.Inc();
                }
                if (StatisticsHelper.Count % (100 * 10000) == 0)
                {
                    Console.WriteLine("count=" + StatisticsHelper.Count);
                    Console.ReadLine();
                }
                ".".WriteGreen();
            }
            sw.Stop();
            $"\r\nanalyze use times {sw.ElapsedMilliseconds.TimeGetMSHelper()}".WriteLineGreen();

            $"\r\nkey count={StatisticsHelper.Count},group count={StatisticsHelper.StatisticsCountResult.Keys.Count}".WriteLineGreen();
            var sortResult = StatisticsHelper.StatisticsCountResult.OrderByDescending(p => p.Value);
            StatisticsHelper.ShowStatisticsCountResult.Clear();
            foreach (var kv in sortResult)
            {
                if (kv.Value > fiterCount)
                {
                    StatisticsHelper.ShowStatisticsCountResult.Add(kv.Key,kv.Value);
                    var consoleStr = (kv.Key + "----" + kv.Value);
                    if (kv.Value > 3000000)
                    {
                        consoleStr.WriteLineDarkRed();
                        continue;
                    }
                    if (kv.Value > 1000000)
                    {
                        consoleStr.WriteLineRed();
                        continue;
                    }
                    if (kv.Value > 100000)
                    {
                        consoleStr.WriteLineDarkYellow();
                        continue;
                    }
                    if (kv.Value > 10000)
                    {
                        consoleStr.WriteLineYellow();
                        continue;
                    }
                    consoleStr.WriteLine(); 
                }
            }
        }

        static string defaultString(string defaultStr = "*")
        {
            var match = Console.ReadLine();
            if (!match.EndsWith("*"))
            {
                match += "*";
            }
            return string.IsNullOrEmpty(match) ? defaultStr : match;
        }
        static string defaultStringNoHavexx(string defaultStr = "*")
        {
            var match = Console.ReadLine();
            return string.IsNullOrEmpty(match) ? defaultStr : match;
        }
        static int defaultInt(int defaultInt = 100)
        {
            return Convert.ToInt32(defaultStringNoHavexx(defaultInt.ToString()));
        }
    }
}
