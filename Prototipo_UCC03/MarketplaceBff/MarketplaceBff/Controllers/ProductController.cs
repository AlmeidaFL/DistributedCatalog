using MarketplaceBff.Extensions;
using MarketplaceBff.Controllers.Resources;
using MarketplaceBff.Domain;
using MarketplaceBff.Integration;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceBff.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController(IProductService service) : ControllerBase
{
    [HttpPost()]
    public async Task CreateProducts([FromBody] IReadOnlyList<ProductResource> resources)
    {
        var products = resources.Select(r =>
            new Product(
                r.VendorId,
                r.StockQuantity,
                r.Name,
                r.Description,
                r.Price,
                r.Categories,
                r.Image.ConvertToBytes()));

        await service.AddProducts(products.ToList());
    }

    [HttpGet("all")]
    public async Task<IReadOnlyList<ProductResource>> GetAllProducts()
    {
        try
        {
            return (await service.GetAllProducts()).Select(ToResource).ToList();
        }
        catch (Exception ex)
        {
            return [];
        }
    }
    
    [HttpGet("/search/{searchTerm}")]
    public async Task<IReadOnlyList<ProductResource>> GetProductsBySearch(string searchTerm)
    {
        try
        {
            return (await service.GetProductsBySearch(searchTerm)).Select(ToResource).ToList();
        }
        catch (Exception ex)
        {
            return [];
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResource>> GetProductsByProductId(string id)
    {
        try
        {
            return ToResource(await service.GetProductsById(id));
        }
        catch (Exception ex)
        {
            return new ObjectResult(ex.Message)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
    
    [HttpPost("ids")]
    public async Task<IReadOnlyList<ProductResource>> GetProductsByIds([FromBody] IList<string> productIds)
    {
        try
        {
            return (await service.GetProductsByIds(productIds)).Select(ToResource).ToList();
        }
        catch (Exception ex)
        {
            return [];
        }
    }

    private static ProductResource ToResource(Product model)
    {
        return new ProductResource
        (
            Id: model.Id,
            Categories: model.Categories,
            Description: model.Description,
            Image: Convert.ToBase64String(model.Image).AddImageExtension(),
            Name: model.Name,
            VendorId: model.VendorId,
            Price: model.Price,
            StockQuantity: model.StockQuantity
        );
    }
}