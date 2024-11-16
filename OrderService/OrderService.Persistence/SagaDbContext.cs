using Microsoft.EntityFrameworkCore;

namespace OrderService.Persistence;

public class SagaDbContext(DbContextOptions<SagaDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}