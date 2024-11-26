namespace CatalogBff.Domain;

public record CartProduct
{
    public string Id { get; init; }
    public int Quantity { get; init; }
    public double? Price { get; init; }
}