using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jst.Standard.Cache
{
    public enum CacheStoreType { OnlyLocal, LoaclAndRedis, OnlyReids }
    public class CacheHelper
    {
        /// <summary>
        /// 生成规则：Erp:Auto:(CacheName==null?TypeName:CacheName): BusKey
        /// </summary>
        /// <param name="busKey"></param>
        /// <returns></returns>
        public static string GeneratePrefixKey<T>(string busKey, string CacheName = null)
        {
            return string.IsNullOrEmpty(CacheName) ? $"Erp:Auto:{typeof(T).Name}:{busKey}" : $"Erp:Auto:{CacheName}:{busKey}";
        }
        public static string GeneratePrefix<T>(string CacheName = null)
        {
            return string.IsNullOrEmpty(CacheName) ? $"Erp:Auto:{typeof(T).Name}" : $"Erp:Auto:{CacheName}";
        }
        public static bool KeyFilterStartsWith(HashSet<string> filterKeys, string key)
        {
            foreach (var vk in filterKeys)
            {
                if (key.StartsWith(vk))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
