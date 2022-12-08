using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RedsiAnalyze
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                "clear last analyze data? yes=1,no=0,default=1".WriteLineBlue();
                StatisticsHelper.Init(defaultInt(1));
                ColorConsole.WriteLineBlue("DB Select,default 1;");
                ColorConsole.WriteLine("Descript： test=1,master=2,large-master=3");
                var cli = RedisBase.cli(defaultInt(1));
                ColorConsole.WriteLineBlue("Function Select,default 1;");
                ColorConsole.WriteLine("Descript： info=1,clientlist=2,config=3,MemoryStats=4,keydistribution=5,ttl=6,bigkey=7,slowlog=8,dealtestdata=9");
                cli.Start(Console.ReadLine());
                "\r\n---- .. analyze over .. ----".WriteLineBlue();
                Console.ReadLine();
            }
        }


        static string defaultString(string defaultStr = "*")
        {
            var match = Console.ReadLine();
            return string.IsNullOrEmpty(match) ? defaultStr : match;
        }
        static int defaultInt(int defaultInt = 10000)
        {
            return Convert.ToInt32(defaultString(defaultInt.ToString()));
        }
    }
    public class Tmodel
    {
        public string name { get; set; }
    }
}
