using Payment.Domain.Models;

namespace Payment.Domain.ViewModel.Sale;

public class CreateSaleViewModel
{
    public Guid SalesmanId { get; set; }
    public List<CreateSaleItem> SaleItems { get; set; }
}

public class CreateSaleItem
{
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
}