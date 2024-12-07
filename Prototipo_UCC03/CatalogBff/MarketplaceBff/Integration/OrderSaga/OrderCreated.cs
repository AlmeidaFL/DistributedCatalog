namespace OrderService.Application.Saga.Events;

public record OrderCreated
{
    public Guid CorrelationId { get; set; }
    public string DeliveryAddressId { get; set; }
    public string PaymentOptionId { get; set; }
}