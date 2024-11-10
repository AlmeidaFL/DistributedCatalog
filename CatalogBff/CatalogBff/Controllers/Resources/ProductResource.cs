namespace CatalogBff.Controllers.Resources;

public record ProductResource(string Name, string Description, decimal Price, IFormFile Image);