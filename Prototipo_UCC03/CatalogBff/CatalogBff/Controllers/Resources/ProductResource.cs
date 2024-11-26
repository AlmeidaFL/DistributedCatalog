namespace CatalogBff.Controllers.Resources;

public record ProductResource(
    string VendorId,
    int StockQuantity,
    string Name,
    string Description,
    decimal Price,
    string[] Categories,
    string Image, // We just support jpeg for now
    string? Id = null);