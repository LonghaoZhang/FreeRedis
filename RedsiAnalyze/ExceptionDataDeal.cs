using FreeRedis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedsiAnalyze
{
    public static class ExceptionDataDeal
    {
        public static void RunExceptionDataDeal(this RedisClient cli)
        {
            Console.WriteLine("match pattern：default Erp:*");
            var match = defaultString();
            $"now datetime : {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}".WriteLineYellow();
            var keyCount = 0;
            var dealCount = 0;
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
                        if (k.StartsWith("Erp:PassportModel") || k.StartsWith("Erp:PT"))
                        {
                            continue;
                        }
                        else
                        {
                            pipe.Del(k);
                        }
                    }
                    var vts = pipe.EndPipe();
                    dealCount += (vts?.Length??0);
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
            $"del data count ={dealCount}".WriteLineDarkRed();

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
