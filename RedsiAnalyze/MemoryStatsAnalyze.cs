using FreeRedis;
using System;

namespace RedsiAnalyze
{
    public static class MemoryStatsAnalyze
    {
        public static void RunMemoryStatsAnalyze(this RedisClient cli)
        {
            var result = cli.MemoryStats();
            foreach (var kv in result)
            {
                switch (kv.Key)
                {
                    case "peak.allocated":
                        kv.Key.Write(" = ");
                        kv.Value.ToString().ByteHelper().Write();
                        "  #redis从启动来，allocator分配的内存峰值".WriteLineGreen();
                        break;
                    case "total.allocated":
                        kv.Key.Write(" = ");
                        kv.Value.ToString().ByteHelper().Write();
                        "  #allocator分配当前内存字节数".WriteLineGreen();
                        break;
                    case "startup.allocated":
                        kv.Key.Write(" = ");
                        kv.Value.ToString().ByteHelper().Write();
                        "  #redis启动完成使用的内存字节数".WriteLineGreen();
                        break;
                    case "clients.normal":
                        kv.Key.Write(" = ");
                        kv.Value.ToString().ByteHelper().Write();
                        "  #所有一般客户端消耗内存节字数,即所有flag为N的客户端内存使用".WriteLineGreen();
                        break;
                    case "aof.buffer":
                        kv.Key.Write(" = ");
                        kv.Value.ToString().ByteHelper().Write();
                        "  #aof buffer使用内存字节数，一般较小，在aof rewrite时会变得较大".WriteLineGreen();
                        break;
                    case "lua.caches":
                        kv.Key.Write(" = ");
                        kv.Value.ToString().ByteHelper().Write();
                        "  #所有lua脚本占用的内存节字数".WriteLineGreen();
                        break;
                    case "overhead.total":
                        kv.Key.Write(" = ");
                        kv.Value.ToString().ByteHelper().Write();
                        "  #redis总的分配的内存的字节数".WriteLineGreen();
                        break;
                    case "keys.count":
                        kv.Key.Write(" = ");
                        kv.Value.ToString().Write();
                        "  #整个实例key的个数，相同于dbsize返回值".WriteLineGreen();
                        break;
                    case "keys.bytes-per-key":
                        kv.Key.Write(" = ");
                        kv.Value.ToString().ByteHelper().Write();
                        "  #每个key平均占用字节数；把overhead也均摊到每个key上".WriteLineGreen();
                        break;
                    case "dataset.bytes":
                        kv.Key.Write(" = ");
                        kv.Value.ToString().ByteHelper().Write();
                        "  #表示redis数据集占用的内存容量，即分配的内存总量减去 overhead.total".WriteLineGreen();
                        break;
                    case "dataset.percentage":
                        kv.Key.Write(" = ");
                        kv.Value.ToString().ByteHelper().Write();
                        "  #表示redis数据占用内存占总内存分配的百分比".WriteLineGreen();
                        break;
                    case "peak.percentage":
                        kv.Key.Write(" = ");
                        kv.Value.ToString().Write();
                        "  #当前内存使用量在峰值时的占比".WriteLineGreen();
                        break;
                    case "allocator.allocated":
                        kv.Key.Write(" = ");
                        kv.Value.ToString().ByteHelper().Write();
                        "  #该参数不同与 total.allocated, 它计算所有分配的内存大小（不仅仅是使用zmalloc分配的）".WriteLineGreen();
                        break;
                    case "allocator.active":
                        kv.Key.Write(" = ");
                        kv.Value.ToString().ByteHelper().Write();
                        "  #与常驻内存allocator.resident不同，这不包括jemalloc申请的还未使用的内存".WriteLineGreen();
                        break;
                    case "allocator.resident":
                        kv.Key.Write(" = ");
                        kv.Value.ToString().Write();
                        "  #与RSS不同，这不包括来自共享库和其他非堆映射的RSS".WriteLineGreen();
                        break;
                }
            }

        }
    }
}
