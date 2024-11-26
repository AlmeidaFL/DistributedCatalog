using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CatalogService.Domain;

public class Reservation
{
    [BsonIgnore]
    public string ProductId { get; set; }
    public double Quantity { get; set; }
    public DateTime ReservedAt { get; set; } = DateTime.UtcNow;
}