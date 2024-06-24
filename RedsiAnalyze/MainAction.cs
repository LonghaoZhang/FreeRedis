using FreeRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedsiAnalyze
{
    public static class MainAction
    {
        public static void Start(this RedisClient cli, string type)
        {
            "\r\n---- ..start analyze.. ----\r\n".WriteLineBlue();
            switch (type)
            {
                case "1":
                    cli.RunInfoAnalyze();
                    break;
                case "2":
                    cli.RunClientListAnalyze();
                    break;
                case "3":
                    cli.RunConfigAnalyze();
                    break;
                case "4":
                    cli.RunMemoryStatsAnalyze();
                    break;
                case "5":
                    cli.RunKeyDistributionAnalyze();
                    break;
                case "6":
                    cli.RunTTLAnalyze();
                    break;
                case "7":
                    cli.RunBigKeyAnalyze();
                    break;
                case "8":
                    cli.RunSlowLogAnalyze();
                    break;
                case "9":
                    cli.RunExceptionDataDeal();
                    break;
                case "0":
                    cli.RunTTLKeyCountAnalyze();
                    break;
            }
        }
    }
}
