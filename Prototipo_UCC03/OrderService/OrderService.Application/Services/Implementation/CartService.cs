using CatalogService;
using Grpc.Core;
using MassTransit.Internals;
using OrderService.Core;
using OrderService.Persistence.Repositories;
using Catalog = CatalogService.CatalogService;

namespace OrderService.Application.Services.Implementation;

public class CartService(ICartRepository repository, Catalog.CatalogServiceClient client) : ICartService
{
    public async Task AddToCart(Cart cart)
    {
        await repository.AddToCart(cart);
    }

    public async Task<Cart> GetCart(Guid idCart)
    {
        var cart = await repository.GetCart(idCart);
        var productsPrice = await GetProductsPrice();
        cart.EnrichProducts(productsPrice.Select(p => (p.Id, p.Price)).ToList());

        return cart;

        async Task<IList<ProductWithoutImage>> GetProductsPrice()
        {
            var call = client.GetProductsByIds(
                new GetProductsByIdsMessage
                {
                    ProductId = { cart.Products.Select(p => p.Id.ToString()) }
                });
            var productWithoutImages = await call.ResponseStream.ReadAllAsync().ToListAsync();
            return productWithoutImages;
        }
    }
}