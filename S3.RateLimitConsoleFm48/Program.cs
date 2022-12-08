using Polly;
using Polly.RateLimit;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

namespace S3.RateLimitConsoleFm48
{
    internal class Program
    {        
        static void Main(string[] args)
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 200; i++)
            {
                var t = Task.Run(() =>
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(100));
                    var result = act();
                    var tctResult = tct();
                    Console.WriteLine(result + $"--{DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss:fff")}--{tctResult}");
                });
                tasks.Add(t);
            }
            Task.WaitAll(tasks.ToArray());

            for (int i = 0; i < 200; i++)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                var result = act();
                Console.WriteLine(result + $"--{DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss:fff")}");
            }

        }
        
        static int act()
        {            
            return Policics.ActRateLimit.Execute<int>(()=> cache(1), () => db(2)) ;
        }
        static int tct()
        {
            return Policics.TtRateLimit.Execute<int>(() => cache(3), () => db(4));
        }

        static int cache(int b)
        {
            return b;
        }
        static int db(int a)
        {
            return a;
        }
    }
    public class Policics
    {
       public static RateLimit ActRateLimit = RateLimit.Instance("act", 5);
       public static RateLimit TtRateLimit = RateLimit.Instance("tct", 7);
    }

    public class RateLimit : Counter
    {
        public static RateLimit Instance(string name,int qps) => new Lazy<RateLimit>(() => {
            return new RateLimit(name, qps);
        }).Value;
        private int QPS { get; set; }
        private string Name { get; set; }
        private RateLimit(string name, int qps) : base()
        {
            QPS = qps;
            Name = name;
        }       
        public TResult Execute<TResult>(Func<TResult> noLimitFunc, Func<TResult> limitedFunc)
        {
            if (DateTime.Compare(DateTime.Now.AddSeconds(-1), LastIncTime) > 0)
            {
                lock (this)
                {
                    if (DateTime.Compare(DateTime.Now.AddSeconds(-1), LastIncTime) > 0)
                    {
                        Clear();
                    }
                }
            }
            Inc();
            if (QPS < Count)
            {
                if ((Count - QPS) % 3 == 1)
                {
                    var traceStr = GetStackTrace();
                    Console.WriteLine($"QPS={QPS},Count={Count},trcak=>{traceStr}");
                }
                return limitedFunc.Invoke();
            }
            else
            {
                return noLimitFunc.Invoke();
            }
        }
        public static string GetStackTrace(int startIndex = 0)
        {
            try
            {
                var st = new StackTrace(true);
                var result = new StringBuilder();

                for (int i = startIndex; i < st.FrameCount; i++)
                {
                    if (i > (startIndex + 10))
                    {
                        break;
                    }
                    var sf = st.GetFrame(i);
                    var str = string.Format("FileName: {0} Method: {1} Line: {2} Column: {3}  ",
                       sf.GetFileName(), sf.GetMethod(), sf.GetFileLineNumber(), sf.GetFileColumnNumber());
                    result.AppendLine(str);
                }
                return result.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
    public class Counter
    {
        protected int Count;
        protected DateTime LastIncTime { get; set; }
        protected Counter()
        {
            LastIncTime = DateTime.Now;
            Count = 0;
        }
        protected void Inc()
        {
            Interlocked.Increment(ref Count);
        }
        protected void Clear()
        {
            LastIncTime = DateTime.Now;
            Count = 0;
        }
    }

    public class P
    { 
       public string Name { get; set; }
    }

}
