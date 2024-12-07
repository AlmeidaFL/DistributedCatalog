namespace MarketplaceBff.Domain;

public record Cart
{
    public Guid UserId { get; init; }
    public IReadOnlyList<CartProduct> Products { get; init; } = new List<CartProduct>();
    public double TotalPrice { get; init; }
}