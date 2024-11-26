using OrderService.Core;

namespace OrderService.Application.Saga.Events;

public interface IOrderCreated
{
    public Guid OrderId { get; set; }
    public IList<Product> Products { get; set; }
}