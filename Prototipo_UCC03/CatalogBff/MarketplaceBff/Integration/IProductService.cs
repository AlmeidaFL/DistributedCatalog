using MarketplaceBff.Domain;

namespace MarketplaceBff.Integration;

public interface IProductService
{
    public Task AddProducts(IReadOnlyList<Product> products);
    public Task<Product> AddProduct(Product product);
    public Task<IList<Product>> GetAllProducts();
    public Task<IList<Product>> GetProductsBySearch(string searchTerm);
    public Task<Product> GetProductsById(string id);
    public Task<IList<Product>> GetProductsByIds(IList<string> ids);
}