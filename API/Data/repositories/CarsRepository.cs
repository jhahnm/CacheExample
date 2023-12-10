using API.Models;
using MongoDB.Entities;

namespace API.Data.repositories;

public class CarsRepository: ICarsRepository
{
    public async Task<List<Car>> GetCarsByMake(string make)
    {
        var query = DB.Find<Car>();
        query.Match(c => c.Make == make);
        return await query.ExecuteAsync();
    }

    public async Task<List<Car>> GetCarsBySeller(string seller)
    {
        var query = DB.Find<Car>();
        query.Match(c => c.Seller == seller);
        return await query.ExecuteAsync();
    }

    public async Task CreateCar(Car car)
    {
        car.ID = Guid.NewGuid().ToString();
        await car.SaveAsync();
    }
}