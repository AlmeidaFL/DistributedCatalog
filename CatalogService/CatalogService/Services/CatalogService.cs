using CatalogService.Integration;
using CatalogService.Persistence;
using Grpc.Core;

namespace CatalogService.Services;

public class CatalogService(CatalogDbContext dbContext) : Integration.CatalogService.CatalogServiceBase
{
    public override Task GetProductsByIds(
        GetProductsByIdsMessage request,
        IServerStreamWriter<ProductWithoutImage> responseStream,
        ServerCallContext context)
    {
        var entities = dbContext.Products.Where(product => request.ProductId.Contains(product.Id.ToString()));
        foreach (var product in entities)
        {
            responseStream.WriteAsync(new ProductWithoutImage
            {   
                Id = product.Id.ToString(),
                Description = product.Description,
                Name = product.Name,
                Price = (double)product.Price,
            });
        }
        return base.GetProductsByIds(request, responseStream, context);
    }
}