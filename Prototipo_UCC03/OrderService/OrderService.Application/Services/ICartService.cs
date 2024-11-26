using OrderService.Core;
using OrderService.Persistence;
using OrderService.Persistence.Repositories;

namespace OrderService.Application.Services;

public interface ICartService
{
    public Task AddToCart(Cart cart);
    public Task<Cart> GetCart(Guid idCart);
}