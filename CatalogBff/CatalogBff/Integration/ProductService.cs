using CatalogService;
using Google.Protobuf;
using Product = CatalogBff.Domain.Product;

namespace CatalogBff.Integration;

using grpc = CatalogService.CatalogService;

public class ProductService(IGrpcClient<grpc.CatalogServiceClient> client)
{
    private IGrpcClient<grpc.CatalogServiceClient> grpcClient = null!;

    public async Task AddProducts(IEnumerable<Product> products)
    {
        if (!products.Any())
        {
            return;
        }
        
        var client = grpcClient.Create();

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