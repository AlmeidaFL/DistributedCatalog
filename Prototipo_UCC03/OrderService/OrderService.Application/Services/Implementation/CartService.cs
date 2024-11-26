using Grpc.Core;
using GrpcContracts;
using MassTransit.Internals;
using OrderService.Core;
using OrderService.Persistence.Repositories;
using Product = OrderService.Core.Product;

namespace OrderService.Application.Services.Implementation;

public class CartService(ICartRepository repository, CatalogService.CatalogServiceClient client) : ICartService
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

    public async Task<Guid> ReserveProducts(IReadOnlyList<Product> products)
    {
        await Task.Factory.StartNew(() =>
        {
            var reservedResult = client.ReserveProducts(new ReserveProductsMessage()
            {
                ProductIds = { products.Select(p => p.Id.ToString()) }
            });

            if (reservedResult.ErrorMessage is not null)
            {
                throw new Exception("Error occured while reserving products");
            }
            
            return reservedResult.ReservedCartId;
        });

        // Should not pass to here
        return new Guid();
    }
}