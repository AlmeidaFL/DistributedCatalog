namespace OrderService.Application.Saga.Events;

public interface IPaymentProcessed
{
    public Guid OrderId { get; }
    public bool Success { get; }
}