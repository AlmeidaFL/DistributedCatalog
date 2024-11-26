using CatalogBff.Domain;

namespace CatalogBff.Integration;

public interface IOrderService
{
    public Task<IList<Order>> GetOrdersByCustomerId(Guid customerId);
}