using System;
using System.Collections.Generic;

namespace Jst.Standard.Cache.Settings
{
    public class CacheSettings
    {
        public int Capacity { get; set; } = 10 * 10000 * 10000;
        public string[] FilterKeys { get; set; }   
        public TimeSpan ClientKeyTimeOut { get; set; } = TimeSpan.FromDays(2);
        public TimeSpan RedisKeyTimeOut { get; set; } = TimeSpan.FromDays(30);
    }
}
