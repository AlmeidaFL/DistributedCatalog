using CatalogService;
using Google.Protobuf;
using Product = CatalogBff.Domain.Product;

namespace CatalogBff.Integration.Implementation;

using Grpc = CatalogService.CatalogService;

public class ProductService(Grpc.CatalogServiceClient client) : IProductService
{
    public async Task AddProducts(IReadOnlyList<Product> products)
    {
        if (!products.Any())
        {
            return;
        }

        using var call = client.AddProduct();

        foreach (var product in products)
        {
            await call.RequestStream.WriteAsync(new CatalogService.Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = (double)product.Price,
                Image = new Image
                {
                    Data = ByteString.CopyFrom(product.Image)
                }
            });
        }
    }
}