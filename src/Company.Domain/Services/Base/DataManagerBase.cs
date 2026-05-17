using AutoMapper;
using Company.Data.Models.Base;
using Company.Data.Repositories.Base;
using Company.Domain.Models.Base;

namespace Company.Domain.Services.Base;

public abstract class DataManagerBase<TModel, TEntity, TRepository> : IDataManager<TModel>
    where TModel : class, IModel
    where TEntity : class, IEntity
    where TRepository : IRepository<TEntity>
{
    protected DataManagerBase(
        IMapper mapper,
        TRepository repository)
    {
        Mapper = mapper;
        Repository = repository;
    }

    protected IMapper Mapper { get; }
    protected TRepository Repository { get; }

    public virtual async Task<TModel> Create(
        TModel model,
        CancellationToken cancellationToken = default)
    {
        return Mapper.Map<TModel>(await Repository.Create(Mapper.Map<TEntity>(model), cancellationToken));
    }

    public virtual async Task<TModel> Update(
        TModel model,
        CancellationToken cancellationToken = default)
    {
        return Mapper.Map<TModel>(await Repository.Update(Mapper.Map<TEntity>(model), cancellationToken));
    }

    public virtual async Task<TModel> Delete(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return Mapper.Map<TModel>(await Repository.Delete(id, cancellationToken));
    }
}
