namespace OrderService.Persistence;

public record Order
{
    public Guid OrderId { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public string Status { get; init; } = string.Empty;
}