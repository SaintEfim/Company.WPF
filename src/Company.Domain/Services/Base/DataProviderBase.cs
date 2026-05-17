using AutoMapper;
using Company.Data.Models.Base;
using Company.Data.Repositories.Base;
using Company.Domain.Models.Base;

namespace Company.Domain.Services.Base;

public abstract class DataProviderBase<TModel, TEntity, TRepository> : IDataProvider<TModel>
    where TModel : class, IModel
    where TEntity : class, IEntity
    where TRepository : IRepository<TEntity>
{
    protected DataProviderBase(
        IMapper mapper,
        TRepository repository)
    {
        Mapper = mapper;
        Repository = repository;
    }

    protected IMapper Mapper { get; }
    protected TRepository Repository { get; }

    public async Task<IEnumerable<TModel>> Get(
        CancellationToken cancellationToken = default)
    {
        return Mapper.Map<IEnumerable<TModel>>(await Repository.Get(cancellationToken));
    }

    public async Task<TModel> GetOneById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return Mapper.Map<TModel>(await Repository.GetOneById(id, cancellationToken));
    }
}
