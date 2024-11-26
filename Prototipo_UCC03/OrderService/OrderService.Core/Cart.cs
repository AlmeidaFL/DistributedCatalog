using OrderService.Persistence;

namespace OrderService.Core;

public record Cart
{
    public Guid Id { get; init; }
    public IReadOnlyList<Product> Products { get; set; } = new List<Product>();
    
    public double Total => Products.Sum(p => p.Price * p.Quantity);

    public Cart()
    {
        Id = Guid.NewGuid();
    }

    public Cart(Guid id, IReadOnlyList<Product> products)
    {
        Id = id;
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
            if (toBeEnrichedProduct != null)
            {
                toBeEnrichedProduct.Price = product.Price;
            }

            throw new Exception($"Product '{product.Id}' was not found");
        }
    }
}