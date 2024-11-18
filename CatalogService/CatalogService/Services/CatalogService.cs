using CatalogService.Persistence;
using Grpc.Core;
using GrpcContracts;

namespace CatalogService.Services;

public class CatalogService(CatalogDbContext dbContext) : GrpcContracts.CatalogService.CatalogServiceBase
{
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
}