namespace OrderService.Application.Saga.Events;

public class ShipmentCancelled
{
    public Guid OrderId { get; init; }
    public Guid CorrelationId { get; init; }
}