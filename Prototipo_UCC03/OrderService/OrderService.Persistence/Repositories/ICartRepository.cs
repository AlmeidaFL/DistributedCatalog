using OrderService.Core;

namespace OrderService.Persistence.Repositories;

public interface ICartRepository
{
    public Task AddProduct(Guid userId, Product product);
    public Task AddToCart(Cart cart);
    public Task CreateCart(Cart cart);
    
    public Task<Cart> GetCart(Guid cartId);

    public Task<Cart?> GetCartOrNull(Guid cartId);
}