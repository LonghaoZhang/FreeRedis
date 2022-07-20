using FreeRedis;
using Jst.Standard.Cache.Settings;
using System;

namespace Jst.Standard.Cache
{
    public class CacheService<T>: CacheServiceBase
    {      

        public static CacheSettings CacheSettings => _cacheSettings == null ? new CacheSettings() : _cacheSettings;
        public static ICacheClient<T> cacheClient;
        static object _mutilTypeNameLock = new object();

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
            if (settings != null && settings.FilterKeys != null && settings.FilterKeys.Length > 0)
            {
                for (var i = 0; i < settings.FilterKeys.Length; i++)
                {
                    FilterKeys.Add(settings.FilterKeys[i]);
                }
            }
        }
        public static ICacheClient<T> CreateClient(CacheStoreType storeType, string CacheName)
        {
            lock (_mutilTypeNameLock)
            {
                var ObjectCachePrefixKey = CacheHelper.GeneratePrefix<T>(CacheName);
                var existFilterKeys = CacheServiceBase.FilterKeys?.Contains(ObjectCachePrefixKey)??false;                
                ICacheClient<T> cacheClient;
                switch (storeType)
                {
                    case CacheStoreType.OnlyLocal:
                        cacheClient = new LocalCacheClient<T>(ObjectCachePrefixKey);
                        return cacheClient;
                    default:
                        cacheClient = new CacheClient<T>(ObjectCachePrefixKey);
                        if (storeType == CacheStoreType.LoaclAndRedis)
                        {
                            if (!existFilterKeys)
                            {
                                CacheServiceBase.AddFilterKeys(ObjectCachePrefixKey);
                                BaseRedis.Client.UseClientSideCaching(new ClientSideCachingOptions
                                {
                                    Capacity = CacheService<T>.CacheSettings.Capacity,
                                    KeyFilter = key => CacheHelper.KeyFilterStartsWith(CacheServiceBase.FilterKeys, key),
                                    CheckExpired = (key, dt) => DateTime.Now.Subtract(dt) > CacheService<T>.CacheSettings.ClientKeyTimeOut
                                });
                            }
                        }
                        return cacheClient;
                }
            }
        }

        public static void LoadClientAndSide()
        {
            Client.UseClientSideCaching(new ClientSideCachingOptions
            {
                Capacity = CacheSettings.Capacity,
                KeyFilter = key => CacheHelper.KeyFilterStartsWith(FilterKeys, key),
                CheckExpired = (key, dt) => DateTime.Now.Subtract(dt) > CacheSettings.ClientKeyTimeOut
            });
        }
    }

}
