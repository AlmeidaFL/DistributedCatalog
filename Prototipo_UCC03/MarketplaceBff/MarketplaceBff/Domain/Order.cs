namespace MarketplaceBff.Domain;

public record Order
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public string PaymentOptionId { get; init; } = string.Empty;
    public Address Address { get; init; }
    public string ShipmentId { get; set; } = string.Empty;
    public string PaymentId { get; set; } = string.Empty;
    public double TotalCart { get; init; }
    public double ShipmentFee { get; init; }
    public IList<ProductHeader> Products { get; init; }
    public string Status { get; set; } = string.Empty;
}

public record Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Number { get; set; }
}

public record ProductHeader
{
    public Guid Id { get; init; }
    public double Price { get; init; }
    public string Name { get; init; }
    public double Quantity { get; init; }
}