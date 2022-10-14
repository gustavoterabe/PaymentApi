using Payment.Domain.Models;

namespace Payment.Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    public TEntity GetByIdOrThrow(Guid id);
    public List<TEntity> GetAll();
    public TEntity Insert(TEntity obj);
    public void Update(TEntity obj);
    public void Delete(Guid id);
}