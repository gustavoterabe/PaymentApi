using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Domain.Models;

namespace Payment.Infrastructure.Mapping;

public class SalesmanMap : IEntityTypeConfiguration<Salesman>
{
    public void Configure(EntityTypeBuilder<Salesman> builder)
    {
        builder.ToTable("Salesmen");

        builder.HasKey(sm => sm.Id);
        
        builder.Property(sm => sm.Id)
            .ValueGeneratedOnAdd();
    }
}