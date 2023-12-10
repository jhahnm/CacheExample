using API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace API.Data.repositories;

public class CachedCarsRepository: ICarsRepository
{
    private readonly ICarsRepository _decorated;
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _options;

    public CachedCarsRepository(ICarsRepository decorated, IDistributedCache cache)
    {
        _cache = cache;
        _decorated = decorated;
        _options = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5)) // Expire time
            .SetAbsoluteExpiration(DateTime.Now.AddHours(6)); // Absolute expire time
    }
    public async Task<List<Car>> GetCarsByMake(string make)
    {
        var cacheKey = $"GetCarsByMake-{make}";
        var carsSerialized = await _cache.GetStringAsync(cacheKey);

        if(!string.IsNullOrEmpty(carsSerialized))
        {
            return JsonConvert.DeserializeObject<List<Car>>(carsSerialized);
        }

        var cars = await _decorated.GetCarsByMake(make);
        await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(cars), _options);
        return cars;
    }

    public async Task<List<Car>> GetCarsBySeller(string seller)
    {
        var cacheKey = $"GetCarsBySeller-{seller}";
        var carsSerialized = await _cache.GetStringAsync(cacheKey);

        if(!string.IsNullOrEmpty(carsSerialized))
        {
            return JsonConvert.DeserializeObject<List<Car>>(carsSerialized);
        }

        var cars = await _decorated.GetCarsBySeller(seller);
        await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(cars), _options);
        return cars;
    }

    public async Task CreateCar(Car car)
    {
        var makeKey = $"GetCarsByMake-{car.Make}";
        var sellerKey = $"GetCarsByMake-{car.Seller}";
        var makeCarsSerialized = await _cache.GetStringAsync(makeKey);
        var sellerCarsSerialized = await _cache.GetStringAsync(sellerKey);
        if (!string.IsNullOrEmpty(makeCarsSerialized))
        {
            await _cache.RemoveAsync(makeKey);
        }
        if (!string.IsNullOrEmpty(sellerCarsSerialized))
        {
            await _cache.RemoveAsync(sellerKey);
        }

        await _decorated.CreateCar(car);
    }
}