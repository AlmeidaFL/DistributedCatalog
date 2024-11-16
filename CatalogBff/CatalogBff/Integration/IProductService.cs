using CatalogBff.Domain;

namespace CatalogBff.Integration;

public interface IProductService
{
    public Task AddProducts(IReadOnlyList<Product> products);
    public Task<Product> AddProduct(Product product);
}