namespace CatalogService.Domain;

public record Image
{
    public int Id { get; init; }
    public Guid ProductId { get; init; }
    public byte[] Representation { get; init; }
}