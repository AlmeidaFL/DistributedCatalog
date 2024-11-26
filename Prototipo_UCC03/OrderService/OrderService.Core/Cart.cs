using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderService.Core;

public record Cart
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? InternalId { get; init; }
    
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid CustomerId { get; init; }
    public IReadOnlyList<Product> Products { get; set; } = new List<Product>();
    
    public double Total => Products.Sum(p => p.Price * p.Quantity);

    public Cart(Guid customerId)
    {
        CustomerId = customerId;
    }

    public Cart(Guid customerId, IReadOnlyList<Product> products)
    {
        CustomerId = customerId;
        ValidateProductsQuantity(products);
        Products = products;
    }

    private void ValidateProductsQuantity(IReadOnlyList<Product> products)
    {
        if (products.Any(p => p.Quantity <= 0))
        {
            throw new Exception("Product quantity must be greater than zero");
        }
    }

    public void EnrichProducts(IReadOnlyList<(string Id, double Price)> products)
    {
        foreach (var product in products)
        {
            var toBeEnrichedProduct = this.Products.FirstOrDefault(p => p.Id.ToString() == product.Id);
            if (toBeEnrichedProduct == null)
            {
                throw new Exception($"Product '{product.Id}' was not found");
            }
            
            toBeEnrichedProduct.Price = product.Price;
        }
    }
}