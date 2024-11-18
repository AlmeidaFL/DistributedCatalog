using Google.Protobuf;
using GrpcContracts;
using Product = CatalogBff.Domain.Product;

namespace CatalogBff.Integration.Implementation;

using Grpc = CatalogService;

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
            await call.RequestStream.WriteAsync(new GrpcContracts.Product()
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

    public Task<Product> AddProduct(Product product)
    {
        return Task.FromResult(product);
    }
}