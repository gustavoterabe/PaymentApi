using Payment.Domain.Models;
using Payment.Domain.Utils.Constants;
using Payment.Domain.ViewModel.Sale;

namespace Payment.Domain.Interfaces;

public interface ISaleService : IBaseService<Sale>
{
    public void ChangeSaleStatus(Guid id, SaleStatusEnum saleStatus);
    public Sale CreateSale(CreateSaleViewModel createSaleViewModel);
}