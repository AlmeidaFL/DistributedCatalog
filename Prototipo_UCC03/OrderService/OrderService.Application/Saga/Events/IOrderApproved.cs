namespace OrderService.Application.Saga.Events;

public interface IOrderApproved
{
    public Guid CorrelationId { get; set;  }
    public Guid OrderId { get; init; }
}