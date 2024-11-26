using CatalogBff.Controllers.Resources;
using CatalogBff.Domain;
using CatalogBff.Extensions;
using CatalogBff.Integration;

namespace CatalogBff.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/cart/{userId}")]
public class CartController(ICartService cartService, IProductService productService) : ControllerBase
{
    private readonly ICartService cartService = cartService;

    [HttpPut("product")]
    public async Task<ActionResult<Cart>> AddProduct(string userId, [FromBody] CartProduct product)
    {
        try
        {
            var updatedCart = await cartService.AddProductAsync(Guid.Parse(userId), product);
            return updatedCart;
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<CartResource>> GetCart(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("UserId cannot be null or empty.");
        }

        var cart = await cartService.GetCartAsync(userId);
        var catalogItems = await productService.GetProductsByIds(
            cart.Products.Select(p => p.Id.ToString()).ToList());
        
        // Enrich products with image
        var resource = new CartResource
        {
            Id = userId,
            Total = cart.TotalPrice,
            Products = catalogItems.Select(p => new ProductCartResource
            {
                Id = p.Id,
                Image = Convert.ToBase64String(p.Image).AddImageExtension(),
                Name = p.Name,
                Price = (double)p.Price,
                Quantity = cart.Products.First(cp => cp.Id == p.Id).Quantity

            }).ToList(),
        };
        
        return resource;
    }
}
