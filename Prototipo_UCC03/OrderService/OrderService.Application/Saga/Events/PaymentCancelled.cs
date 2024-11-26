namespace OrderService.Application.Saga.Events;

public record PaymentCancelled
{
    public Guid CorrelationId { get; set;  }
    public Guid OrderId { get; init; }
};