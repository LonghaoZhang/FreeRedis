using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jst.Standard.Cache
{
    public enum CacheStoreType { OnlyLocal, LoaclAndRedis, OnlyReids }
    public enum BusArchCode { Order, Wms }
    public class BusArch
    {
        string key;
        public BusArch(String _key)
        {
            this.key = _key;
        }
        public BusArchCode Order { get; } = BusArchCode.Order;
        public BusArchCode ERP { get; } = BusArchCode.Wms;
    }
    public class CacheHelper
    {
        /// <summary>
        /// 生成规则：Erp:Auto:(CacheName==null?TypeName:CacheName): BusKey
        /// </summary>
        /// <param name="busKey"></param>
        /// <returns></returns>  
        public static string GeneratePrefix<T>(CacheStoreType storeType, string CacheName = null)
        {
            if (storeType == CacheStoreType.LoaclAndRedis)
            {
                return string.IsNullOrEmpty(CacheName) ? $"ERP:AUTO:CAS:{typeof(T).Name}" : $"ERP:AUTO:CAS:{CacheName}";
            }
            return string.IsNullOrEmpty(CacheName) ? $"ERP:AUTO:{typeof(T).Name}" : $"ERP:AUTO:{CacheName}";
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
    public static class KeyHelper
    {
        public static string CacheOrderKey(this string key)
        {
            return "";
        }
        public static string CacheWmsKey(this string key)
        {
            return "";
        }
        public static string CacheMidKey(this string key)
        {
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="busCode"></param>
        /// <param name="tableName"></param>
        public static string GenerateKey(this string key, string busCode, string tableName)
        {
            return "";  
        }
    }
}
