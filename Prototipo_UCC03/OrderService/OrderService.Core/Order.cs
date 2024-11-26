using OrderService.Core; 

namespace OrderService.Persistence;

public record Order
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public string AddressId { get; init; } = string.Empty;
    public string PaymentOptionId { get; init; } = string.Empty;
    public Address Address { get; init; }
    public string ShipmentId { get; set; } = string.Empty;
    public string PaymentId { get; set; } = string.Empty;
    public double TotalCart { get; init; }
    public double ShipmentFee { get; init; }
    public IList<Product> Products { get; init; }
    public string Status { get; set; } = string.Empty;
}