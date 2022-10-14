using System.ComponentModel.DataAnnotations.Schema;

namespace Payment.Domain.Models;

public class SaleProductGroup 
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public Guid SaleId { get; set; }
        
    public Product? Product { get; set; }
    public Sale? Sale { get; set; }
}