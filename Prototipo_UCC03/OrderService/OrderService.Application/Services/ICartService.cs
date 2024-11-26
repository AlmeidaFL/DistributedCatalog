using System;
using System.Threading.Tasks;
using OrderService.Core;

namespace OrderService.Application.Services;

public interface ICartService
{
    public Task AddToCart(Cart cart);
    public Task<Cart> GetCart(Guid idCart);
}