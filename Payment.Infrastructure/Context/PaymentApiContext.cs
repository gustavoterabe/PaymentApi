
using Microsoft.EntityFrameworkCore;
using Payment.Domain.Models;
using Payment.Infrastructure.Mapping;

namespace Payment.Infrastructure.Context;

public class PaymentApiContext: DbContext
{
    public PaymentApiContext(DbContextOptions<PaymentApiContext> options)
        : base(options) {}
    
    public virtual DbSet<Product>? Products { get; set; }
    public virtual DbSet<Salesman>? Salesmen { get; set; }
    public virtual DbSet<Sale>? Sales { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(new ProductMap().Configure);
        modelBuilder.Entity<Sale>(new SaleMap().Configure);
        modelBuilder.Entity<Salesman>(new SalesmanMap().Configure);
        modelBuilder.Entity<SalesStatus>(new SalesStatusMap().Configure);
    }
}