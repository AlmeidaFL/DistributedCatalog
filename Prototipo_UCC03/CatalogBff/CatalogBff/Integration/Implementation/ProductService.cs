using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
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
                VendorId = product.VendorId,
                StockQuantity = product.StockQuantity,
                Name = product.Name,
                Description = product.Description,
                Price = (double)product.Price,
                Categories = { product.Categories },
                Image = new Image
                {
                    Data = ByteString.CopyFrom(product.Image)
                },
            });
        }
        
        await call.RequestStream.CompleteAsync();
        await call;
    }

    public async Task<IList<Product>> GetAllProducts()
    {
        var products = new List<Product>();
        using var call = client.GetAllProducts(new Empty());
        
        await foreach (var product in call.ResponseStream.ReadAllAsync())
        {
            products.Add(ToDomain(product));
        }

        return products;
    }

    public async Task<IList<Product>> GetProductsBySearch(string searchTerm)
    {
        var products = new List<Product>();
        using var call = client.GetProductsBy(new SearchTerm() { Value = searchTerm});
        
        await foreach (var product in call.ResponseStream.ReadAllAsync())
        {
            products.Add(ToDomain(product));
        }

        return products;
    }

    public async Task<Product> GetProductsById(string id)
    {
        var product = await client.GetProductByIdAsync(new GetProductByIdMessage() { ProductId = id });
        return ToDomain(product);
    }

    public async Task<IList<Product>> GetProductsByIds(IList<string> ids)
    {
        var products = new List<Product>();
        using var call = client.GetProductsByIds(new GetProductsByIdsMessage() { ProductId = { ids }});
        
        await foreach (var product in call.ResponseStream.ReadAllAsync())
        {
            products.Add(ToDomain(product));
        }

        return products;
    }

    public Task<Product> AddProduct(Product product)
    {
        return Task.FromResult(product);
    }

    private Product ToDomain(GrpcContracts.Product product)
    {
        return new Product(
            Id: product.Id,
            Categories: product.Categories.ToArray(),
            Description: product.Description,
            Image: product.Image.Data.ToByteArray(),
            Name: product.Name,
            Price: (decimal)product.Price,
            StockQuantity: product.StockQuantity,
            VendorId: product.VendorId
        );
    }
}