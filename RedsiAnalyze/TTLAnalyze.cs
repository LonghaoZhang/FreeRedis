using FreeRedis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedsiAnalyze
{
   public static class TTLAnalyze
    {
        public static void RunTTLAnalyze(this RedisClient cli)
        {
            "use key distribution analyze keys. console write yes=1,no=0 ,default 0".WriteLineBlue();
             var useAnalyzeKeys=defaultInt(0);
            List<string> matchs = new List<string>();
            if (useAnalyzeKeys == 0)
            {
                matchs.Clear();
                "match pattern：default Erp:*".WriteLineBlue();
                var match = defaultString();
                matchs.Add(match);
            }
            else
            {
                matchs.AddRange(StatisticsHelper.ShowStatisticsCountResult.Keys.ToList());
            }
            List<(string key, int analyzeCount, long maxTtl, string maxTtlKey, string analyzeTime)> resultData = new List<(string key, int analyzeCount, long maxTtl, string maxTtlKey, string analyzeTime)>();
            $"now datetime : {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}".WriteLineYellow();
            foreach (var match in matchs)
            {
                var pattern = match;
                if (!match.EndsWith("*"))
                {
                    pattern = match + "*";
                }                
                long maxTtl = 0;
                var maxTtlKey = string.Empty;
                var keyCount = 0;
                Stopwatch sw = Stopwatch.StartNew();
                foreach (var rt in cli.Scan(pattern, 1000, null))
                {
                    if (keyCount > 20 * 10000)
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
            }
            $"\r\n| patternKey | analyzeCount | analyzeTime | maxTtl | maxTtlKey |".WriteLineBlue();
            foreach (var item in resultData.OrderByDescending(p=>p.maxTtl).ToList())
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
