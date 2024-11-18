using CatalogService.Persistence;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcContracts;
using Image = CatalogService.Domain.Image;

namespace CatalogService.Services;

public class CatalogService: GrpcContracts.CatalogService.CatalogServiceBase
{
    private readonly CatalogDbContext dbContext;

    public CatalogService(CatalogDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public override async Task GetProductsByIds(
        GetProductsByIdsMessage request,
        IServerStreamWriter<ProductWithoutImage> responseStream,
        ServerCallContext context)
    {
        var entities = dbContext.Products.Where(product => request.ProductId.Contains(product.Id.ToString()));
        foreach (var product in entities)
        {
            await responseStream.WriteAsync(new ProductWithoutImage
            {   
                Id = product.Id.ToString(),
                Description = product.Description,
                Name = product.Name,
                Price = (double)product.Price,
            });
        }
    }

    public override async Task<Empty> AddProduct(IAsyncStreamReader<Product> requestStream, ServerCallContext context)
    {
        await foreach (var product in requestStream.ReadAllAsync())
        {
            var entity = new Domain.Product()
            {
                Name = product.Name,
                Description = product.Description,
                Price = (decimal)product.Price,
                Image = new Image
                {
                    Representation = product.Image.Data.ToByteArray(),
                }
            };
            Console.WriteLine(entity);
            dbContext.Products.Add(entity);
        }
        
        await dbContext.SaveChangesAsync();
        
        return await Task.FromResult(new Empty());
    }
}