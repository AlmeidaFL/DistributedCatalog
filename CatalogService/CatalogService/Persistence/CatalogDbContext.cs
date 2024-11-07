using System.Reflection;
using CatalogService.Domain;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Persistence;

public class CatalogDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Image> Images { get; set; }

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .ToTable(t => t.HasCheckConstraint("CK_Product_Price", "Price >= 0"));

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetCallingAssembly());
    }
}