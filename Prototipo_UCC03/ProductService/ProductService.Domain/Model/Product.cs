namespace ProductService.Domain.Model;

public record Product(string Name, string Description, decimal Price, Guid VendorId)
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ProductStatus ProductStatus { get; set; }
}

public enum ProductStatus
{
   Revision,
   Published,
   SoldOut,
   Disabled,
   Excluded
}