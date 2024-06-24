using FreeRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedsiAnalyze
{
    public static class ClientListAnalyze
    {
        public static void RunClientListAnalyze(this RedisClient cli)
        {
            var result = cli.ClientList();
            var conStr = result.TrimEnd('\n').Split('\n');
            var redCount = 0;
            foreach (var con in conStr)
            {
                var itemStr = con.Split(' ');
                var isRed = false;
                foreach (var str in itemStr)
                {
                    var item = str.GetEquelVal();
                    switch (item.k)
                    {
                        case "idle":
                            isRed = Convert.ToInt64(item.v) > 1800;
                            if (isRed)
                            {
                                redCount++;
                            }
                            break;
                    }
                }
                if (isRed)
                {
                    con.WriteLineRed();
                }
                else
                {
                    con.WriteLineGreen();
                }
            }
            if (redCount > 0)
            {
                $"all client count={conStr.Length};".Write();
                $"green client count={conStr.Length - redCount};".WriteGreen();
                $"red client count={redCount};".WriteLineRed();
            }
        }
    }
}
