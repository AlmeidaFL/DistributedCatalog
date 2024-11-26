using System;
using System.Threading.Tasks;
using OrderService.Core;

namespace OrderService.Application.Services;

public interface ICartService
{
    public Task AddProduct(Guid userId, Product product);
    public Task AddCart(Cart cart);
    public Task<Cart> GetCart(Guid cartId);
    public Task ReserveProducts(Cart cart);
    public Task UnreserveProducts(Guid cartId);
}