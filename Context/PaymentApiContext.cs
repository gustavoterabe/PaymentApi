using Microsoft.EntityFrameworkCore;
using PaymentApi.Models;

namespace PaymentApi.Context;

public class PaymentApiContext: DbContext
{
    public PaymentApiContext(DbContextOptions<PaymentApiContext> options)
        : base(options) {}
    
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Salesman> Salesmen { get; set; }
    public virtual DbSet<Sale> Sales { get; set; }
    
}