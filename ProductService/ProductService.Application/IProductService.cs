using ProductService.Domain.Model;

namespace Application;

public interface IProductService
{
    public Guid CreateProduct(Product product);
    public Product? GetProductById(Guid id);
    public IReadOnlyList<Product> GetVendorCatalogById(Guid id);
    public void Update(Product product); 
}
