using MassTransit;
using OrderService.Core;
using OrderService.Persistence;

namespace OrderService.Application.Saga;

public class OrderState : SagaStateMachineInstance
{
    public Guid OrderId { get; set; }
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public IList<Product> Products { get; set; } = new List<Product>();
}
