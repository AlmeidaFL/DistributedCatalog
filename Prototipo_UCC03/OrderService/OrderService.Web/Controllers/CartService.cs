using GrpcContracts.Cart;
using OrderService.Application.Services;

namespace OrderService.Controllers;
using Grpc = GrpcContracts;
using Grpc.Core;
using System.Threading.Tasks;

public class CartService(ICartService cartService) : CartServiceGrpc.CartServiceGrpcBase
{
    // Does not validate if user exists. If it doesn't, adds a product to it cart anyway
    public override async Task<Cart> AddProduct(AddProductRequest request, ServerCallContext context)
    {
        var userId = Guid.Parse(request.UserId);

        var product = new Core.Product()
        {
            Id = request.Product.Id,
            Quantity = request.Product.Quantity,
        };

        await cartService.AddProduct(userId, product);

        var updatedCart = await cartService.GetCart(userId);
        return MapCartToProto(updatedCart);
    }

    public override async Task<Cart> GetCart(GetCartRequest request, ServerCallContext context)
    {
        var cartId = Guid.Parse(request.CartId);

        var cart = await cartService.GetCart(cartId);

        return MapCartToProto(cart);
    }

    private static Cart MapCartToProto(Core.Cart cart)
    {
        return new Cart
        {
            UserId = cart.CustomerId.ToString(),
            Products = 
            {
                cart.Products.Select(p => new Product
                {
                    Id = p.Id.ToString(),
                    Quantity = p.Quantity,
                    Price = p.Price
                }) 
            },
            TotalPrice = cart.Total
        };
    }
}
