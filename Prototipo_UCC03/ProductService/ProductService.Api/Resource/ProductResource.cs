namespace ProductService.Api.Resource;

public record ProductResource(string Name, string Description, decimal Price, Guid VendorId);
