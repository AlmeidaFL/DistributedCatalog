namespace OrderService.Application.Saga.Events;

public record PaymentRejected()
{
    public Guid OrderId { get; init; }
    public string Reason { get; init; } = string.Empty;
    public Guid CorrelationId { get; set;  }
};