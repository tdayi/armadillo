using Armadillo.Core.Cache;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Armadillo.Application.Concrete.Cache
{
    public class InMemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache memoryCache;

        public InMemoryCacheManager(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public bool CacheContains(string key)
        {
            object cacheValue;

            memoryCache.TryGetValue(key, out cacheValue);

            if (cacheValue != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public T GetByKey<T>(string key)
        {
            var result = default(T);

            if (CacheContains(key))
            {
                result = memoryCache.Get<T>(key);
            }

            return result;
        }

        public void Set<T>(string key, T data)
        {
            memoryCache.Set(key, data, new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.Normal
            });
        }

        public void Remove(string key)
        {
            if (CacheContains(key))
            {
                memoryCache.Remove(key);
            }
        }
    }
}
