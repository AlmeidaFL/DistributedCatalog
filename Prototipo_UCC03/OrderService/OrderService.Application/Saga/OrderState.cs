using MassTransit;
using OrderService.Core;
using OrderService.Persistence;

namespace OrderService.Application.Saga;

public class OrderState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public Guid CustomerId { get; set; }
    public Guid PaymentId { get; set; }
    public Guid ShipmentId { get; set; }
    public Guid OrderId { get; set; }
    public string DeliveryAddressId { get; set; }
    public string PaymentOptionId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
