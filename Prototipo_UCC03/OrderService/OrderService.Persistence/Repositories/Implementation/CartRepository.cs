using Microsoft.EntityFrameworkCore;
using OrderService.Core;

namespace OrderService.Persistence.Repositories.Implementation;

public class CartRepository(OrderDbContext orderDbContext) : ICartRepository
{
    public async Task AddToCart(Cart cart)
    {
        orderDbContext.Carts.Update(cart);
        await orderDbContext.SaveChangesAsync();
    }

    public Task<Cart> GetCart(Guid cartId)
    {
        throw new NotImplementedException();
    }
}