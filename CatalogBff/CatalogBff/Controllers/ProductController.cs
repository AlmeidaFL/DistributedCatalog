using CatalogBff.Controllers.Resources;
using CatalogBff.Domain;
using CatalogBff.Integration;
using Microsoft.AspNetCore.Mvc;

namespace CatalogBff.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController(IProductService service)
{
    [HttpPost()]
    public async Task CreateProducts([FromBody] IEnumerable<ProductResource> resources)
    {
        var products = await Task.WhenAll(resources.Select(async r =>
        {
            using var stream = new MemoryStream();
            await r.Image.CopyToAsync(stream);

            return new Product(r.Name, r.Description, r.Price, stream.ToArray());
        }));
        
        await service.AddProducts(products.ToList());
    }
}