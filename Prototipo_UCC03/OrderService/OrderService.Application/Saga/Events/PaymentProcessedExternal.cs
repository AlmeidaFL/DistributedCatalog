namespace OrderService.Application.Saga.Events;

public record PaymentProcessedExternal
{
    public Guid CorrelationId { get; set;  }
    public Guid OrderId { get; init; }
};