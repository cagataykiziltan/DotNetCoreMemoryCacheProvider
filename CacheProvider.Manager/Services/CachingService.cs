using CacheProvider.Services.Services.Abstractions;
using CacheProvider.Manager.Models;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace CacheProvider.Services.Services
{
    public class CachingService : ICachingService
    {
        private readonly IMemoryCache _memCache;
        public CachingService(IMemoryCache memCache)
        {
            _memCache = memCache;
        }

        public T SetGetCache<T>( string cacheKey, DateTime cacheDuration, Func<T> acquireFunction)
        {
            T myEntity;        
            bool isCacheKeyExist =_memCache.TryGetValue(cacheKey, out string value);        
            
            if (!isCacheKeyExist)
            {
                myEntity = acquireFunction();
                string jsonEntity = Serializer.ToJson(myEntity);

                var cacheExpOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = cacheDuration,
                    Priority = CacheItemPriority.Normal
                };

                _memCache.Set(cacheKey, jsonEntity, cacheExpOptions);

            }
            else
            {
                myEntity = Serializer.FromJson<T>(value);
            }

            return myEntity;
        }

        public ResultModel<T> SetCache<T>(T valueObject, string cacheKey, DateTime cacheDuration)
        {
            bool isCacheKeyExist = _memCache.TryGetValue(cacheKey, out T value);

            if (!isCacheKeyExist)
            { 
                string jsonValue =Serializer.ToJson(valueObject);
                _memCache.Set(cacheKey, jsonValue, new MemoryCacheEntryOptions
                { 
                    AbsoluteExpiration = cacheDuration, 
                    Priority = CacheItemPriority.Normal 
                });
            }  

            return new ResultModel<T> { Success = true, Message = MessageConstants.SuccessMessage };
        }

        public ResultModel<T> GetFromCache<T>(string cacheKey)
        {
            bool isCacheKeyExist = _memCache.TryGetValue(cacheKey, out string value);

            if (isCacheKeyExist)
            {
                var entity = Serializer.FromJson<T>(value);
                return new ResultModel<T> { Success = true, CachedValue = entity }; 
            }

            return new ResultModel<T> { Success = true, Message = MessageConstants.NoCachedValue };
        }

        public string RemoveCache(string key)
        {
            var isCacheKeyExist = _memCache.TryGetValue(key, out string cacheValue);

            if (!isCacheKeyExist)
                return MessageConstants.NoCachedValue;

            _memCache.Remove(key);

            return MessageConstants.DeleteSuccess;

        }
    }
}
