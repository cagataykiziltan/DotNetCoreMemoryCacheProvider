using CacheProvider.Manager.Models;
using System;


namespace CacheProvider.Services.Services.Abstractions
{
    public interface ICachingService
    {
        T SetGetCache<T>(string cacheKey, DateTime cacheDuration, Func<T> acquireFunction);

        ResultModel<T> SetValueCache<T>(T valueObject, string cacheKey, DateTime cacheDuration);

        ResultModel<T> GetFromCache<T>(string cacheKey);

         string RemoveCache(string key);
    }
}
