using CatalogBff.Controllers.Resources;
using CatalogBff.Domain;
using CatalogBff.Extensions;
using CatalogBff.Integration;
using Microsoft.AspNetCore.Mvc;

namespace CatalogBff.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController(IProductService service) : ControllerBase
{
    [HttpPost()]
    public async Task CreateProducts([FromBody] IReadOnlyList<ProductResource> resources)
    {
        var products = resources.Select(r => 
            new Product(r.Name, r.Description, r.Price, r.Image.ConvertToBytes()));
        
        await service.AddProducts(products.ToList());
    }
}