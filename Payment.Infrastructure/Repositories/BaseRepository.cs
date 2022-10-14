using System.Data.Entity.Core;
using Payment.Domain.Interfaces;
using Payment.Domain.Models;
using Payment.Infrastructure.Context;

namespace Payment.Infrastructure.Repositories;

public class BaseRepository<TEntity>  : IBaseRepository<TEntity> 
    where TEntity: BaseEntity
{
    private readonly PaymentApiContext _context;

    public BaseRepository(PaymentApiContext context)
    {
        _context = context;
    }

    public TEntity GetByIdOrThrow(Guid id)
    {
        TEntity? obj = _context.Set<TEntity>().Find(id);

        if (obj == null)
            throw new ObjectNotFoundException($"Product with id {id} not found!");
        
        return obj;
    }

    public List<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }

    public TEntity Insert(TEntity obj)
    {
        TEntity newObj = _context.Set<TEntity>().Add(obj).Entity;
        _context.SaveChanges();
        
        return newObj;
    }

    public void Update(TEntity obj)
    {
        _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        _context.Set<TEntity>().Remove(GetByIdOrThrow(id));
        _context.SaveChanges();
    }
}