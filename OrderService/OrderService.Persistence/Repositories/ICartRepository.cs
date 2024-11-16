using OrderService.Core;

namespace OrderService.Persistence.Repositories;

public interface ICartRepository
{
    public Task AddToCart(Cart cart);
    
    public Task<Cart> GetCart(Guid cartId);
}