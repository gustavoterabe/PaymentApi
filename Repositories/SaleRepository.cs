using System.Data.Entity.Core;
using AutoMapper;
using PaymentApi.Context;
using PaymentApi.Models;
using PaymentApi.Utils.Constants;
using PaymentApi.ViewModel;

namespace PaymentApi.Repositories;

public class SaleRepository: ISaleRepository
{
    private readonly PaymentApiContext _context;
    private readonly IMapper _mapper;

    public SaleRepository(PaymentApiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Sale CreateAndReturnSale(CreateSaleViewModel sale)
    {
        return _context.Sales.Add(_mapper.Map<Sale>(sale)).Entity;
    }

    public Sale GetById(string id)
    {
        Sale? sale = _context.Sales.Find(id); 
        
        if (sale == null)
            throw new ObjectNotFoundException($"Sale with Id {id} not found");

        return sale;
    }

    public void ChangeStatus(SaleStatusEnum status, string id)
    {
        Sale sale = GetById(id);

        sale.status = status;
        _context.SaveChanges();
    }
}