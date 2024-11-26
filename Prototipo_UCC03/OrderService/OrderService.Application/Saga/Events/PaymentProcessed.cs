namespace OrderService.Application;

public interface PaymentProcessed
{
    public Guid OrderId { get; init; }
}