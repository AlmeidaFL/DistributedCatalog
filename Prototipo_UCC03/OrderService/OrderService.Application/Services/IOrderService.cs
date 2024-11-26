using OrderService.Core;
using OrderService.Persistence;

namespace OrderService.Application.Services;

public interface IOrderService
{
    public Task<Guid> CreateOrder(Customer customer);
    public Task CancelOrder(Guid orderId);
    public Task UpdateOrderStatus(Guid orderId, string status);
    public Task UpdateShipmentId(Guid orderId, string shipmentId);
    public Task UpdatePaymentId(Guid orderId, string paymentId);
    public Task ReserveProducts(Guid customerId);
    public Task UnreserveProducts(Guid customerId);
    public Task<IList<Order>> GetOrders(Guid customerId);
}