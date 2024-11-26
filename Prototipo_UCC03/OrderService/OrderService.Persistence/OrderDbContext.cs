using Microsoft.EntityFrameworkCore;
using OrderService.Core;

namespace OrderService.Persistence;

public class OrderDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasKey(p => p.IdentityId);

        modelBuilder.Entity<Product>()
            .Property(p => p.IdentityId)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Order>(options =>
        {
            options.OwnsOne(o => o.Address);
        });
    }
}