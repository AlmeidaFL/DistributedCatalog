using Grpc.Core;
using GrpcContracts;
using MassTransit.Internals;
using OrderService.Application.Exceptions;
using OrderService.Core;
using OrderService.Persistence.Repositories;
using Product = OrderService.Core.Product;

namespace OrderService.Application.Services.Implementation;

public class CartService(ICartRepository repository, CatalogService.CatalogServiceClient client) : ICartService
{
    public async Task AddProduct(Guid userId, Product product)
    {
        try
        {
            var productHeader = client.GetProductHeaderById(
                new GetProductByIdMessage() { ProductId = product.Id.ToString() }
            );
            
            var cart = await repository.GetCartOrNull(userId);
            var persistedProductCartQuantity 
                = cart?.Products?.FirstOrDefault(p => p.Id == product.Id)?.Quantity ?? 0;
            if (persistedProductCartQuantity + product.Quantity > productHeader.StockQuantity)
            {
                throw new InvalidOperationException($"Our stock product does not have enough quantity");
            }
            
            product.Price = productHeader.Price;
            await repository.AddProduct(userId, product);
        }
        catch (Exception ex) when (ex is not InvalidOperationException)
        {
            throw new Exception("Product does not exist in catalog");
        }
    }

    public async Task AddCart(Cart cart)
    {
        await repository.AddToCart(cart);
    }

    public async Task<Cart> GetCart(Guid cartId)
    {
        var cart = await repository.GetCart(cartId);
        var productsPrice = await GetProductsPrice();
        cart.EnrichProducts(productsPrice.Select(p => (p.Id, p.Price)).ToList());

        return cart;

        async Task<IList<ProductWithoutImage>> GetProductsPrice()
        {
            var call = client.GetProductsByIdsWithoutImage(
                new GetProductsByIdsMessage
                {
                    ProductId = { cart.Products.Select(p => p.Id.ToString()) }
                });
            var productWithoutImages = await call.ResponseStream.ReadAllAsync().ToListAsync();
            return productWithoutImages;
        }
    }

    public async Task ReserveProducts(Cart cart)
    {
        await Task.Factory.StartNew(() =>
        {
            var reservedResult = client.ReserveProducts(new ReserveProductsMessage()
            {
                CustomerId= cart.CustomerId.ToString(),
                ReservedProducts =
                {
                    cart.Products.Select(p => 
                        new ReserveProductsMessage.Types.ProductX()
                        {
                            ProductId = p.Id.ToString(),
                            Quantity = p.Quantity
                        }) 
                }
            });

            if (reservedResult.IsError)
            {
                throw new ReserveProductException(
                    cart.CustomerId,
                    $"Error occured while reserving products - {reservedResult.Message}");
            }
        });
    }   
    
    public async Task UnreserveProducts(Guid cartId)
    {
        await Task.Factory.StartNew(() =>
        {
            var reservedResult = client.UnreserveProducts(new CustomerId()
            {
                CustomerId_ = cartId.ToString()
            });

            if (reservedResult.IsError)
            {
                throw new Exception($"Error occured while reserving products - {reservedResult.Message}");
            }
        });
    }
}