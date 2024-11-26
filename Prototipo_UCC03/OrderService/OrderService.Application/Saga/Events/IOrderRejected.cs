namespace OrderService.Application.Saga.Events;

public interface IOrderRejected
{
    public Guid OrderId { get; init; }
}