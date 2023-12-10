using API.Models;

namespace API.Data.repositories;

public interface ICarsRepository
{
    Task<List<Car>> GetCarsByMake(string make);
    Task<List<Car>> GetCarsBySeller(string seller);
    Task CreateCar(Car car);
}