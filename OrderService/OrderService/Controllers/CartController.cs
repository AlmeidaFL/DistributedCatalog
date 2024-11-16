using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Services;
using OrderService.Controllers.Resources;
using OrderService.Core;
using OrderService.Persistence;

namespace OrderService.Controllers;

[ApiController]
[Route("/api/cart")]
public class CartController(ICartService service)
{
    [HttpPut]
    public async Task AddProduct(CartResource resource)
    {
        resource.Id ??= Guid.NewGuid();
        var cart = new Cart(resource.Id.Value, resource.Products);
        await service.AddToCart(cart);
    }

    [HttpGet]
    public async Task<Cart> GetCart(Guid cartId)
    {
        return await service.GetCart(cartId);
    }
}