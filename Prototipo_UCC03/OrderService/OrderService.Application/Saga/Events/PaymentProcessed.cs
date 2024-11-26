namespace OrderService.Application.Saga.Events;

public record PaymentProcessed
{
    public Guid PaymentId { get; init; }
    public Guid OrderId { get; init;  }
    public Guid CorrelationId { get; init;  }
}