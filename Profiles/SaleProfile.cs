using AutoMapper;
using PaymentApi.Models;
using PaymentApi.ViewModel;

namespace PaymentApi.Profiles;

public class SaleProfile: Profile
{
    public SaleProfile()
    {
        CreateMap<Sale, CreateSaleViewModel>();
    }
}