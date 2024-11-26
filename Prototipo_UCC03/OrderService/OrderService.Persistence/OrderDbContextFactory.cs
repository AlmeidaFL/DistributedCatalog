namespace OrderService.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class OrderDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
{
    public OrderDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();
        
        optionsBuilder.UseMySql(
            "server=localhost;port=3306;database=OrderDb;user=root;password=password;",
            ServerVersion.AutoDetect("server=localhost;port=3306;database=OrderDb;user=root;password=password;")
        );

        return new OrderDbContext(optionsBuilder.Options);
    }
}
