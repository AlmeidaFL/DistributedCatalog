using Grpc.Core;
using GrpcContracts.Order;
using MassTransit.Initializers;
using OrderService.Application.Services;

namespace OrderService.Controllers;


public class OrderService(IOrderService orderService) : GrpcContracts.Order.OrderService.OrderServiceBase
{
    public override async Task<OrderInformationRepeated> GetOrders(
        CustomerId request,
        ServerCallContext context)
    {
        var orders = await orderService.GetOrders(Guid.Parse(request.CustomerId_));
        return new OrderInformationRepeated()
        {
            CustomerId = request.CustomerId_,
            Orders =
            {
                orders.Select(o => new OrderInformation()
                {
                    Id = o.Id.ToString(),
                    PaymentId = o.PaymentId,
                    PaymentOptionId = o.PaymentOptionId,
                    Status = o.Status,
                    ShipmentFee = o.ShipmentFee,
                    TotalCart = o.TotalCart,
                    Product =
                    {
                        o.Products.Select(p => new Product()
                        {
                            Id = p.Id,
                            Price = p.Price,
                            Quantity = p.Quantity,
                        })
                    },
                    Address = new Address()
                    {
                        Street = o.Address.Street,
                        City = o.Address.City,
                        Number = o.Address.Number,
                        State = o.Address.State,
                        ZipCode = o.Address.ZipCode
                    }
                })
            }
        };
    }
}