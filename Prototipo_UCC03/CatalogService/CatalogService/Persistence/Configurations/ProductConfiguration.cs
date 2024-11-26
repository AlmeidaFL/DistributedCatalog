using CatalogService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasDefaultValueSql("NEWID()");
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .IsRequired();
        
        builder.Property(x => x.Price)
            .HasColumnType("decimal")
            .HasPrecision(2)
            .HasDefaultValueSql("0")
            .IsRequired();
        
        builder.HasOne(p => p.Image)
            .WithOne()
            .HasForeignKey<Image>(x => x.ProductId)
            .IsRequired(false);
    }
}