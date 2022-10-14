using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Domain.Models;

namespace Payment.Infrastructure.Mapping;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);
        
        builder.HasMany(p => p.SaleProductGroups)
            .WithOne(spg => spg.Product)
            .HasForeignKey(spg => spg.ProductId);
        
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Price)
            .HasPrecision(18, 2);
        
    }
}