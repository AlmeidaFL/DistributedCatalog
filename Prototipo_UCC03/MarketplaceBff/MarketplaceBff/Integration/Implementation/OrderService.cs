using GrpcContracts.Order;
using MarketplaceBff.Domain;
using Address = MarketplaceBff.Domain.Address;
using Domain_Address = MarketplaceBff.Domain.Address;

namespace MarketplaceBff.Integration.Implementation;

public class OrderService(GrpcContracts.Order.OrderService.OrderServiceClient client) : IOrderService
{
    public async Task<IList<Order>> GetOrdersByCustomerId(Guid customerId)
    {
        var x = await client.GetOrdersAsync(new CustomerId()
        {
            CustomerId_ = customerId.ToString()
        });

        return x.Orders.Select(o => new Order
        {
            CustomerId = customerId,
            Id = Guid.Parse(o.Id),
            PaymentId = o.PaymentId,
            PaymentOptionId = o.PaymentOptionId,
            ShipmentFee = o.ShipmentFee,
            ShipmentId = o.ShipmentId,
            Status = o.Status,
            TotalCart = o.TotalCart,
            Products = o.Product.Select(x => new ProductHeader()
            {
                Id = Guid.Parse(x.Id),
                Quantity = x.Quantity,
                Price = x.Price,
            }).ToList(),
            Address = new Domain_Address()
            {
                City = o.Address.City,
                Number = o.Address.Number,
                Street = o.Address.Street,
                State = o.Address.State,
                ZipCode = o.Address.ZipCode,
            }
        }).ToList();
    }
}