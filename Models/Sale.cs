using PaymentApi.Utils.Constants;

namespace PaymentApi.Models;

public class Sale
{
    public string Id { get; set; }
    public Salesman Salesman { get; set; }
    public Product[] Products { get; set; }
    public SaleStatusEnum status { get; set; }
}