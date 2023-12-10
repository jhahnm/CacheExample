using MongoDB.Entities;

namespace API.Models;

public class Car: Entity
{
    public int ReservePrice { get; set; }
    public string Seller { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Status { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string Color { get; set; }
    public int Mileage { get; set; }
}