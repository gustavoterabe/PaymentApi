using PaymentApi.Models;

namespace PaymentApi.ViewModel;

public class CreateSaleViewModel
{
    public Salesman Salesman { get; set; }
    public Product[] Products { get; set; }
}