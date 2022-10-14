using Payment.Domain.Utils.Constants;

namespace Payment.Domain.Models;

public class SalesStatus
{
    public SaleStatusEnum Id { get; set; }
    public string Name { get; set; }
    
    
    public List<Sale>? Sales { get; set; }
}