using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Domain.Models;
using Payment.Domain.Utils.Constants;

namespace Payment.Infrastructure.Mapping;

public class SaleMap : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");
        
        builder.HasKey(s => s.Id);
        
        builder
            .HasOne(s => s.Salesman)
            .WithMany(sm => sm.Sales)
            .HasForeignKey(s => s.SalesmanId);
        
        builder
            .HasMany(s => s.SaleProductGroups)
            .WithOne(spg => spg.Sale)
            .HasForeignKey(spg => spg.SaleId);
        
        builder
            .HasOne<SalesStatus>()
            .WithMany(ss => ss.Sales)
            .HasForeignKey(s => s.StatusId);
        
        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd();

        builder.Property(s => s.StatusId)
            .HasDefaultValue(SaleStatusEnum.WaitingForPayment);
        
        builder.Property(s => s.CreatedAt)
            .HasDefaultValue(DateTime.Now);
        
    }
    
}