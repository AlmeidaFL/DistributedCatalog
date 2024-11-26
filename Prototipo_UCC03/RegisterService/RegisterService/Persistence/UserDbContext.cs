using Microsoft.EntityFrameworkCore;
using RegisterService.Domain;

namespace RegisterService.Persistence;

public class UserDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public UserDbContext(DbContextOptions<UserDbContext> options): base(options){}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion(
                role => role.ToString(),
                value => new Role(value)
            );
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.DeliveryAddresses)
            .WithOne(d => d.User)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.PaymentCards)
            .WithOne(d => d.User)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}