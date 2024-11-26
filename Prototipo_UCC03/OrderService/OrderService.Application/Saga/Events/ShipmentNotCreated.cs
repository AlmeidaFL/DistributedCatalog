namespace OrderService.Application.Saga.Events;

public class ShipmentNotCreated
{
    public Guid CorrelationId { get; set;  }
    public Guid OrderId { get; init; }
}