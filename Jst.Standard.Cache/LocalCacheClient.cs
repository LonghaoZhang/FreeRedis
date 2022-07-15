using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jst.Standard.Cache
{
    public partial class LocalCacheClient<T>
    {
        private IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        public string ObjectCachePrefixKey { get; set; }
    }
}
