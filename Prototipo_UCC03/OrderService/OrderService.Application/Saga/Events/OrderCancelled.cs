namespace OrderService.Application.Saga.Events;

public record OrderCancelled
{
    public Guid CorrelationId { get; set;  }
    public Guid OrderId { get; init; }
};