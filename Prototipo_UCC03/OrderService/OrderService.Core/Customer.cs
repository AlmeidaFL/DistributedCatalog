namespace OrderService.Core;

public record Customer
{
    public Guid Id { get; init; }
    public string AddressId { get; init; } = string.Empty;
    public string PaymentOptionId { get; init; } = string.Empty;
}