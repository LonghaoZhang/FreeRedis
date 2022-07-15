using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jst.Standard.Cache
{
    public interface ICacheClient<T>
    {
        string ObjectCachePrefixKey{get;set;}
        #region string
        T Get(string key);        
        T Get(string key, Func<string, T> data, TimeSpan timeout = default);
        long Del(params string[] keys);
        #endregion
        #region hash
        /// <summary>
        /// only_local type no support
        /// </summary>
        Dictionary<string, T> HGET(string key, Func<string, Dictionary<string, T>> data, TimeSpan timeout);
        /// <summary>
        /// only_local type no support
        /// </summary>
        long HDel(string key, params string[] fields);
        /// <summary>
        /// only_local type no support
        /// </summary>
        T HGet(string key, string filed, Func<string, T> data);
        #endregion
    }
}
