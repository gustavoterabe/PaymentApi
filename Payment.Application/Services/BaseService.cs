using AutoMapper;
using FluentValidation;
using Payment.Domain.Interfaces;
using Payment.Domain.Models;

namespace Payment.Application.Services;

public class BaseService<TEntity> : IBaseService<TEntity> 
    where TEntity : BaseEntity
{
    private readonly IBaseRepository<TEntity> _baseRepository;
    private readonly IMapper _mapper;

    protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
    {
        _baseRepository = baseRepository;
        _mapper = mapper;
    }

    public TEntity GetById(Guid id) =>
        _baseRepository.GetByIdOrThrow(id);


    public IList<TEntity> GetAll() =>
        _baseRepository.GetAll();
    
    public TEntity Create<TInputViewModel, TValidator>(TInputViewModel inputViewModel)
        where TInputViewModel : class
        where TValidator : AbstractValidator<TEntity>
    {
        TEntity entity = _mapper.Map<TEntity>(inputViewModel);

        Validate(entity, Activator.CreateInstance<TValidator>());
        TEntity newEntity = _baseRepository.Insert(entity);

        return newEntity;
    }

    public void Update<TInputViewModel, TValidator>(TInputViewModel inputViewModel)
        where TValidator : AbstractValidator<TEntity>
        where TInputViewModel : class
    {
        TEntity entity = _mapper.Map<TEntity>(inputViewModel);

        Validate(entity, Activator.CreateInstance<TValidator>());
        _baseRepository.Update(entity);
    }

    public void Delete(Guid id) =>
        _baseRepository.Delete(id);
            
    private void Validate(TEntity obj, AbstractValidator<TEntity> validator)
    {
        if (obj == null)
            throw new Exception("Registers not found!");

        validator.ValidateAndThrow(obj);
    }
}