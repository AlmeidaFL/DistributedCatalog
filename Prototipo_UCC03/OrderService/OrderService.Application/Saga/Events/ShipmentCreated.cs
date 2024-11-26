namespace OrderService.Application.Saga.Events;

public class ShipmentCreated
{
    public Guid CorrelationId { get; set;  }
    public Guid ShipmentId { get; set;  }
    public decimal ShippingCost { get; set; } = 15;
    public Guid OrderId { get; init; }
}