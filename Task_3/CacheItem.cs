using System;

namespace task_3
{
    public class CacheItem<TResult>
    {
        public TResult Result { get; private set; }
        public DateTime Expiration { get; private set; }

        public CacheItem(TResult result)
        {
            Result = result;
        }

        public bool IsExpired()
        {
            return Expiration <= DateTime.Now;
        }

        public void SetExpiration(TimeSpan duration)
        {
            Expiration = DateTime.Now.Add(duration);
        }
    }
}