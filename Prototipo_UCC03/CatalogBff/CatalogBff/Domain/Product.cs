namespace CatalogBff.Domain;

public record Product(
    string VendorId,
    int StockQuantity,
    string Name, 
    string Description,
    decimal Price,
    string[] Categories,
    byte[] Image,
    string? Id = null);