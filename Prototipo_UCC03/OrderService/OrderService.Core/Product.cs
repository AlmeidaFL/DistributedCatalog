using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OrderService.Persistence;

namespace OrderService.Core;

public class Product
{
    // To be used in E.F Core
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [BsonIgnore]
    public Guid IdentityId { get; set; }
    
    // Tech Debt: Entity Framework was populating Id as primary key. It was necesseary to add IdentityId so we 
    // can add same products for other orders
    public string Id { get; set; } 
    public int Quantity { get; set; }
    public double Price { get; set; }
    
    [BsonIgnore]
    public Guid OrderId { get; set; }
    [BsonIgnore]
    public Order Order { get; set; }
}