namespace CatalogBff.Controllers.Resources;

public record CartResource
{
    // Same Id as Customer. Client deserialize, so don't change name
    public string Id { get; init; }
    public IReadOnlyList<ProductCartResource> Products { get; init; } = new List<ProductCartResource>();
    public double Total { get; init; }
}

public record ProductCartResource
{
    public string Id { get; init; }
    public string Name { get; init; }
    // Only thing relative to customer
    public int Quantity { get; init; }
    public double Price { get; init; }
    public string Image { get; init; }
}