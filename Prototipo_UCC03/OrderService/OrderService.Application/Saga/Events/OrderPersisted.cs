namespace OrderService.Application.Saga.Events;

public record OrderPersisted
{ 
    public Guid OrderId { get; init; }
    public Guid CorrelationId { get; init; }
}