using Payment.Domain.Utils.Constants;

namespace Payment.Domain.Models;

public class Sale : BaseEntity
{
    public SaleStatusEnum StatusId { get; set; } = SaleStatusEnum.WaitingForPayment;
    public Guid SalesmanId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public IList<SaleProductGroup> SaleProductGroups { get; set; }
    public Salesman Salesman { get; set; }
}
