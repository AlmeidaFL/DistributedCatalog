namespace OrderService.Application.Saga.Events;

public interface IOrderRejected
{
    public Guid CorrelationId { get; set;  }
    public Guid OrderId { get; init; }
}