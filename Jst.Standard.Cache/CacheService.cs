using FreeRedis;
using Jst.Standard.Cache.Settings;
using System;

namespace Jst.Standard.Cache
{
    public class CacheService<T>: CacheServiceBase
    {      

        internal static CacheSettings CacheSettings => _cacheSettings == null ? new CacheSettings() : _cacheSettings;

        static CacheSettings _cacheSettings { get; set; }
        /// <summary>
        /// process：
        /// 1.filterKey append this FilterKeys
        /// 2.use timeout ，no set use default
        /// </summary>
        /// <param name="settings"></param>
        public void InitCacheSettings(CacheSettings settings)
        {
            _cacheSettings = settings;
        }
        public static ICacheClient<T> CreateClient(CacheStoreType storeType, string CacheName)
        {
            var ObjectCachePrefixKey = CacheHelper.GeneratePrefix<T>(CacheName);
            ICacheClient<T> cacheClient;
            switch (storeType)
            {                       
                case CacheStoreType.OnlyLocal:
                    cacheClient = new LocalCacheClient<T>();
                    cacheClient.ObjectCachePrefixKey = ObjectCachePrefixKey;
                    return cacheClient;
                default:
                    cacheClient = new CacheClient<T>();
                    cacheClient.ObjectCachePrefixKey = ObjectCachePrefixKey;
                    if (storeType == CacheStoreType.LoaclAndRedis)
                    {
                        AddFilterKeys(cacheClient.ObjectCachePrefixKey);
                        LoadClientAndSide();
                    }
                    return cacheClient;
            }
        }

        static void LoadClientAndSide()
        {
            var cacheSettings = new CacheSettings();
            CacheClient<T>.redisClient.UseClientSideCaching(new ClientSideCachingOptions
            {
                Capacity = cacheSettings.Capacity,
                KeyFilter = key => CacheHelper.KeyFilterStartsWith(FilterKeys, key),
                CheckExpired = (key, dt) => DateTime.Now.Subtract(dt) > cacheSettings.ClientKeyTimeOut
            });
        }
        static void LoadClientAndSide(CacheSettings cacheSettings)
        {
            CacheClient<T>.redisClient.UseClientSideCaching(new ClientSideCachingOptions
            {
                Capacity = cacheSettings.Capacity,
                KeyFilter = key => CacheHelper.KeyFilterStartsWith(FilterKeys, key),
                CheckExpired = (key, dt) => DateTime.Now.Subtract(dt) > cacheSettings.ClientKeyTimeOut
            });
        }
    }

}
