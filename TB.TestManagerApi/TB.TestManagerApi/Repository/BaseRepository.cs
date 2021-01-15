using System;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TB.TestManagerApi.Common;

namespace TB.TestManagerApi.Repository
{
    public abstract class BaseRepository
    {
        private MemoryCacheEntryOptions _cacheExpirationOptions;
        protected MemoryCacheEntryOptions GetCacheOptions
        {
            get
            {
                return _cacheExpirationOptions;
            }
        }
     
        protected void SetCacheOptions(IConfiguration configuration)
        {
            int absoluteTime = 0;
            try
            {
                absoluteTime = configuration.GetValue<int>(ConfigurationKeys.CacheExpirationMinutes);
            }
            catch (Exception)
            {
            }
            if (absoluteTime == 0) absoluteTime = 10;
            _cacheExpirationOptions = new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTime.Now.AddMinutes(absoluteTime) };
        }
    }
}




