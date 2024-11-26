using OrderService.Core;

namespace OrderService.Application.Saga.Events;

public record PreOrderReceived
{
    public Guid CorrelationId { get; set;  }
    public string CustomerId { get; set; }
}