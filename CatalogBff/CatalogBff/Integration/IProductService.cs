using CatalogBff.Domain;

namespace CatalogBff.Integration;

public interface IProductService
{
    public Task AddProducts(IReadOnlyList<Product> products);
}