using FreeRedis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedsiAnalyze
{
    public static class TTLKeyCountAnalyze
    {
        static int Hour = 60 * 60;
        static int Day = 60 * 60 * 24;
        static int Week = 60 * 60 * 24 * 7;
        static int Mounth = 60 * 60 * 24 * 30;
        static int Quarter = 60 * 60 * 24 * 90;
        public static void RunTTLKeyCountAnalyze(this RedisClient cli)
        {
            "match pattern：default Erp:*".WriteLineBlue();
            var match = defaultString();
            List<(string key, int analyzeCount, long maxTtl, string maxTtlKey, string analyzeTime)> resultData = new List<(string key, int analyzeCount, long maxTtl, string maxTtlKey, string analyzeTime)>();
            $"now datetime : {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}".WriteLineYellow();
            var pattern = match;
            if (!match.EndsWith("*"))
            {
                pattern = match + "*";
            }
            int HourCount = 0;
            int DayCount = 0;
            int WeekCount = 0;
            int MounthCount = 0;
            int QuarterCount = 0;
            string HourKeys = string.Empty;
            string DayCountKeys = string.Empty;
            string WeekCountKeys = string.Empty;
            string MounthCountKeys = string.Empty;
            string QuarterCountKeys = string.Empty;
            int keyCount = 0;
            long maxTtl = 0;
            var maxTtlKey = string.Empty;
            Stopwatch sw = Stopwatch.StartNew();
            foreach (var rt in cli.Scan(pattern, 1000, null))
            {
                if (keyCount > 300 * 10000)
                {
                    break;
                }
                keyCount += rt.Length;
                Stopwatch stopwatch = Stopwatch.StartNew();
                using (var pipe = cli.StartPipe())
                {
                    foreach (var k in rt)
                    {
                        var v = pipe.Ttl(k);
                    }
                    var vts = pipe.EndPipe();
                    for (var i = 0; i < vts.Length; i++)
                    {
                        var v = (long)vts[i];
                        if (v > maxTtl)
                        {
                            maxTtl = v;
                            maxTtlKey = rt[i];
                        }
                        if (v > Quarter)
                        {
                            QuarterCount++;
                            if (QuarterCountKeys.Length < 200)
                            {
                                QuarterCountKeys += rt[i];
                            }
                        }
                        if (v > Mounth && v <= Quarter)
                        {
                            MounthCount++;
                            if (MounthCountKeys.Length < 200)
                            {
                                MounthCountKeys += rt[i];
                            }
                        }
                        if (v > Week && v <= Mounth)
                        {
                            WeekCount++;
                            if (WeekCountKeys.Length < 200)
                            {
                                WeekCountKeys += rt[i];
                            }
                        }
                        if (v > Day && v <= Week)
                        {
                            DayCount++;
                            if (DayCountKeys.Length < 200)
                            {
                                DayCountKeys += rt[i];
                            }
                        }
                        if (v > Hour && v <= Day)
                        {
                            HourCount++;
                            if (HourKeys.Length < 200)
                            {
                                HourKeys += rt[i];
                            }
                        }
                    }
                }
                stopwatch.Stop();
                if (stopwatch.ElapsedMilliseconds > 500)
                {
                    ".".WriteRed();
                }
                else
                {
                    ".".WriteGreen();
                }
            }
            sw.Stop();
            resultData.Add((match, keyCount, maxTtl, maxTtlKey, sw.ElapsedMilliseconds.TimeGetMSHelper()));
            "|".WriteGreen();

            $"\r\n| patternKey | analyzeCount | analyzeTime | maxTtl | maxTtlKey |".WriteLineBlue();
            foreach (var item in resultData.OrderByDescending(p => p.maxTtl).ToList())
            {
                string show = $"| {item.key} | {item.analyzeCount} | {item.analyzeTime} | ttl={item.maxTtl.TimeGetSecondHelper()} | key={item.maxTtlKey} |";
                if (item.maxTtl > 60 * 60 * 24 * 20)
                {
                    show.WriteLineDarkRed();
                    continue;
                }
                if (item.maxTtl > 60 * 60 * 24 * 7)
                {
                    show.WriteLineRed();
                    continue;
                }
                if (item.maxTtl > 60 * 60 * 24 * 1)
                {
                    show.WriteLineDarkYellow();
                    continue;
                }
                if (item.maxTtl > 60 * 60 * 6)
                {
                    show.WriteLineYellow();
                    continue;
                }
                show.WriteLineGreen();
            }
            $"\r\n >90d  count = {QuarterCount}".WriteLineDarkRed();
            $"\r\n >90d  keys = {QuarterCountKeys}".WriteLineDarkRed();
            $"\r\n >30d&<90d  count = {MounthCount}".WriteLineRed();
            $"\r\n >30d&<90d  keys = {MounthCountKeys}".WriteLineRed();
            $"\r\n >7d>&<30d  count = {WeekCount}".WriteLineDarkYellow();
            $"\r\n >7d>&<30d  keys = {WeekCountKeys}".WriteLineDarkYellow();
            $"\r\n >1d&<7d  count = {DayCount}".WriteLineYellow();
            $"\r\n >1d&<7d  keys = {DayCountKeys}".WriteLineYellow();
            $"\r\n >1h&<24h  count = {HourCount}".WriteGreen();
            $"\r\n >1h&<24h  keys = {HourKeys}".WriteGreen();

        }
        static string defaultString(string defaultStr = "Erp:*")
        {
            var match = Console.ReadLine();
            if (!match.EndsWith("*"))
            {
                match += "*";
            }
            return string.IsNullOrEmpty(match) ? defaultStr : match;
        }
        static int defaultInt(int defaultInt = 10000)
        {
            var v = defaultString(defaultInt.ToString()).TrimEnd('*');
            if (v.IndexOf("Erp") >= 0)
            {
                v = "0";
            }
            return Convert.ToInt32(v);
        }
    }
}
