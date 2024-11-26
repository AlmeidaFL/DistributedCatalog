using Microsoft.EntityFrameworkCore;
using OrderService.Core;

namespace OrderService.Persistence;

public class OrderDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<Cart> Carts { get; set; }
    
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }
}