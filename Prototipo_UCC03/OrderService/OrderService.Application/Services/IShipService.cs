using OrderService.Core;
using OrderService.Persistence;

namespace OrderService.Application.Services.Implementation;

public interface IShipService
{
    public Task<ShippingInformation> GetShippingInformation(Order order);
    public Task CreateShip(Order order, Guid consumerId, Guid vendorId);
    public Task CancelShip(Guid orderId);
}