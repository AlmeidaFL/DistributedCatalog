using System.Collections.Immutable;
using ProductService.Domain.Model;

namespace ProductService.Integration;

public interface IProductRepository
{
    public IReadOnlyList<Product> GetCatalogByVendorId(Guid id);
    
    public Product? GetProductById(Guid id);
    public void SaveProduct(Product product);
    public void Update(Product product);
}