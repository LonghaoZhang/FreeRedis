using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jst.Standard.Cache
{
    public class CacheServiceBase:BaseRedis
    {
        public static HashSet<string> FilterKeys { get; set; }
        public static void AddFilterKeys(string key)
        {
            if (FilterKeys == null)
            {
                FilterKeys = new HashSet<string>();
            }
            FilterKeys.Add(key);
        }
    }
}
