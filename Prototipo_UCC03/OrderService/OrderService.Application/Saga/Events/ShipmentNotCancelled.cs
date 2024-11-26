namespace OrderService.Application.Saga.Events;

public class ShipmentNotCancelled
{
    public Guid CorrelationId { get; set;  }
    public Guid OrderId { get; init; }
}