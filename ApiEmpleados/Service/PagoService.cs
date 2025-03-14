using ApiEmpleados.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ApiEmpleados.Service
{
    public class PagoService : IPagoService
    {
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _valkey;
        private readonly TimeSpan _cacheExpiry = TimeSpan.FromMinutes(5);
        Repository<Pago> _repository;
        public PagoService(IDb<Pago> db, IDistributedCache cache, IConnectionMultiplexer valkey)
        {
            _cache = cache;
            _repository = db.GetRepo();
            _valkey = valkey;
        }
        public async Task<List<Pago>> GetPagosByEmployeeId(Guid employeeId)
        {
            string cacheKey = $"employeeId_{employeeId}";
            // Step 1: Try to get the item from Redis cache
            //var cachedItem = await _cache.GetStringAsync(cacheKey);
            var cachedItem = await GetValueFromCache(cacheKey);
            if (cachedItem != null)
            {
                Console.WriteLine("✅ Item retrieved from cache!");
                return JsonConvert.DeserializeObject<List<Pago>>(cachedItem) ?? new List<Pago>();
            }
            // Step 2: Get info from repository:
            var list = await _repository.Get(employeeId);
            // Step 3: Cache the item in Redis
            //await _cache.SetStringAsync(
            //    cacheKey,
            //    JsonConvert.SerializeObject(list),
            //    new DistributedCacheEntryOptions {
            //        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            //        SlidingExpiration = TimeSpan.FromMinutes(2) // Resets on access
            //    }
            //);
            SetValueToCache(cacheKey, JsonConvert.SerializeObject(list));
            Console.WriteLine("🔄 Item added to cache.");
            return list;
        }
        private async Task<string> GetValueFromCache(string key)
        {
            var db = _valkey.GetDatabase();
            var value = await db.StringGetAsync(key);
            return value;
        }
        private async void SetValueToCache(string key, string valueToSave)
        {
            var db = _valkey.GetDatabase();
            var value = await db.StringSetAsync(key, valueToSave);
        }
    }
}
