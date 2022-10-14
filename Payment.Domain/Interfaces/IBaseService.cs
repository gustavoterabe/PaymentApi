using FluentValidation;
using Payment.Domain.Models;

namespace Payment.Domain.Interfaces;

public interface IBaseService<TEntity> where TEntity : BaseEntity
{
    public TEntity GetById(Guid id);
    public IList<TEntity> GetAll();
    public TEntity Create<TInputViewModel,TValidator>(TInputViewModel inputViewModel)
        where TInputViewModel : class
        where TValidator : AbstractValidator<TEntity>;
    public void Update<TInputViewModel,TValidator>(TInputViewModel inputViewModel) 
        where TValidator : AbstractValidator<TEntity>
        where TInputViewModel : class;
    public void Delete(Guid id);
}