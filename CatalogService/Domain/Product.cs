namespace CatalogService.Domain;

public record Product
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public string Description { get; init; }
    public Image? Image { get; init; }
    public int ImageId { get; init; }
}