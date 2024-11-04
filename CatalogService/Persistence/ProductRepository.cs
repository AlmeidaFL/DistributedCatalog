using CatalogService.Domain;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Persistence;

public interface IProductRepository
{
    public Task<IEnumerable<Product>> GetProducts(int skip, int take);
    public Task<Product?> GetProductByIdEntity(Guid id);
    public Product? GetProductByIdObject(Guid id);
}

public class ProductRepository : IProductRepository
{
    private readonly CatalogDbContext dbContext;

    public ProductRepository(CatalogDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Product? GetProductByIdObject(Guid id)
    {
        var product = dbContext.Products.Join(
            dbContext.Images,
            p => p.ImageId,
            i => i.Id,
            (product, image) => new Product()
                {
                 Price   = product.Price,
                 Description = product.Description,
                 ImageId = image.Id,
                 Image = image,
                 Id = product.Id,
                 Name = product.Name,
                })
            .FirstOrDefault();
        
        return product;
    }

    public async Task<Product?> GetProductByIdEntity(Guid id)
    {
        var product = await dbContext.Products
            .Include(p => p.Image)
            .SingleOrDefaultAsync(p => p.Id == id);

        return product;
    }

    public async Task<IEnumerable<Product>> GetProducts(int skip, int take)
    {
        return await dbContext.Products
            .Include(x => x.Image)
            .Skip(skip)
            .Take(take)
            .ToArrayAsync();
    }

    public async Task AddProducts(IReadOnlyList<Product> products)
    {
        await dbContext.Products.AddRangeAsync(products);
    }
}