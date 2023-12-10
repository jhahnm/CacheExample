using System.Text.Json;
using API.Models;
using MongoDB.Driver;
using MongoDB.Entities;

namespace API.Data;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        await DB.InitAsync("CarsDB",
            MongoClientSettings
                .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));
        await DB.Index<Car>()
            .Key(x => x.Make, KeyType.Text)
            .Key(x => x.Model, KeyType.Text)
            .Key(x => x.Color, KeyType.Text)
            .CreateAsync();

        var count = await DB.CountAsync<Car>();
        
        if (count == 0)
        {
            Console.WriteLine("No data - will attempt to seed");
            var itemData = await File.ReadAllTextAsync("Data/cars.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var cars = JsonSerializer.Deserialize<List<Car>>(itemData, options);
            await DB.SaveAsync(cars);
        }
    }
}