using MongoDB.Driver;

namespace CatalogService.Persistence;
using Domain;

public interface IProductRepository
{
    public Task<IAsyncCursor<Product>> GetAllProducts();
    public Task<IAsyncCursor<Product>> GetProductsBySearchTerm(string searchTerm);
    public Task<IEnumerable<Product>> GetProducts(int skip, int take);
    public Task<Product?> GetProductById(string id);
    public Task<IAsyncCursor<Product>> GetProductsByIdsAsync(IEnumerable<string> ids);
    public Task<IEnumerable<Product>> GetProductsByIds(IEnumerable<string> ids);
    public Task AddProduct(Product product);
    public Task UpdateProducts(IEnumerable<Product> products);
    public Task<IList<Product>> GetProductsReservedByCustomer(string customerId);
}

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> products;

    public ProductRepository(IMongoDatabase database)
    {
        products = database.GetCollection<Product>("Products");
    }

    public async Task AddProduct(Product product)
    {
        await products.InsertOneAsync(product);
    }

    public async Task<Product?> GetProductById(string id)
    {
        return await products.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProducts(int skip, int take)
    {
        return await products.Find(_ => true)
            .Skip(skip)
            .Limit(take)
            .ToListAsync();
    }

    public async Task<IAsyncCursor<Product>> GetAllProducts()
    {
        var filter = Builders<Product>.Filter.Empty;
        return await products.Find(filter).ToCursorAsync();
    }

    public async Task<IAsyncCursor<Product>> GetProductsBySearchTerm(string searchTerm)
    {
        var filter = Builders<Product>.Filter.Regex(p => p.Name, searchTerm);
        return await products.Find(filter).ToCursorAsync();
    }
    
    public async Task<IAsyncCursor<Product>> GetProductsByIdsAsync(IEnumerable<string> ids)
    {
        var filter = Builders<Product>.Filter.In(p => p.Id, ids);
        return await products.Find(filter).ToCursorAsync();
    }
    
    public async Task<IEnumerable<Product>> GetProductsByIds(IEnumerable<string> ids)
    {
        var filter = Builders<Product>.Filter.In(p => p.Id, ids);
        return await products.Find(filter).ToListAsync();
    }

    public async Task UpdateProducts(IEnumerable<Product> products)
    {
        var updateRequests = new List<WriteModel<Product>>();

        foreach (var product in products)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            var update = Builders<Product>.Update.Set(p => p.ReservationByCustomerId, product.ReservationByCustomerId);

            var updateOneRequest = new UpdateOneModel<Product>(filter, update);
            updateRequests.Add(updateOneRequest);
        }

        await this.products.BulkWriteAsync(updateRequests);
    }

    public async Task<IList<Product>> GetProductsReservedByCustomer(string customerId)
    {
        var filter = Builders<Product>.Filter.ElemMatch(p => p.ReservationByCustomerId, 
            r => r.Key == customerId); // Filtra pelas chaves do dicionário

        return await this.products.Find(filter).ToListAsync();
    }
}