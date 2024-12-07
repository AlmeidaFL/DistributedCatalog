using MarketplaceBff.Domain;

namespace MarketplaceBff.Integration;

public interface IOrderService
{
    public Task<IList<Order>> GetOrdersByCustomerId(Guid customerId);
}