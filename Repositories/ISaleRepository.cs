using PaymentApi.Models;
using PaymentApi.Utils.Constants;
using PaymentApi.ViewModel;

namespace PaymentApi.Repositories;

public interface ISaleRepository
{
    public Sale CreateAndReturnSale(CreateSaleViewModel sale);
    public Sale GetById(string id);
    public void ChangeStatus(SaleStatusEnum status, string id);
}