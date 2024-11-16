namespace OrderService.Application.Saga.Events;

public interface IOrderApproved
{
    public Guid OrderId { get; init; }
}