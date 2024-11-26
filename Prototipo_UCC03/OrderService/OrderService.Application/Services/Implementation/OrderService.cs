using Microsoft.EntityFrameworkCore;
using OrderService.Application.Exceptions;
using OrderService.Core;
using OrderService.Integration;
using OrderService.Persistence;

namespace OrderService.Application.Services.Implementation;

public class OrderService(
    ICartService cartService,
    RegisterServiceClient registerClient,
    OrderDbContext database) : IOrderService
{
    private readonly OrderDbContext database = database;
    private readonly ICartService cartService = cartService;
    
    public async Task<Guid> CreateOrder(Customer customer)
    {
        var cart = await cartService.GetCart(customer.Id);
        if (!cart.Products.Any())
        {
            throw new LackOfProductOnCartException($"There is not products on customer {customer.Id} cart");
        }
        
        var address = await registerClient.GetAddressById(customer.Id, customer.AddressId)
            ?? throw new AddressNotFoundException($"Address coming from order not found on database");
        
        var order = new Order()
        {
            Address = address,
            CustomerId = customer.Id,
            ShipmentFee = ShipmentService.GetShipmentCost(),
            PaymentOptionId = customer.PaymentOptionId,
            AddressId = customer.AddressId,
            Products = cart.Products.ToList(),
            Status = OrderStatus.Pending.ToString()
        };
        
        await database.Orders.AddAsync(order);
        await database.SaveChangesAsync();
        return database.Orders.First(o => o.Id == order.Id).Id;
    }
    
    public async Task CancelOrder(Guid orderId)
    {
        var order = await database
            .Orders
            .AsTracking()
            .FirstOrDefaultAsync(o => o.Id == orderId);
        
        if (order == null)
        {
            throw new Exception($"Order with id {orderId} not found");        
        }
    
        order.Status = OrderStatus.Cancelled.ToString();
        await database.SaveChangesAsync();
    }
    
    public async Task UpdateOrderStatus(Guid orderId, string status)
    {
        var order = await database
            .Orders
            .AsTracking()
            .FirstOrDefaultAsync(o => o.Id == orderId);
        
        if (order != null)
        {
            order.Status = status;
        }
    }
    
    public async Task UpdatePaymentId(Guid orderId, string paymentId)
    {
        var order = await database
            .Orders
            .AsTracking()
            .FirstOrDefaultAsync(o => o.Id == orderId);
        if (order != null)
        {
            order.PaymentId = paymentId;
        }
    }
    
    public async Task UpdateShipmentId(Guid orderId, string shipmentId)
    {
        var order = await database
            .Orders
            .AsTracking()
            .FirstOrDefaultAsync(o => o.Id == orderId);
        
        if (order != null)
        {
            order.ShipmentId = shipmentId;
        }

        await database.SaveChangesAsync();
    }
    
    public async Task ReserveProducts(Guid customerId)
    {
        var cart = await cartService.GetCart(customerId);
        await cartService.ReserveProducts(cart);
    }
    
    public async Task UnreserveProducts(Guid customerId)
    {
        await cartService.UnreserveProducts(customerId);
    }

    public async Task<IList<Order>> GetOrders(Guid customerId)
    {
        return await database.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
    }
}