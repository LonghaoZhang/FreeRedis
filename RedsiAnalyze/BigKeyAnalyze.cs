using FreeRedis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedsiAnalyze
{
    public static class BigKeyAnalyze
    {
        public static void RunBigKeyAnalyze(this RedisClient cli)
        {
            Console.WriteLine("match pattern：default Erp:*");
            var match = defaultString();
            $"now datetime : {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}".WriteLineYellow();
            long maxLength = 0;
            var maxLengthKey = string.Empty;
            var maxLengthValue = string.Empty;
            var keyCount = 0;
            long sumSpace = 0;
            Stopwatch sw = Stopwatch.StartNew();
            foreach (var rt in cli.Scan(match, 1000, null))
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
                        var v = pipe.Get(k);
                    }
                    var vts = pipe.EndPipe();
                    for (var i = 0; i < vts.Length; i++)
                    {
                        var v = vts[i]?.ToString()??"";
                        var length = Encoding.UTF8.GetBytes(v).Length;
                        if (length > maxLength)
                        {
                            maxLength = length;
                            maxLengthKey = rt[i];
                        }
                        length.Inc(rt[i]);
                        sumSpace += length;
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
            $"\r\nanalyze use times {sw.ElapsedMilliseconds.TimeGetMSHelper()}".WriteLineGreen();
            $"analyze keys count={keyCount},sum space usage ={sumSpace.ByteHelper()}. if count>20w ,only analyze 20w .".WriteLineGreen();
            
            $"\r\nall key count = {StatisticsHelper.Count_All}".WriteLineGreen();
            $"lt 1k count ={StatisticsHelper.Count_LT_KB}".WriteLineGreen();
            $"gt 1kb count ={StatisticsHelper.Count_KB_1}".WriteLineGreen();
            $"gt 10kb count ={StatisticsHelper.Count_KB_10}".WriteLineYellow();
            $"gt 100kb count ={StatisticsHelper.Count_KB_100}".WriteLineDarkYellow();
            $"gt 1mb count ={StatisticsHelper.Count_MB_1}".WriteLineRed();
            $"gt 10mb count ={StatisticsHelper.Count_MB_10}".WriteLineDarkRed();
            $"gt 50mb count ={StatisticsHelper.Count_MB_50}".WriteLineDarkRed();
                      
            $"\r\nmax big key length={maxLength.ByteHelper()}".WriteLineRed();
            $"max big key={maxLengthKey}".WriteLineRed();
            int cm = 0;
            while (cm == 0)
            {
                "show details,skip=0,no=n,yes: all=1,>50mb=2,>10mb=3,>1mb=4,>100kb=5,>10kb=6".WriteLineGreen();
                try
                {
                    var v = Console.ReadLine();
                    switch (v)
                    {
                        case "0":
                            cm = 1;
                            break;
                        case "n":                           
                            break;
                        case "1":
                            "---- >50mb random 100 keys ---- \r\n".WriteLineYellow();
                            string.Join(",", StatisticsHelper.Count_MB_50_Keys).WriteLineRed();
                            "---- >10mb random 100 keys  ---- \r\n".WriteLineYellow();
                            string.Join(",", StatisticsHelper.Count_MB_10_Keys).WriteLineRed();
                            "---- >1mb random 100 keys ---- \r\n".WriteLineYellow();
                            string.Join(",", StatisticsHelper.Count_MB_1_Keys).WriteLineRed();
                            "---- >100kb random 100 keys ---- \r\n".WriteLineYellow();
                            string.Join(",", StatisticsHelper.Count_KB_100_Keys).WriteLineRed();
                            "---- >10kb random 100 keys ---- \r\n".WriteLineYellow();
                            string.Join(",", StatisticsHelper.Count_KB_10_Keys).WriteLineRed();
                            break;
                        case "2":
                            "---- >50mb random 100 keys ---- \r\n".WriteLineYellow();
                            string.Join(",", StatisticsHelper.Count_MB_50_Keys).WriteLineRed();                            
                            break;
                        case "3":
                            "---- >10mb random 100 keys  ---- \r\n".WriteLineYellow();
                            string.Join(",", StatisticsHelper.Count_MB_10_Keys).WriteLineRed();                           
                            break;
                        case "4":
                            "---- >1mb random 100 keys ---- \r\n".WriteLineYellow();
                            string.Join(",", StatisticsHelper.Count_MB_1_Keys).WriteLineRed();                           
                            break;
                        case "5":
                            "---- >100kb random 100 keys ---- \r\n".WriteLineYellow();
                            string.Join(",", StatisticsHelper.Count_KB_100_Keys).WriteLineRed();                            
                            break;
                        case "6":
                            "---- >10kb random 100 keys ---- \r\n".WriteLineYellow();
                            string.Join(",", StatisticsHelper.Count_KB_10_Keys).WriteLineRed();                            
                            break;
                    }
                }
                catch
                { }
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
            return Convert.ToInt32(defaultString(defaultInt.ToString()));
        }
    }
}
