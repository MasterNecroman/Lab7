using System;
using System.Collections.Generic;

namespace task_3
{
    public class FunctionCache<TKey, TResult>
    {
        private Dictionary<TKey, CacheItem<TResult>> cache = new Dictionary<TKey, CacheItem<TResult>>();
        private Func<TKey, TResult> function;

        public FunctionCache(Func<TKey, TResult> func)
        {
            function = func;
        }

        public TResult GetResult(TKey key)
        {
            if (cache.TryGetValue(key, out CacheItem<TResult> cachedItem) && !cachedItem.IsExpired())
            {
                Console.WriteLine($"Result for key {key} is retrieved from cache.");
                return cachedItem.Result;
            }
            else
            {
                TResult result = function(key);
                Console.WriteLine($"Result for key {key} is calculated and cached.");
                cache[key] = new CacheItem<TResult>(result);
                return result;
            }
        }

        public void SetCacheDuration(TKey key, TimeSpan duration)
        {
            if (cache.ContainsKey(key))
            {
                cache[key].SetExpiration(duration);
            }
        }
    }
}