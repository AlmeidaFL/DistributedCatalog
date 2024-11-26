namespace CatalogBff.Domain;

public record Product(string Name, string Description, decimal Price, byte[] Image);