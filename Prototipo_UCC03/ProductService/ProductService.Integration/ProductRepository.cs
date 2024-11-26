using System.Collections.Immutable;
using ProductService.Domain.Model;

namespace ProductService.Integration;

public class ProductRepository : IProductRepository
{
    private Dictionary<Guid, Product> productById = new Dictionary<Guid, Product>();
    
    public IReadOnlyList<Product> GetCatalogByVendorId(Guid id)
    {
        return productById.Values.Where(p => p.VendorId == id).ToList();
    }

    public Product? GetProductById(Guid id)
    {
        productById.TryGetValue(id, out var product);
        return product;
    }

    public void SaveProduct(Product product)
    {
        productById[product.Id] = product;
    }

    public void Update(Product product)
    {
        productById[product.Id] = product;
    }
}