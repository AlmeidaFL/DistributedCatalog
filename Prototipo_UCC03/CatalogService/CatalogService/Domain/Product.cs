using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace CatalogService.Domain;

using CustomerId = string;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid VendorId { get; init; }
    public int StockQuantity { get; init; }
    public string Name { get; init; }
    public string[] Categories { get; init; }
    public decimal Price { get; init; }
    public string Description { get; init; }
    public IList<Image> Images { get; init; } = new List<Image>();
    [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)]
    public Dictionary<CustomerId, Reservation> ReservationByCustomerId = new();
    
    public double QuantityReserved => ReservationByCustomerId.Values.Sum(v => v.Quantity);
}