using CatalogBff.Domain;
using GrpcContracts.Cart;
using Cart = CatalogBff.Domain.Cart;
using Product = GrpcContracts.Cart.Product;

namespace CatalogBff.Integration.Implementation;

using Grpc = GrpcContracts.Cart.CartServiceGrpc;

public class CartService(Grpc.CartServiceGrpcClient client) : ICartService
{
    public async Task<Cart> AddProductAsync(Guid userId, CartProduct product)
    {
        var request = new AddProductRequest
        {
            UserId = userId.ToString(),
            Product = new ProductRequest()
            {
                Id = product.Id,
                Quantity = product.Quantity
            }
        };

        var cart = await client.AddProductAsync(request);
        return ToModel(cart);
    }

    public async Task<Cart> GetCartAsync(string userId)
    {
        var request = new GetCartRequest
        {
            CartId = userId
        };

        var cart = await client.GetCartAsync(request);
        return ToModel(cart);
    }

    private Cart ToModel(GrpcContracts.Cart.Cart cart)
    {
        return new Cart()
        {
            TotalPrice = cart.TotalPrice,
            UserId = Guid.Parse(cart.UserId),
            Products = cart.Products.Select(p => new CartProduct()
            {
                Id = p.Id,
                Price = p.Price,
                Quantity = p.Quantity
            })
            .ToList(),
        };
    }
}