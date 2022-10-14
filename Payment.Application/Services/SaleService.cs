using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Payment.Application.Validators;
using Payment.Domain.Interfaces;
using Payment.Domain.Models;
using Payment.Domain.Utils.Constants;
using Payment.Domain.ViewModel.Sale;

namespace Payment.Application.Services;

public class SaleService : BaseService<Sale>, ISaleService
{
    private readonly IBaseRepository<Sale> _saleRepository;
    private readonly IBaseRepository<Salesman> _salesmanRepository;
    private readonly IBaseRepository<Product> _productRepository;
    
    public SaleService (
        IBaseRepository<Sale> saleRepository, 
        IBaseRepository<Salesman> salesmanRepository,
        IBaseRepository<Product> productRepository,
        IMapper mapper
        ) : base(saleRepository, mapper)
    {
        _saleRepository = saleRepository;
        _salesmanRepository = salesmanRepository;
        _productRepository = productRepository;
    }

    public Sale CreateSale(CreateSaleViewModel createSaleViewModel)
    {
        Sale sale = new()
        {
            
            Salesman = _salesmanRepository.GetByIdOrThrow(createSaleViewModel.SalesmanId),
            SaleProductGroups = new List<SaleProductGroup>()
            
        };
        foreach (CreateSaleItem? item in createSaleViewModel.SaleItems)
        {
            SaleProductGroup saleProductGroup = new()
            {
                Quantity = item.Quantity,
                Product = _productRepository.GetByIdOrThrow(item.ProductId)
            };
            sale.SaleProductGroups.Add(saleProductGroup);
        }

        return Create<Sale, SaleValidator>(sale);
    }

    public void ChangeSaleStatus(Guid id, SaleStatusEnum saleStatus)
    {
        Sale sale = _saleRepository.GetByIdOrThrow(id);

        switch (saleStatus)
        {
            case SaleStatusEnum.PaymentApproved:
                if (sale.StatusId != (int)SaleStatusEnum.WaitingForPayment)
                    throw new ValidationException($"The status change from {sale.StatusId} to {saleStatus} is invalid");
                break;
                    
            case SaleStatusEnum.Canceled:
                if (sale.StatusId != SaleStatusEnum.WaitingForPayment 
                    && sale.StatusId != SaleStatusEnum.PaymentApproved)
                    throw new ValidationException($"The status change from {sale.StatusId} to {saleStatus} is invalid");
                break;
            
            case SaleStatusEnum.SentToShippingCompany:
                if (sale.StatusId != SaleStatusEnum.PaymentApproved)
                    throw new ValidationException($"The status change from {sale.StatusId} to {saleStatus} is invalid");
                break;
            
            case SaleStatusEnum.Delivered:
                if (sale.StatusId != SaleStatusEnum.SentToShippingCompany)
                    throw new ValidationException($"The status change from {sale.StatusId} to {saleStatus} is invalid");
                break;

            case SaleStatusEnum.WaitingForPayment:
            default:
                throw new ValidationException($"The status change from {sale.StatusId} to {saleStatus} is invalid");
        }
        sale.StatusId = saleStatus;
        
        Update<Sale, SaleValidator>(sale);
    }
}